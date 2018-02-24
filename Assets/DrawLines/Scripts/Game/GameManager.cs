using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

#pragma warning disable 0168 // variable declared but not used.

[DisallowMultipleComponent]
public class GameManager : MonoBehaviour
{
		/// <summary>
		/// The grid cell prefab.
		/// </summary>
		public GameObject gridCellPrefab;

		[Range(0,1)]
	   /// <summary>
	   /// The grid cell top background alpha.
	   /// </summary>
		public float gridCellTopBackgroundAlpha = 0.5f;

		/// <summary>
		/// The grid cell z-position.
		/// </summary>
		[Range(-20,20)]
		public float gridCellZPosition = 0;
	
		/// <summary>
		/// The grid line prefab.
		/// </summary>
		public GameObject gridLinePrefab;
	
		/// <summary>
		/// The grid line z-position.
		/// </summary>
		[Range(-50,50)]
		public float gridLineZPosition = -2;
	
		/// <summary>
		/// The grid line width factor.
		/// </summary>
		[Range(0,10)]
		public float gridLineWidthFactor = 0.5f;

		/// <summary>
		/// The cell content prefab.
		/// </summary>
		public GameObject cellContentPrefab;

		/// <summary>
		/// The cell content scale factor.
		/// </summary>
		[Range(0.1f,1)]
		public float cellContentScaleFactor = 0.6f;

		/// <summary>
		/// The cell content z-position.
		/// </summary>
		[Range(-20,20)]
		public float cellContentZPosition = -5;

		/// <summary>
		/// The dragging element prefab.
		/// </summary>
		public GameObject draggingElementPrefab;

		[Range(0,1)]
	   /// <summary>
	   /// The dragging element alpha.
	   /// </summary>
		public float draggingElementAlpha = 0.47f;

		/// <summary>
		/// The dragging element z-position.
		/// </summary>
		[Range(-50,50)]
		public float draggingElementZPosition = 0;

		/// <summary>
		/// The dragging element scale factor.
		/// </summary>
		[Range(0,10)]
		public float draggingElementScaleFactor = 1;

		/// <summary>
		/// The grid.
		/// </summary>
		public GameObject grid;

		/// <summary>
		/// The grid contents transform.
		/// </summary>
		public Transform gridContentsTransform;

		/// <summary>
		/// The grid top pivot transform.
		/// </summary>
		public Transform gridTopPivotTransform;
	
		/// <summary>
		/// The grid bottom pivot transform.
		/// </summary>
		public Transform gridBottomPivotTransform;

		/// <summary>
		/// The grid lines transfrom.
		/// </summary>
		public Transform gridLinesTransfrom;
	
		/// <summary>
		/// The level text.
		/// </summary>
		public Text levelText;

		/// <summary>
		/// The mission text.
		/// </summary>
		public Text missionText;

		/// <summary>
		/// The movements text.
		/// </summary>
		public Text movementsText;

		/// <summary>
		/// The grid cells in the grid.
		/// </summary>
		public static GridCell[] gridCells;

		/// <summary>
		/// The grid lines in the grid.
		/// </summary>
		public static Line[] gridLines;

		/// <summary>
		/// The number of rows of the grid.
		/// </summary>
		public static int numberOfRows;

		/// <summary>
		/// The number of columns of the grid.
		/// </summary>
		public static int numberOfColumns;

		/// <summary>
		/// The water bubble sound effect.
		/// </summary>
		public AudioClip waterBubbleSFX;

		/// <summary>
		/// The level locked sound effect.
		/// </summary>
		public AudioClip levelLockedSFX;

		/// <summary>
		/// The connected sound effect.
		/// </summary>
		public AudioClip connectedSFX;

		/// <summary>
		/// The next button image.
		/// </summary>
		public Image nextButtonImage;

		/// <summary>
		/// The back button image.
		/// </summary>
		public Image backButtonImage;

		/// <summary>
		/// The movements counter.
		/// </summary>
		public static int movements;

		/// <summary>
		/// current line in the grid.
		/// </summary>
		private Line currentLine;

		/// <summary>
		/// Temp ray cast hit 2d for ray casting.
		/// </summary>
		private RaycastHit2D tempRayCastHit2D;

		/// <summary>
		/// Temp click position.
		/// </summary>
		private Vector3 tempClickPosition;

		/// <summary>
		/// The current(selected) grid cell.
		/// </summary>
		private GridCell currentGridCell;

