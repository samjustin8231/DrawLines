using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System;
using UnityEngine;

[DisallowMultipleComponent]
public class DataManager : MonoBehaviour
{
	/// <summary>
	/// The scene missions data.
	/// (will be loaded from the Missions scene)
	/// </summary>
	private static List<MissionData> sceneMissionsData;

	/// <summary>
	/// The file missions data.
	/// (will be loaded from the file)
	/// </summary>
	private static List<MissionData> fileMissionsData;

	/// <summary>
	/// The filterd missions data.
	/// (The final missions data after filtering)
	/// </summary>
	public static List<MissionData> filterdMissionsData;

	/// <summary>
	/// The name of the file.
	/// </summary>
	private static string fileName = "drawlines-data.bin";

	/// <summary>
	/// The path of the file.
	/// </summary>
	private static string path;

	/// <summary>
	/// Whether the Missions data is empty or null.
	/// </summary>
	private static bool isNullOrEmpty;

	/// <summary>
	/// Whether it's need to save new data.
	/// </summary>
	private static bool needsToSaveNewData;

	void Awake ()
	{
		Debug.Log ("DataManager Awake");
		try {
			#if UNITY_IPHONE
						//Enable the MONO_REFLECTION_SERIALIZER(For IOS Platform Only)
						System.Environment.SetEnvironmentVariable ("MONO_REFLECTION_SERIALIZER", "yes");
			#endif
						
			///Reset flags
			isNullOrEmpty = false;
			needsToSaveNewData = false;

			///Load Missions data from the Missions Scene
			sceneMissionsData = LoadMissionsDataFromScene ();
			if (sceneMissionsData == null) {
				return;
			}

			if (sceneMissionsData.Count == 0) {
				return;
			}

			///Load Missions data from the file
			fileMissionsData = LoadMissionsDataFromFile ();

			if (fileMissionsData == null) {
				isNullOrEmpty = true;
			} else {
				if (fileMissionsData.Count == 0) {
					isNullOrEmpty = true;
				}
			}

			///If the Missions data in the file is null or empty,then save the scene Missions data to the file
			if (isNullOrEmpty) {
				SaveMissionsDataToFile (sceneMissionsData);
				filterdMissionsData = sceneMissionsData;
			} else {
				///Otherwise get the filtered Missions Data
				filterdMissionsData = GetFilterdMissionsData ();
				///If it's need to save a new Missions data to the file ,the save it
				if (needsToSaveNewData) {
					SaveMissionsDataToFile (filterdMissionsData);
				}
			}
		} catch (Exception ex) {
			Debug.Log (ex.Message);
		}
	}

	///MissionData is class used for persistent loading and saving
	[System.Serializable]
	public class MissionData
	{
		public int ID;
//the ID of the Mission
		public List<LevelData> levelsData = new List<LevelData> ();
//the levels of the mission

		/// <summary>
		/// Find the level data by ID.
		/// </summary>
		/// <returns>The level data.</returns>
		/// <param name="ID">the ID of the level.</param>
		public LevelData FindLevelDataById (int ID)
		{
			foreach (LevelData levelData in levelsData) {
				if (levelData.ID == ID) {
					return levelData;
				}
			}
			return null;
		}
	}

	///LevelData is class used for persistent loading and saving
	[System.Serializable]
	public class LevelData
	{
		public int ID;
//THE id of the level
		public bool isLocked = true;
		public TableLevel.StarsNumber starsLevel = TableLevel.StarsNumber.ZERO;
//number of stars (stars rating)
	}


	/// <summary>
	/// 重置游戏
	/// 对于每个mission下的每个Level遍历，重置locked和startLevel,然后保存
	/// Reset the game data.
	/// </summary>
	public static void ResetData ()
	{
		try {
			//从文件加载missions数据
			fileMissionsData = LoadMissionsDataFromFile ();

			if (fileMissionsData == null) {
				return;
			}

			//对于每个mission下的每个Level遍历，重置locked和startLevel,然后保存
			foreach (MissionData missionData in fileMissionsData) {
				if (missionData == null) {
					continue;
				}
				foreach (LevelData levelData in missionData.levelsData) {
					if (levelData == null) {
						continue;
					}

					//该关的数据清空：未解锁，start = 0
					///UnLock the level of ID equals 1(first level) ,otherwise lock the others
					if (levelData.ID == 1) {
						levelData.isLocked = false;
					} else {
						levelData.isLocked = true;
					}

					///Set star level to zero for each level
					levelData.starsLevel = TableLevel.StarsNumber.ZERO;
				}
			}
			//保存到文件
			SaveMissionsDataToFile (fileMissionsData);
		} catch (Exception ex) {
			Debug.Log (ex.Message);
			return;
		}
		Debug.Log ("Game Data has been reset successfully");
	}

	/// <summary>
	/// Load the missions data from the scene.
	/// </summary>
	/// <returns>The missions data from the scene.</returns>
	private List<MissionData> LoadMissionsDataFromScene ()
	{
		Debug.Log ("Loading Missions Data");

		GameObject[] missions = GameObject.FindGameObjectsWithTag ("Mission");

		if (missions == null) {
			Debug.Log ("No Mission with 'Mission' tag found");
			return null;
		}

		Mission tempMission = null;
		LevelsManager tempLevelManager = null;

		List<MissionData> tempMissionsData = new List<MissionData> ();
		MissionData tempMissionData = null;
		foreach (GameObject missionGameObject in missions) {
			tempMission = missionGameObject.GetComponent<Mission> ();
			tempLevelManager = missionGameObject.GetComponent<LevelsManager> ();
			tempMissionData = new MissionData ();

			tempMissionData.ID = tempMission.ID;
			tempMissionData.levelsData = GetLevelData (tempLevelManager.levels);

			tempMissionsData.Add (tempMissionData);
		}

		return tempMissionsData;
	}

