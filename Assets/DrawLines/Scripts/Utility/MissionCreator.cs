using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine.UI;
#endif

#pragma warning disable 0168 // variable declared but not used.

[DisallowMultipleComponent]
[ExecuteInEditMode]
public class MissionCreator : MonoBehaviour
{
	#if UNITY_EDITOR
		private GameObject missionsParent;
		private int tempId, greatestMissionId;
	
		public void Awake ()
		{
				RectTransform missionsRectTransform;
				missionsParent = GameObject.Find ("Missions");

				if (missionsParent == null) {
						missionsParent = new GameObject ("Missions");
				}

				missionsRectTransform = missionsParent.GetComponent<RectTransform> ();
		
  				if (missionsRectTransform == null) {
						missionsRectTransform = (RectTransform)missionsParent.AddComponent<RectTransform> ();
				}
				missionsRectTransform.pivot = new Vector2 (0.5f, 1);

				
				EditorApplication.hierarchyWindowChanged = CheckMissionInstances;
		}


		[MenuItem("Tools/Draw Lines/New Mission #m")]
		static void CreateNewMission ()
		{
				GameObject newMission = new GameObject ();
				newMission.AddComponent<Mission> ();
				RectTransform missionRectTransform = (RectTransform)newMission.GetComponent<RectTransform> ();
				if (missionRectTransform == null) {
						newMission.AddComponent<RectTransform> ();
				}
				newMission.transform.localScale = Vector3.one;
				newMission.transform.localPosition = Vector3.zero;
		}

		//Hierarchy Window Changed Event
		private void CheckMissionInstances ()
		{
				Mission [] missions = GameObject.FindObjectsOfType (typeof(Mission)) as Mission[];
				if (missions == null) {
						return;
				}
		
				greatestMissionId = GetLastMissionID (missions);
				foreach (Mission missionInstance in missions) {
						try {
								if (!int.TryParse (missionInstance.gameObject.name.Split ('-') [1], out tempId)) {
										PrepareTheMission (missionInstance.gameObject);
								}
				
						} catch (Exception ex) {
								PrepareTheMission (missionInstance.gameObject);
						}
				}
		}
	
		//Get the Greatest Mission ID from the Hierarchy
		private int GetLastMissionID (Mission[] missions)
		{
				greatestMissionId = 0;
		
				if (missions == null) {
						return greatestMissionId;
				}
		
				tempId = 0;
				foreach (Mission missionInstance in missions) {
						try {
								if (int.TryParse (missionInstance.gameObject.name.Split ('-') [1], out tempId)) {
										if (greatestMissionId < tempId) {
												greatestMissionId = tempId;
										}
								}
				
						} catch (Exception ex) {
						}
				}
				return greatestMissionId;
		}
	
		//Prepare the new mission
		private void PrepareTheMission (GameObject mission)
		{
				if (mission == null) {
						return;
				}
		       
				bool displayMessage = false;
				RectTransform missionRectTransform = mission.GetComponent<RectTransform> ();
				RectTransform missionTitleRectTransform;
				GridLayoutGroup missionsGridLayoutGroup = missionsParent.GetComponent<GridLayoutGroup> ();

				if (missionsGridLayoutGroup == null) {
						missionsGridLayoutGroup = (GridLayoutGroup)missionsParent.AddComponent (typeof(GridLayoutGroup));
						missionsGridLayoutGroup.cellSize = new Vector2 (300, 65);
						missionsGridLayoutGroup.spacing = new Vector2 (0, 12);
						missionsGridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
						missionsGridLayoutGroup.constraintCount = 1;
						ContentSizeFitter missionsCntentSizeFilter = (ContentSizeFitter)missionsParent.AddComponent (typeof(ContentSizeFitter));
						missionsCntentSizeFilter.horizontalFit = missionsCntentSizeFilter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
						displayMessage = true;
				}

				Vector2 cellSize = missionsGridLayoutGroup.cellSize;
				if (missionRectTransform == null) {
						mission.AddComponent (typeof(RectTransform));
				}

				greatestMissionId++;
				UIExtension.SetSize (missionRectTransform, cellSize);
				mission.transform.SetParent (missionsParent.transform);
				mission.name = "Mission-" + greatestMissionId;
				mission.tag = "Mission";
				mission.transform.localScale = Vector3.one;
				mission.transform.localPosition = Vector3.zero;
				Image missionImage = mission.GetComponent<Image> ();
				if (missionImage != null) {
						missionImage.preserveAspect = true;
				}

				GameObject title = new GameObject ("Title");
				title.transform.SetParent (mission.transform);
				missionTitleRectTransform = (RectTransform)title.AddComponent (typeof(RectTransform));
				UIExtension.SetSize (missionTitleRectTransform, cellSize);
				title.name = "Title";
				Text textComponent = (Text)title.AddComponent (typeof(Text));
				textComponent.text = "New Mission";
				textComponent.color = new Color (66, 51, 51, 255) / 255.0f;
				textComponent.fontSize = 35;
				textComponent.fontStyle = FontStyle.Bold;
				textComponent.alignment = TextAnchor.MiddleCenter;
	        
				title.transform.localScale = Vector3.one;
				title.transform.localPosition = Vector3.zero;

				Debug.Log ("New Mission 'Mission-" + greatestMissionId + "' has been created");
				if (displayMessage) {
						EditorUtility.DisplayDialog ("Missions Creator Notification ", "Move the Missions Gameobject to the UICanvas, and then set the scale vector to [1,1,1]", "ok");
				}
		}
	#endif
}