		/// <summary>
		/// The previous grid cell.
		/// </summary>
		private GridCell previousGridCell;
		
		/// <summary>
		/// The current level.
		/// </summary>
		public static Level currentLevel;

		/// <summary>
		/// The size of the grid.
		/// </summary>
		private Vector2 gridSize;

		/// <summary>
		/// Temp point.
		/// </summary>
		private Vector3 tempPoint;

		/// <summary>
		/// Temp scale.
		/// </summary>
		private Vector3 tempScale;

		/// <summary>
		/// Temp color.
		/// </summary>
		private Color tempColor;

		/// <summary>
		/// Temp collider 2d.
		/// </summary>
		private Collider2D tempCollider2D;

		/// <summary>
		/// Temporary sprite renderer.
		/// </summary>
		private SpriteRenderer tempSpriteRendererd;

		/// <summary>
		/// Whether the dragging element is rendering(dragging)
		/// </summary>
		private bool drawDraggingElement;

		/// <summary>
		///Whether the current click is moving 
		/// </summary>
		private bool clickMoving;

		/// <summary>
		/// Whether the GameManager is running.
		/// </summary>
		private bool isRunning;

		/// <summary>
		/// The dragging element reference.
		/// </summary>
		private GameObject draggingElement;

		/// <summary>
		/// The dragging element sprite renderer.
		/// </summary>
		private SpriteRenderer draggingElementSpriteRenderer;

		/// <summary>
		/// The timer reference.
		/// </summary>
		private Timer timer;

		/// <summary>
		/// The effects audio source.
		/// </summary>
		public AudioSource effectsAudioSource;

		void Awake ()
		{
				///Setting up the references
				if (timer == null) {
						timer = GameObject.Find ("Time").GetComponent<Timer> ();
				}

				if (nextButtonImage == null) {
						nextButtonImage = GameObject.Find ("NextButton").GetComponent<Image> ();
				}

				if (backButtonImage == null) {
						backButtonImage = GameObject.Find ("BackButton").GetComponent<Image> ();
				}

				if (effectsAudioSource == null) {
						effectsAudioSource = GameObject.Find ("AudioSources").GetComponents<AudioSource> () [1];
				}
		}
	
		/// <summary>
		/// When the GameObject becomes visible
		/// </summary>
		void OnEnable ()
		{
				if (movementsText == null) {
						movementsText = GameObject.Find ("Movements").GetComponent<Text> ();
				}

				if (levelText == null) {
						levelText = GameObject.Find ("Level").GetComponent<Text> ();
				}

				if (missionText == null) {
						missionText = GameObject.Find ("MissionTitle").GetComponent<Text> ();
				}

				if (grid == null) {
						grid = GameObject.Find ("Grid");
				}

				if (gridContentsTransform == null) {
						gridContentsTransform = grid.transform.Find ("Contents").transform;
				}

				if (gridTopPivotTransform == null) {
						gridTopPivotTransform = GameObject.Find ("GridTopPivot").transform;
				}
		
				if (gridBottomPivotTransform == null) {
						gridBottomPivotTransform = GameObject.Find ("GridBottomPivot").transform;
				}
				if (gridLinesTransfrom == null) {
						gridLinesTransfrom = GameObject.Find ("GridLines").transform;
				}

				try {
						///Setting up Attributes
						numberOfRows = Mission.wantedMission.rowsNumber;
						numberOfColumns = Mission.wantedMission.colsNumber;
						//levelText.color = Mission.wantedMission.missionColor;
						missionText.text = Mission.wantedMission.missionTitle;
						grid.name = numberOfRows + "x" + numberOfRows + "-Grid";
				} catch (Exception ex) {
						Debug.Log (ex.Message);
				}


				///Determine the size of the Grid
				Vector3 gridTopPivotPostion = gridTopPivotTransform.transform.position;
				Vector3 gridBottomPiviotPosition = gridBottomPivotTransform.transform.position;
		
				gridSize = gridBottomPiviotPosition - gridTopPivotPostion;
				gridSize = new Vector2 (Mathf.Abs (gridSize.x), Mathf.Abs (gridSize.y));

				///Create New level (the selected level)
				CreateNewLevel ();
		}

		/// <summary>
		/// When the GameObject becomes invisible.
		/// </summary>
		void OnDisable ()
		{
				///stop the timer
				if (timer != null)
						timer.Stop ();
		}
	