	/// <summary>
	/// Get the level data.
	/// </summary>
	/// <returns>The new levels data.</returns>
	/// <param name="levels">The current levels data.</param>
	private List<LevelData> GetLevelData (List<Level> levels)
	{
		if (levels == null) {
			return null;
		}

		LevelData tempLevelData = null;
		List<LevelData> tempLevelsData = new List<LevelData> ();
		int ID = 1;
		for (int i = 0; i < levels.Count; i++) {
			tempLevelData = new LevelData ();
			tempLevelData.ID = ID;
			ID++;
			if (i == 0) {
				///First level must be unlocked
				tempLevelData.isLocked = false;
			}
			tempLevelsData.Add (tempLevelData);
		}

		return tempLevelsData;
	}

	/// <summary>
	/// Get the filterd missions data.
	/// (Compare the Missions data in the file with the Missions data in the scene)
	/// Scenario :
	/// -you may have a set of missions saved into file
	/// -if you add/delete a mission using inspector
	/// -then it's need to determine and save the new data
	/// </summary>
	/// <returns>The filterd missions data.</returns>
	private List<MissionData> GetFilterdMissionsData ()
	{
		if (fileMissionsData == null || sceneMissionsData == null) {
			return null;
		}

		MissionData tempMissionData = null;
		List<MissionData> tempFilteredMissionsData = new List<MissionData> ();

		foreach (MissionData missionData in sceneMissionsData) {
			///Get the Mission data from the file
			tempMissionData = FindMissionDataById (missionData.ID, fileMissionsData);
			if (tempMissionData != null) {
				///If the number of levels in the scene Mission Equals to the number of levels in the file Mission
				if (missionData.levelsData.Count == tempMissionData.levelsData.Count) {
					///Add tempMissionData(file mission data) to the filtered list
					tempFilteredMissionsData.Add (tempMissionData);
				} else {//Otherwise,its need to save new data
					//TODO:levels data will be lost,when a level is added or removed
					needsToSaveNewData = true;
					///Add the  missionData(scene mission data) to the filtered list 
					tempFilteredMissionsData.Add (missionData);
				}
			} else {//Otherwise,its need to save new data
				needsToSaveNewData = true;
				///Add the missionData(scene mission data) to the filtered list 
				tempFilteredMissionsData.Add (missionData);
			}
		}
		return tempFilteredMissionsData;
	}

	/// <summary>
	/// Save the missions data to the file.
	/// </summary>
	/// <param name="missionsData">Missions data.</param>
	public static void SaveMissionsDataToFile (List<MissionData> missionsData)
	{
		Debug.Log ("Saving Missions Data");
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (path)) {
			Debug.Log ("Null or Empty path");
			return;
		}

		if (missionsData == null) {
			Debug.Log ("Null Data");
			return;
		}
		
		FileStream file = null;
		try {
			BinaryFormatter bf = new BinaryFormatter ();
			file = File.Open (path, FileMode.Create);
			bf.Serialize (file, missionsData);
			file.Close ();
		} catch (Exception ex) {
			file.Close ();
			Debug.LogError ("Exception : " + ex.Message);
		}
	}

	/// <summary>
	/// 从文件中加载missions数据
	/// Load the missions data from the file.
	/// </summary>
	/// <returns>The Missions data.</returns>
	public static List<MissionData> LoadMissionsDataFromFile ()
	{
		SettingUpFilePath ();
		if (string.IsNullOrEmpty (path)) {
			Debug.Log ("Null or Empty path");
			return null;
		}
		if (!File.Exists (path)) {
			Debug.Log (path + " is not exists");
			return null;
		}

		List<MissionData> missionsData = null;
		FileStream file = null;
		try {
			BinaryFormatter bf = new BinaryFormatter ();
			file = File.Open (path, FileMode.Open);
			missionsData = (List<MissionData>)bf.Deserialize (file);
			file.Close ();
		} catch (Exception ex) {
			file.Close ();
			Debug.LogError ("Exception : " + ex.Message);
		}
		
		return missionsData;
	}

	/// <summary>
	/// Finds the mission data by id.
	/// </summary>
	/// <returns>The mission data by ID.</returns>
	/// <param name="ID">the ID of the mission.</param>
	/// <param name="missionsData">Missions data list to search in.</param>
	public static MissionData FindMissionDataById (int ID, List<MissionData> missionsData)
	{
		if (missionsData == null) {
			return null;
		}
		
		foreach (MissionData missionData in missionsData) {
			if (missionData.ID == ID) {
				return missionData;
			}
			
		}
		return null;
	}

	/// <summary>
	/// Settings up the path of the file ,relative to the current platform.
	/// </summary>
	private static void SettingUpFilePath ()
	{
		if (Application.platform == RuntimePlatform.Android) {//Android Platform
			path = Application.persistentDataPath;
		} else if (Application.platform == RuntimePlatform.IPhonePlayer) {//IOS Platform
			///Get iPhone Documents Path
			string dataPath = Application.dataPath;
			string fileBasePath = dataPath.Substring (0, dataPath.Length - 5);
			path = fileBasePath.Substring (0, fileBasePath.LastIndexOf ('/')) + "/Documents";
		} else {//Others
			path = Application.dataPath;
		}
				
		path += "/" + fileName;
	}
}