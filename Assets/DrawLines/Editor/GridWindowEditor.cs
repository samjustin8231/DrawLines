using UnityEngine;
using UnityEditor;

///Created by Indie Games Studio
///https://www.assetstore.unity3d.com/en/#!/publisher/9268

namespace DrawLinesEditors
{
		public class GridWindowEditor : EditorWindow
		{
				#if UNITY_EDITOR
				private Vector2 scrollPos;
				private static Level level;
				private static int numberOfRows, numberOfColumns;
				private static Level.DotsPair dotPair;
				private static Texture2D texture;
				private static int gridCellndex;
				private Vector2 offset = new Vector2 (0, 0);
				private Vector2 scale = new Vector2 (45, 45);
				private Vector2 scrollView = new Vector2 (550, 430);
				private static GridWindowEditor window;
				private static string levelTitle;

				public static void Init (Level lvl, string lvlTitle, int NOR, int NOC)
				{
						levelTitle = lvlTitle;
						numberOfRows = NOR;
						numberOfColumns = NOC;
						level = lvl;
						window = (GridWindowEditor)EditorWindow.GetWindow (typeof(GridWindowEditor));
						window.title = levelTitle;
				}

				void OnGUI ()
				{
						if (window == null) {
								return;
						}

						window.Repaint ();

						if (level == null) {
								return;
						}

						scrollView = new Vector2 (position.width, position.height);
						scrollPos = EditorGUILayout.BeginScrollView (scrollPos, GUILayout.Width (scrollView.x), GUILayout.Height (scrollView.y));

						GUILayout.Space (5);
						for (int i = 0; i < numberOfRows; i++) {

								GUILayout.BeginHorizontal ();
								GUILayout.Space (5);

								for (int j = 0; j < numberOfColumns; j++) {
										GUI.contentColor = Color.clear;
										gridCellndex = i * numberOfColumns + j;
										getGridCellTexture (gridCellndex);

										if (GUILayout.Button (texture, GUILayout.Width (scale.x), GUILayout.Height (scale.y))) {
												EditorUtility.DisplayDialog ("GridCell", "GridCell of index " + gridCellndex, "ok");
										}

										GUILayout.Space (offset.x);
								}
								GUILayout.EndHorizontal ();
								GUILayout.Space (offset.y);

								GUI.contentColor = Color.white;
								GUILayout.BeginHorizontal ();
								GUILayout.Space (5);
								for (int j = 0; j < numberOfColumns; j++) {
										gridCellndex = i * numberOfColumns + j;

										GUILayout.TextField (gridCellndex + "", GUILayout.Width (scale.x), GUILayout.Height (20));
										GUILayout.Space (offset.x);
								}
								GUILayout.EndHorizontal ();

						}
						EditorGUILayout.Separator ();
						EditorGUILayout.Separator ();
			
						EditorGUILayout.BeginHorizontal ();
						GUILayout.Space (scrollView.x / 2 - 50);
						GUI.contentColor = Color.yellow;
						EditorGUILayout.LabelField (numberOfRows + "x" + numberOfColumns + " Grid");
						GUI.contentColor = Color.white;
						EditorGUILayout.EndHorizontal ();
						EditorGUILayout.Separator ();

						EditorGUILayout.EndScrollView ();
				}

				private void getGridCellTexture (int gridCellndex)
				{
						texture = null;

						if (level == null) {
								return;
						}

						foreach (Level.DotsPair dotPair in level.dotsPairs) {

								if (dotPair.firstDot.index == gridCellndex || dotPair.secondDot.index == gridCellndex) {
										if (dotPair.applyColorOnSprite)  
												GUI.contentColor = dotPair.color;
										else
												GUI.contentColor = Color.white;

										if (dotPair.sprite != null)
												texture = dotPair.sprite.texture;
								}
						}
				}
			#endif
		}
}