		// Update is called once per frame
		void Update ()
		{
				if (!isRunning) {
						return;
				}

				if (gridLines == null || gridCells == null) {
						return;
				}

				if (Input.GetMouseButtonDown (0)) {
						RayCast (Input.mousePosition, ClickType.Began);
				} else if (Input.GetMouseButtonUp (0)) {
						Release (currentLine);
				}

				if (clickMoving) {
						RayCast (Input.mousePosition, ClickType.Moved);
				}

				if (drawDraggingElement) {
						if (!draggingElementSpriteRenderer.enabled) {
								draggingElementSpriteRenderer.enabled = true;
						}

						DrawDraggingElement (Input.mousePosition);
				} else {
						if (draggingElementSpriteRenderer.enabled) {
								draggingElementSpriteRenderer.enabled = false;
						}
				}
		}


		/// <summary>
		/// On Click Release event.
		/// </summary>
		/// <param name="line">The current Line.</param>
		private void Release (Line line)
		{
				clickMoving = false;
				drawDraggingElement = false;
				draggingElement.transform.Find ("ColorsEffect").GetComponent<ParticleEmitter>().emit = false;
				draggingElementSpriteRenderer.enabled = false;

				if (line != null) {
						if (!line.completedLine) {
								line.ClearPath ();
						}
				}

				previousGridCell = null;
				currentGridCell = null;
				currentLine = null;
		}

		/// <summary>
		/// Raycast the click (touch) on the screen.
		/// </summary>
		/// <param name="clickPosition">The position of the click (touch).</param>
		/// <param name="clickType">The type of the click(touch).</param>
		private void RayCast (Vector3 clickPosition, ClickType clickType)
		{
				tempClickPosition = Camera.main.ScreenToWorldPoint (clickPosition);
				tempRayCastHit2D = Physics2D.Raycast (tempClickPosition, Vector2.zero);
				tempCollider2D = tempRayCastHit2D.collider;

				if (tempCollider2D != null) {
						///When a ray hit a grid cell
						if (tempCollider2D.tag == "GridCell") {
								currentGridCell = tempCollider2D.GetComponent<GridCell> ();
								if (clickType == ClickType.Began) {
										previousGridCell = currentGridCell;
										drawDraggingElement = true;
										draggingElement.transform.Find ("ColorsEffect").GetComponent<ParticleEmitter>().emit = true;
										GridCellClickBegan ();
								} else if (clickType == ClickType.Moved) {
										drawDraggingElement = true;
										GridCellClickMoved ();
								}
						}
				}
		}

		/// <summary>
		/// When a click(touch) began on the GridCell.
		/// </summary>
		private void GridCellClickBegan ()
		{
				///If the current grid cell is not currently used and it's empty,then ignore it
				if (currentGridCell.isEmpty && !currentGridCell.currentlyUsed) {
						Debug.Log ("Current grid cell of index " + currentGridCell.index + " is Ignored [Reason : Empty,Not Currently Used]");
						return;
				} 

				///Increase the movements counter
				IncreaseMovements ();

				///If the grid cell is currently used
				if (currentGridCell.currentlyUsed) {

						if (currentGridCell.gridLineIndex == -1) {
								return;
						}

						///If the grid line of the current grid cell is completed,then reset the grid cells of the line
						if (gridLines [currentGridCell.gridLineIndex].completedLine) {
								Debug.Log ("Reset Grid cells of the Line Path of the index " + currentGridCell.gridLineIndex);
								gridLines [currentGridCell.gridLineIndex].completedLine = false;
								Release (gridLines [currentGridCell.gridLineIndex]);
								return;
						} 

				} else if (!currentGridCell.isEmpty) {//If the grid is not currently used and it's not empty
						///Setting up dragging element color
						tempColor = currentGridCell.topBackgroundColor;
						tempColor.a = gridCellTopBackgroundAlpha;
						//draggingElementSpriteRenderer = draggingElement.GetComponent<Image> ();
						draggingElementSpriteRenderer.color = tempColor;

						///Setting up the attributes for the current grid cell
						clickMoving = true;
						currentGridCell.currentlyUsed = true;
						if (currentLine == null) {
								currentLine = gridLines [currentGridCell.gridLineIndex];
						}

						Debug.Log ("New GridCell of Index " + currentGridCell.index + " added to the Line Path of index " + currentLine.index);

						///Add the current grid cell index to the current traced grid cells list
						currentLine.path.Add (currentGridCell.index);

						///Determine the New Line Point
						tempPoint = currentGridCell.transform.position;
						tempPoint.z = gridLineZPosition;

						///Add the position of the New Line Point to the current line
						gridLines [currentGridCell.gridLineIndex].AddPoint (tempPoint);
				}
		}

