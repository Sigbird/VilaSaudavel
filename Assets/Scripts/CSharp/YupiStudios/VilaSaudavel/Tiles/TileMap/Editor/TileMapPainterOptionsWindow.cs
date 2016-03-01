using UnityEngine;
using UnityEditor;
 	
using System.Collections;

namespace YupiStudios.VilaSaudavel.Tiles.TileMap {

	public class TileMapPainterOptionsWindow : EditorWindow {

		private const int TileShowSize = 60;
		private const int GUILayoutWidth = 180;

		private TileMapPainter _tileMapPainter;

		private Vector2 tileScrollPos = new Vector2(0,0);

		private Texture2D []_tiles;

		private TileMapPainter TileMapPainter
		{
			get {

				if (_tileMapPainter == null)
					_tileMapPainter = (TileMapPainter) GameObject.FindObjectOfType<TileMapPainter>();

				return _tileMapPainter;
			}
		}

		private Texture2D[] Tiles 
		{
			get {
				if (_tiles == null || _tiles [0] == null)
					_tiles = GetTiles();

				return _tiles;
			}
		}

		public void SetPainter(TileMapPainter tileMapPainter)
		{
			this._tileMapPainter = tileMapPainter;
		}
		
		public void Init()
		{
			if (this.TileMapPainter == null) {
				Close();
			}

			TileMapPainter.IsDrawing = true;
		}


		public void OnDestroy()
		{
			TileMapPainter.IsDrawing = false;
		}

		private void ChangeTab(int newSelection)
		{
			if (newSelection != TileMapPainter.SelectedTab) {
				TileMapPainter.SelectedTab = newSelection;
				switch(TileMapPainter.SelectedTab)
				{
				case 1:
					//SceneView.currentDrawingSceneView.renderMode = DrawCameraMode.Wireframe;
					break;
				default:
					//SceneView.currentDrawingSceneView.renderMode = DrawCameraMode.Normal;
					break;
				}

				//SceneView.RepaintAll();
			}
		}

		private Color [] Resize (int fromWidth, int  fromHeight,int  newWidth, int  newHeight, Color[] original)
		{
			Color [] newColors = new Color[newWidth * newHeight];

			float ratioX = (float)fromWidth / newWidth;
			float ratioY = (float)fromHeight / newHeight;
			for (int y = 0; y < newHeight; y++) {
				int orig_y = (int)(y*ratioY)*fromWidth;
				for (int x = 0; x < newWidth; x++) {
					newColors[y*newWidth + x] = original[orig_y + (int)(ratioX*x)];
				}
			}

			return newColors;
		}

		private Texture2D [] GetTiles()
		{
			int totTextures = TileMapPainter.Data.MaterialSettings.TilesetSizeX * TileMapPainter.Data.MaterialSettings.TilesetSizeY;
			Texture2D [] textures = new Texture2D[totTextures];

			int tWidth = TileMapPainter.Data.MaterialSettings.TileMaterial.mainTexture.width / TileMapPainter.Data.MaterialSettings.TilesetSizeX;
			int tHeight = TileMapPainter.Data.MaterialSettings.TileMaterial.mainTexture.height / TileMapPainter.Data.MaterialSettings.TilesetSizeY;

			Texture2D orig = (Texture2D)TileMapPainter.Data.MaterialSettings.TileMaterial.mainTexture;

			for (int i = 0; i < TileMapPainter.Data.MaterialSettings.TilesetSizeY; ++i) {
				for (int j = 0; j < TileMapPainter.Data.MaterialSettings.TilesetSizeX; ++j) {
					Texture2D t = new Texture2D(TileShowSize, TileShowSize);

					Color[] pix = orig.GetPixels(j*tWidth, (i)*tHeight, tWidth, tHeight);
					Color[] pixResized = Resize(tWidth, tHeight, TileShowSize, TileShowSize, pix);
					t.SetPixels(pixResized);
					t.Apply();

					textures [i*TileMapPainter.Data.MaterialSettings.TilesetSizeX+j] = t;
				}
			}

			return textures;
		}



		private void PaintUVGUI()
		{
			GUILayoutOption [] options = { GUILayout.Width (GUILayoutWidth) };
			EditorGUILayout.BeginVertical ();

			GUILayout.BeginHorizontal();

			GUILayout.Label("Checked Pattern",options);
			TileMapPainter.CheckedPattern = GUILayout.Toggle(TileMapPainter.CheckedPattern, "", options);
			GUILayout.EndHorizontal();
			
			GUILayout.BeginHorizontal();
			GUILayout.Label(" Tile Value ", options);
			GUILayout.Label(TileMapPainter.Tilenum.ToString(), options);
			GUILayout.EndHorizontal();

			Texture2D []txs = Tiles;
			tileScrollPos = GUILayout.BeginScrollView (tileScrollPos,false,true);
			TileMapPainter.Tilenum = GUILayout.SelectionGrid (TileMapPainter.Tilenum, txs, 3);
			GUILayout.EndScrollView ();
			
			EditorGUILayout.EndVertical ();
		}

		private void PaintTypeGUI()
		{
			GUILayoutOption [] options = { GUILayout.Width (GUILayoutWidth) };
			EditorGUILayout.BeginVertical ();
			
			GUILayout.BeginHorizontal();		
			GUILayout.Label("Tyle Type",options);
			TileMapPainter.SelectedTyleType = (TileInfo.ETileType) EditorGUILayout.EnumPopup ("", TileMapPainter.SelectedTyleType, options);
			GUILayout.EndHorizontal();
			
			EditorGUILayout.EndVertical ();
		}

		void OnGUI()
		{
			if (this.TileMapPainter == null) {
				Close();
			}

			GUILayoutOption [] options = { GUILayout.Width (GUILayoutWidth) };

			TileMapPainter.SelectedTab = GUILayout.Toolbar (TileMapPainter.SelectedTab, new string[] {"PaintUV", "PaintType"});

			EditorGUILayout.Separator ();

			GUILayout.BeginHorizontal();		
			GUILayout.Label("Draw Style",options);
			TileMapPainter.DrawStyle = (TileMapPainter.EDrawStyle) EditorGUILayout.EnumPopup ("", TileMapPainter.DrawStyle, options);
			GUILayout.EndHorizontal();

			if (TileMapPainter.SelectedTab == 0) {

				PaintUVGUI ();

			} else if (TileMapPainter.SelectedTab == 1) {

				PaintTypeGUI ();
			}

			EditorGUILayout.Separator ();

			if (GUILayout.Button ("Save")) {
				TileMapPainter.Data.SaveToDisk();
				AssetDatabase.Refresh();
			}

			if (GUILayout.Button ("Load")) {
				TileMapPainter.Data.LoadStage();
				AssetDatabase.Refresh();
			}
		}

		void Start()
		{
		}

		void Update()
		{
		}
	}

}
