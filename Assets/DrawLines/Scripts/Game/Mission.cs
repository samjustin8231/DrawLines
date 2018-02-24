using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;

#pragma warning disable 0168 // variable declared but not used.

[DisallowMultipleComponent]
[RequireComponent(typeof(LevelsManager))]
[RequireComponent(typeof(MissionCreator))]
[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Button))]

/// <summary>
/// A Mission Component used with the LevelsManager Component.
/// To create an new mission you need to add this Component.
/// </summary>
[ExecuteInEditMode]
public class Mission : MonoBehaviour
{
		/// <summary>
		/// Mission ID.
		/// </summary>
		[HideInInspector]
		public int ID = -1;

		/// <summary>
		/// The wanted or selected Mission.
		/// </summary>
		public static Mission wantedMission;

		/// <summary>
		/// The color of the mission.
		/// </summary>
		public Color missionColor = Color.white;

		/// <summary>
		/// The title of the mission .
		/// </summary>
		public string missionTitle = "New Mission";

		/// <summary>
		/// Number of Rows.
		/// </summary>
		[HideInInspector]
		public int rowsNumber;

		/// <summary>
		/// Number of Columns.
		/// </summary>
		[HideInInspector]
		public int colsNumber;

		/// <summary>
		/// The LevelsManager Component.
		/// </summary>
		[HideInInspector]
		public LevelsManager levelsManagerComponent;

		void Awake ()
		{
				InitMission ();
		}

		void Update ()
		{
			#if UNITY_EDITOR
				if (!Application.isPlaying) {
						InitMission ();
				}
			#endif
		}
		
		/// <summary>
		/// Inits the mission.
		/// </summary>
		private void InitMission ()
		{
				///Setting up the ID of the Mission
				if (Application.isPlaying) {
						bool validName = int.TryParse (name.Split ('-') [1], out ID);
						if (!validName) {
								Debug.LogError ("Invalid Mission Name");
						}
				}

				//Setting up the LevelsManager Component
				levelsManagerComponent = GetComponent<LevelsManager> ();
		
				if (levelsManagerComponent != null) {
						if (string.IsNullOrEmpty (missionTitle) || missionTitle == "New Mission") {
								//Define the Title of the Mission
								missionTitle = levelsManagerComponent.numberOfRows + "x" + levelsManagerComponent.numberOfCols;
						}
				}
		
				if (Application.isPlaying) {
						Debug.Log ("Setting up Mission <b>" + missionTitle + "</b> of ID " + ID);
				}

				///Get the Number of Rows from LevelsManager Component
				rowsNumber = levelsManagerComponent.numberOfRows;
				///Get the Number of Columns from LevelsManager Component
				colsNumber = levelsManagerComponent.numberOfCols;
				
				//Apply missioncolor to Mission SpriteRenderer Component Color
				GetComponent<Image> ().color = missionColor;
				Transform missionTitleTransform = transform.Find ("Title");
		
				///Setting up the Title of the Mission
				if (missionTitleTransform != null) {
						Text uiText = missionTitleTransform.GetComponent<Text> ();
						if (uiText != null)
								uiText.text = missionTitle;
				}
		}
}