		/// <summary>
		/// When a click(touch) moved over the GridCell.
		/// </summary>
		private void GridCellClickMoved ()
		{
				if (currentLine == null) {
						Debug.Log ("Current Line is undefined");
						return;
				}

				if (currentGridCell == null) {
						Debug.Log ("Current GridCell is undefined");
						return;
				}

				if (previousGridCell == null) {
						Debug.Log ("Previous GridCell is undefined");
						return;
				}

				if (currentGridCell.index == previousGridCell.index) {
						return;
				}
	
				///If the current grid cell is not adjacent of the previous grid cell,then ignore it
				if (!previousGridCell.OneOfAdjacents (currentGridCell.index)) {
						Debug.Log ("Current grid cell of index " + currentGridCell.index + " is Ignored [Reason : Not Adjacent Of Previous GridCell " + previousGridCell.index);
						return;
				}

				///If the current grid cell is currently used
				if (currentGridCell.currentlyUsed) {
					
						if (currentGridCell.gridLineIndex == -1) {
								return;
						}

						if (currentGridCell.gridLineIndex == previousGridCell.gridLineIndex) {
							
								gridLines [currentGridCell.gridLineIndex].RemoveElements (currentGridCell.index);
								previousGridCell = currentGridCell;
								Debug.Log ("Remove some Elements from the Line Path of index " + currentGridCell.gridLineIndex);
								///Increase the movements counter
								IncreaseMovements ();
								return;//skip next
						} else {
								Debug.Log ("Clear the Line Path of index " + currentGridCell.gridLineIndex);
								gridLines [currentGridCell.gridLineIndex].ClearPath ();
						}
				}

				///If the current grid cell is not empty or it's not a partner of the previous grid cell
				if (!currentGridCell.isEmpty && currentGridCell.index != previousGridCell.tragetIndex) {
						Debug.Log ("Current grid cell of index " + currentGridCell.index + " is Ignored [Reason : Not the wanted Traget]");
						return;//skip next
				}

				///Increase the movements counter
				IncreaseMovements ();

				///Setting up the attributes for the current grid cell
				currentGridCell.currentlyUsed = true;
				currentGridCell.gridLineIndex = previousGridCell.gridLineIndex;
				if (currentGridCell.gridLineIndex == -1) {
						return;
				}
				if (currentGridCell.isEmpty)
						currentGridCell.tragetIndex = previousGridCell.tragetIndex;

				///Link the color of top background of the current grid cell with the top background color of the previous grid cell
				currentGridCell.topBackgroundColor = previousGridCell.topBackgroundColor;

				Debug.Log ("New GridCell of Index " + currentGridCell.index + " added to the Line Path of index " + currentLine.index);

				///Add the current grid cell index to the current traced grid cells list
				currentLine.path.Add (currentGridCell.index);

				///Determine the New Line Point
				tempPoint = currentGridCell.transform.position;
				tempPoint.z = gridLineZPosition;

				///Add the position of the New Line Point to the current line
				gridLines [currentGridCell.gridLineIndex].AddPoint (tempPoint);

				bool playBubble = true;
				if (!currentGridCell.isEmpty) {
						//Two pairs connected
						if (previousGridCell.tragetIndex == currentGridCell.index) {
								Debug.Log ("Two GridCells connected [GridCell " + (gridLines [currentGridCell.gridLineIndex].GetFirstPathElement ()) + " with GridCell " + (gridLines [currentGridCell.gridLineIndex].GetLastPathElement ()) + "]");
								currentLine.completedLine = true;
								GridCell gridCell = null;
									for (int i = 0; i < currentLine.path.Count; i++) {
										gridCell = gridCells [currentLine.path [i]];

										if(i == 0 || i == currentLine.path.Count-1){
											//Setting up the connect pairs
											GameObjectUtil.FindChildByTag(gridCell.transform,"GridCellContent").GetComponent<SpriteRenderer>().sprite = currentLevel.dotsPairs[gridCell.elementPairIndex].connectSprite;
										}
										///Setting up the color of the top background of the grid cell
										tempColor = previousGridCell.topBackgroundColor;
										tempColor.a = gridCellTopBackgroundAlpha;
										tempSpriteRendererd = gridCell.transform.Find ("background").GetComponent<SpriteRenderer> ();
										tempSpriteRendererd.color = tempColor;
										///Enable the top backgroud of the grid cell
										tempSpriteRendererd.enabled = true;
								}

								///Play the connected sound effect at the center of the unity world
								AudioSource.PlayClipAtPoint (connectedSFX, Vector3.zero, effectsAudioSource.volume);
								playBubble = false;
								Release (null);
								CheckLevelComplete ();
								return;
						}
				}
				if (playBubble) {
						///Play the water buttle sound effect at the center of the unity world
						AudioSource.PlayClipAtPoint (waterBubbleSFX, Vector3.zero, effectsAudioSource.volume);
				}
				previousGridCell = currentGridCell;
		}

		/// <summary>
		/// Refreshs(Reset) the grid.
		/// </summary>
		public void RefreshGrid ()
		{
				movements = 0;
				if (movementsText != null)
						movementsText.text = "Movements : " + movements;
				timer.Stop ();

				if (gridLines != null) {
						for (int i = 0; i <gridLines.Length; i++) {
								if (gridLines [i].completedLine)
										gridLines [i].ClearPath ();
						}
				}
				currentLine = null;
				currentGridCell = previousGridCell = null;
				timer.Start ();
		}

		/// <summary>
		/// Draw(Drag) the dragging element.
		/// </summary>
		/// <param name="clickPosition">Click position.</param>
		private void DrawDraggingElement (Vector3 clickPosition)
		{
				if (draggingElement == null) {
						return;
				}

				tempClickPosition = Camera.main.ScreenToWorldPoint (clickPosition);
				tempClickPosition.z = draggingElementZPosition;
				draggingElement.transform.position = tempClickPosition;
		}

		/// <summary>
		/// Create a new level.
		/// </summary>
		private void CreateNewLevel ()
		{
				try {
						movements = 0;
						movementsText.text = "Movements : " + movements;
	
						levelText.text = "Level " + TableLevel.wantedLevel.ID;
						ResetGameContents ();
						BuildTheGrid ();
						SettingUpPairs ();
						SettingUpNextBackAlpha ();
						timer.Stop ();
						timer.Start ();
						isRunning = true;
				} catch (Exception ex) {
					Debug.Log("Make sure you selected a level");
				}
		}

		/// <summary>
		/// Build the grid.
		/// </summary>
		private void BuildTheGrid ()
		{
				Debug.Log ("Building the Grid " + numberOfRows + "x" + numberOfColumns);
			
				Vector3 gridCellScale = new Vector3 (gridSize.x / numberOfColumns, gridSize.y / numberOfRows, 1);
				Vector3 gridCellPosition = Vector2.zero;
				Vector3 gridPosition = gridTopPivotTransform.position;
				gridCells = new GridCell[numberOfRows * numberOfColumns];
				GridCell gridCellComponent;
				GameObject gridCell = null;
				int gridCellIndex;
		
				for (int i = 0; i < numberOfRows; i++) {
						for (int j = 0; j < numberOfColumns; j++) {
								gridCellIndex = i * numberOfColumns + j;
								gridCellPosition.x = gridPosition.x + gridCellScale.x * j + gridCellScale.x / 2;
								gridCellPosition.y = gridPosition.y - gridCellScale.y * i - gridCellScale.x / 2;
								gridCellPosition.z = gridCellZPosition;
								gridCell = Instantiate (gridCellPrefab, gridCellPosition, Quaternion.identity) as GameObject;
								//Set the color for each grid cell
								//gridCell.GetComponent<SpriteRenderer> ().color = Mission.wantedMission.missionColor;
								gridCellComponent = gridCell.GetComponent<GridCell> ();
								gridCellComponent.index = gridCellIndex;
								///Define the adjacents of the grid cell
								gridCellComponent.DefineAdjacents (i, j);
								gridCell.transform.localScale = gridCellScale;
								gridCell.transform.parent = gridContentsTransform;
								gridCell.name = "GridCell-" + (gridCellIndex + 1);
								gridCells [gridCellIndex] = gridCell.GetComponent<GridCell> ();
						}
				}
		}

		/// <summary>
		/// Setting up the cells pairs.
		/// </summary>
		private void SettingUpPairs ()
		{
				Debug.Log ("Setting up the Cells Pairs");
	
				 currentLevel = Mission.wantedMission.levelsManagerComponent.levels [TableLevel.wantedLevel.ID - 1];

				if (currentLevel == null) {
						Debug.Log ("level is undefined");
						return;
				}

				TextMesh numberTextmesh;
				Color numberColor = Color.white;
				Level.DotsPair elementsPair = null;
				Transform gridCellTransform;
				GridCell gridCell;
				Vector3 cellContentPosition = new Vector3 (0, 0, cellContentZPosition);
				Vector3 gridCellScale = Vector3.zero;
				GameObject firstElement = null;
				GameObject secondElement = null;
				float cellContentScale = 0;
		
				gridLines = new Line[currentLevel.dotsPairs.Count];
		
				for (int i = 0; i <currentLevel.dotsPairs.Count; i++) {
			
						elementsPair = currentLevel.dotsPairs [i];
						numberColor = new Color (1 - elementsPair.color.r, 1 - elementsPair.color.g, 1 - elementsPair.color.b, 1);//opposite color
			
						//Setting up the First Dot(Element)
						gridCell = gridCells [elementsPair.firstDot.index];
						gridCell.gridLineIndex = i;
						gridCell.elementPairIndex = i;
						gridCell.topBackgroundColor = elementsPair.lineColor;
						gridCell.isEmpty = false;
						gridCell.tragetIndex = elementsPair.secondDot.index;
			
						gridCellTransform = gridCell.gameObject.transform;
						gridCellScale = gridCellTransform.localScale;
						cellContentScale = (Mathf.Max (gridCellScale.x, gridCellScale.y) / Mathf.Min (gridCellScale.x, gridCellScale.y)) * cellContentScaleFactor;
			
						firstElement = Instantiate (cellContentPrefab) as GameObject;
						firstElement.transform.SetParent (gridCellTransform);
						firstElement.transform.localPosition = cellContentPosition;
						firstElement.transform.localScale = new Vector3 (cellContentScale, cellContentScale, cellContentScale);
						firstElement.name = "Pair" + (i + 1) + "-FirstElement";
						firstElement.GetComponent<SpriteRenderer> ().sprite = elementsPair.sprite;
			
						if (elementsPair.applyColorOnSprite) {
								firstElement.GetComponent<SpriteRenderer> ().color = elementsPair.color;//apply the sprite color
						} else {
								firstElement.GetComponent<SpriteRenderer> ().color = Color.white;//apply the white color
						}

						numberTextmesh = firstElement.transform.Find ("Number").GetComponent<TextMesh> ();
						if(currentLevel.showPairsNumber){
							numberTextmesh.text = (i + 1).ToString ();
							numberTextmesh.color = numberColor; 
						}else{
							numberTextmesh.text = "";//empty value
						}

						//Setting up the Second Dot(Element)
						gridCell = gridCells [elementsPair.secondDot.index];
						gridCell.gridLineIndex = i;
						gridCell.elementPairIndex = i;
						gridCell.topBackgroundColor = elementsPair.lineColor;
						gridCell.isEmpty = false;
						gridCell.tragetIndex = elementsPair.firstDot.index;
			
						gridCellTransform = gridCell.gameObject.transform;
						gridCellScale = gridCellTransform.localScale;
						cellContentScale = (Mathf.Max (gridCellScale.x, gridCellScale.y) / Mathf.Min (gridCellScale.x, gridCellScale.y)) * cellContentScaleFactor;
			
						secondElement = Instantiate (cellContentPrefab) as GameObject;
						secondElement.transform.parent = gridCellTransform;
						secondElement.transform.localPosition = cellContentPosition;
						secondElement.transform.localScale = new Vector3 (cellContentScale, cellContentScale, cellContentScale);
						secondElement.name = "Pair" + (i + 1) + "-SecondElement";
						secondElement.GetComponent<SpriteRenderer> ().sprite = elementsPair.sprite;

						if (elementsPair.applyColorOnSprite) {
								secondElement.GetComponent<SpriteRenderer> ().color = elementsPair.color;//apply the sprite color
						} else {
								secondElement.GetComponent<SpriteRenderer> ().color = Color.white;//apply the white color
						}

						numberTextmesh = secondElement.transform.Find ("Number").GetComponent<TextMesh> ();
						if(currentLevel.showPairsNumber){
							numberTextmesh.text = (i + 1).ToString ();
							numberTextmesh.color = numberColor; 
						}else{
							numberTextmesh.text = "";//empty value
						}

						///Create Grid Line
						CreateGridLine (cellContentScale * gridLineWidthFactor, elementsPair.lineColor, "Line " + elementsPair.firstDot.index + "-" + elementsPair.secondDot.index, i);
				}
		
				Color tempColor = Mission.wantedMission.missionColor;
				tempColor.a = draggingElementAlpha;
		
				CreateDraggingElement (tempColor, new Vector3 (cellContentScale * draggingElementScaleFactor, cellContentScale * draggingElementScaleFactor, cellContentScale));
		}

		/// <summary>
		/// Setting up Grid Lines
		/// </summary>
		/// <param name="lineWidth">Line width.</param>
		/// <param name="lineColor">Line color.</param>
		/// <param name="name">Name of grid line.</param>
		/// <param name="index">Index.</param>
		private void CreateGridLine (float lineWidth, Color lineColor, string name, int index)
		{
				GameObject gridLine = Instantiate (gridLinePrefab, grid.transform.position, Quaternion.identity) as GameObject;
				gridLine.transform.parent = gridLinesTransfrom;
				gridLine.name = name;
				Line line = gridLine.GetComponent<Line> ();
				line.SetWidth (lineWidth);
				line.SetColor (lineColor);
				if (gridLines != null) {
						gridLines [index] = line;
						gridLines [index].index = index;
				}
		}

		/// <summary>
		/// Creates the dragging element.
		/// </summary>
		/// <param name="color">Color of the dragging element.</param>
		/// <param name="scale">Scale of the dragging element.</param>
		private void CreateDraggingElement (Color color, Vector3 scale)
		{
				GameObject currentDraggingElement = GameObject.Find ("DraggingElement");
				if (draggingElement == null) {
						draggingElement = Instantiate (draggingElementPrefab) as GameObject;
						draggingElement.transform.parent = GameObject.Find ("GameScene").transform;
						draggingElement.name = "DraggingElement";
						draggingElement.transform.Find ("ColorsEffect").GetComponent<ParticleEmitter>().emit = false;
				} else {
						draggingElement = currentDraggingElement;
						draggingElement.transform.Find ("ColorsEffect").GetComponent<ParticleEmitter>().emit = false;
				}
		
				draggingElement.transform.localScale = scale;
				draggingElementSpriteRenderer = draggingElement.GetComponent<SpriteRenderer> ();
				draggingElementSpriteRenderer.color = color;
				draggingElementSpriteRenderer.enabled = false;
		}

		/// <summary>
		/// Go to the next level.
		/// </summary>
		public void NextLevel ()
		{
				if (LevelsTable.currentLevelID >= 1 && LevelsTable.currentLevelID < LevelsTable.tableLevels.Count) {
						///Get the next level and check if it's locked , then do not load the next level
						DataManager.MissionData currentMissionData = DataManager.FindMissionDataById (Mission.wantedMission.ID, DataManager.filterdMissionsData);//Get the current mission
						DataManager.LevelData currentLevelData = currentMissionData.FindLevelDataById (TableLevel.wantedLevel.ID);///Get the current level
						if (currentLevelData .ID + 1 < currentMissionData.levelsData.Count) {
								DataManager.LevelData nextLevelData = currentMissionData.FindLevelDataById (TableLevel.wantedLevel.ID + 1);///Get the next level
								if (nextLevelData.isLocked) {
										///Play lock sound effectd
										if (levelLockedSFX != null) {
												AudioSource.PlayClipAtPoint (levelLockedSFX, Vector3.zero, effectsAudioSource.volume);
										}
										///Skip the next
										return;
								}
						}
						LevelsTable.currentLevelID++;///Increase level ID
						TableLevel.wantedLevel = LevelsTable.tableLevels [LevelsTable.currentLevelID - 1];///Set the wanted level
						CreateNewLevel ();///Create new level
				} else {
						///Play lock sound effectd
						if (levelLockedSFX != null) {
								AudioSource.PlayClipAtPoint (levelLockedSFX, Vector3.zero, effectsAudioSource.volume);
						}
				}
		}

		//// <summary>
		/// Back to the previous level.
		/// </summary>
		public void PreviousLevel ()
		{
				if (LevelsTable.currentLevelID > 1 && LevelsTable.currentLevelID <= LevelsTable.tableLevels.Count) {
						LevelsTable.currentLevelID--;
						TableLevel.wantedLevel = LevelsTable.tableLevels [LevelsTable.currentLevelID - 1];
						CreateNewLevel ();
				} else {
						///Play lock sound effectd
						if (levelLockedSFX != null) {
								AudioSource.PlayClipAtPoint (levelLockedSFX, Vector3.zero, effectsAudioSource.volume);
						}
				}
		}

		/// <summary>
		/// Setting up alpha value for the next and back buttons.
		/// </summary>
		private  void SettingUpNextBackAlpha ()
		{
				if (TableLevel.wantedLevel.ID == 1) {
						tempColor = backButtonImage.color;
						tempColor.a = 0.5f;
						backButtonImage.color = tempColor;
						backButtonImage.GetComponent<Button> ().interactable = false;

						tempColor = nextButtonImage.color;
						tempColor.a = 1;
						nextButtonImage.color = tempColor;
						nextButtonImage.GetComponent<Button> ().interactable = true;
				} else if (TableLevel.wantedLevel.ID == LevelsTable.tableLevels.Count) {
						tempColor = backButtonImage.color;
						tempColor.a = 1;
						backButtonImage.color = tempColor;
						backButtonImage.GetComponent<Button> ().interactable = true;

						tempColor = nextButtonImage.color;
						tempColor.a = 0.5f;
						nextButtonImage.color = tempColor;
						nextButtonImage.GetComponent<Button> ().interactable = false;
				} else {
						tempColor = backButtonImage.color;
						tempColor.a = 1;
						backButtonImage.color = tempColor;
						backButtonImage.GetComponent<Button> ().interactable = true;

						tempColor = nextButtonImage.color;
						tempColor.a = 1;
						nextButtonImage.color = tempColor;
						nextButtonImage.GetComponent<Button> ().interactable = true;
				}
		}

		/// <summary>
		/// Resets the game contents.
		/// </summary>
		private void ResetGameContents ()
		{
				GameObject [] gridCells = GameObject.FindGameObjectsWithTag ("GridCell");
				foreach (GameObject gridCell in gridCells) {
						Destroy (gridCell);
				}

				GameObject [] gridLines = GameObject.FindGameObjectsWithTag ("GridLine");
				foreach (GameObject gridLine in gridLines) {
						Destroy (gridLine);
				}
		}

		/// <summary>
		/// Checks Wheter the level is completed.
		/// </summary>
		private void CheckLevelComplete ()
		{
				if (gridLines == null) {
						return;
				}

				bool isLevelComplete = true;


				for (int i = 0; i < gridLines.Length; i++) {
						if (!gridLines [i].completedLine) {
								isLevelComplete = false;
								break;
						}
				}

				if (isLevelComplete) {
						timer.Stop ();
						isRunning = false;

						try {
								///Save the stars level
								DataManager.MissionData currentMissionData = DataManager.FindMissionDataById (Mission.wantedMission.ID, DataManager.filterdMissionsData);
								DataManager.LevelData currentLevelData = currentMissionData.FindLevelDataById (TableLevel.wantedLevel.ID);
								currentLevelData.starsLevel = StarsRating.GetLevelStarsRating (Timer.timeInSeconds, GameManager.movements, gridCells.Length);
								if (currentLevelData .ID + 1 <= currentMissionData.levelsData.Count) {
										///Unlock the next level
										DataManager.LevelData nextLevelData = currentMissionData.FindLevelDataById (TableLevel.wantedLevel.ID + 1);
										nextLevelData.isLocked = false;
								}
								DataManager.SaveMissionsDataToFile (DataManager.filterdMissionsData);
						} catch (Exception ex) {
								Debug.Log (ex.Message);
						}

						///Show the black area
						BlackArea.Show ();
						///Show the awesome dialog
						GameObject.FindObjectOfType<AwesomeDialog> ().Show ();
						Debug.Log ("You completed level " + TableLevel.wantedLevel.ID);
				}
		}

		/// <summary>
		/// Increase the movements counter.
		/// </summary>
		private void IncreaseMovements ()
		{
				movements++;
				movementsText.text = "Movements : " + movements;
		}

		public enum ClickType
		{
				Began,
				Moved,
				Ended
		}
}