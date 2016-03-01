using UnityEngine;
using System.Collections;

namespace YupiStudios.VilaSaudavel.Tiles.TileMap {


	[ExecuteInEditMode]
	[RequireComponent(typeof(TileMapData))]
	public class TileMapPainter : MonoBehaviour {

		public enum EDrawStyle
		{
			Point,
			Fill,
			HLine,
			VLine,
			DDownward,
			DUpward
		}

		public bool IsDrawing { get; set;	}


		/// <summary>
		/// TileMapPainter Option Window
		/// </summary>
		public int Tilenum;
		public Vector2 TilePos;
		public bool CheckedPattern;	
		public EDrawStyle DrawStyle;	
		public TileInfo.ETileType SelectedTyleType;
		public int SelectedTab;


		public TileMapData Data;

		private bool IsValidPos(Vector2 mousePos)
		{
			return  (
						((int)mousePos.x >= 0 && (int)mousePos.x < Data.WorldSettings.WorldSizeX) &&
						((int)mousePos.y >= 0 && (int)mousePos.y < Data.WorldSettings.WorldSizeY)
					);

		}

		
		
		public void PaintTile(int x, int y)
		{
			switch (SelectedTab) {
			case 1: // Types
				Data.TileMap[y, x].Type = SelectedTyleType;
				break;
			default: // Tiles
				Data.TileMap[y, x].TileIndex = CheckedPattern ? Tilenum + (x + y) % 2 : Tilenum;
				break;
			}
		}

		private void DrawPoint ()
		{
			if (Input.GetMouseButton (0)) {
				Vector2 mousePos = TileUtils.GetMouseTilePos(Data);
				if ( IsValidPos(mousePos) )
				{
					int x = (int)mousePos.x;
					int y = (int)mousePos.y;
					PaintTile(x,y);
					Data.RedoUVs();
				}
			}
		}

		private void DrawType ()
		{
			if (Input.GetMouseButton (0)) {
				Vector2 mousePos = TileUtils.GetMouseTilePos(Data);
				if ( IsValidPos(mousePos) )
				{
					int x = (int)mousePos.x;
					int y = (int)mousePos.y;
					PaintTile(x,y);
					Data.RedoUVs();
				}
			}
		}

		private void DrawFill ()
		{
			if (Input.GetMouseButtonDown (0)) {
				Vector2 mousePos = TileUtils.GetMouseTilePos(Data);
				if ( IsValidPos(mousePos) )
				{
					for (int i = 0; i < Data.WorldSettings.WorldSizeY; ++i)
						for (int j = 0; j < Data.WorldSettings.WorldSizeX; ++j)
							PaintTile(j,i);
					Data.RedoUVs();
				}
			}
		}

		private void DrawHLine ()
		{
			if (Input.GetMouseButtonDown (0)) {
				Vector2 mousePos = TileUtils.GetMouseTilePos(Data);
				if ( IsValidPos(mousePos) )
				{
					int y = (int)mousePos.y;
					for (int j = 0; j < Data.WorldSettings.WorldSizeX; ++j)
						PaintTile(j,y);
					Data.RedoUVs();
				}
			}
		}

		private void DrawVLine ()
		{
			if (Input.GetMouseButtonDown (0)) {
				Vector2 mousePos = TileUtils.GetMouseTilePos(Data);
				if ( IsValidPos(mousePos) )
				{
					int x = (int)mousePos.x;
					for (int i = 0; i < Data.WorldSettings.WorldSizeY; ++i)
						PaintTile(x,i);
					Data.RedoUVs();
				}
			}
		}

		private void DrawDiagonalDownward()
		{
			if (Input.GetMouseButtonDown (0)) {
				Vector2 mousePos = TileUtils.GetMouseTilePos(Data);
				if ( IsValidPos(mousePos) )
				{
					int x = (int)mousePos.x;
					int y = (int)mousePos.y;

					int xLine = x, yLine = y;
					while (xLine >=0 && yLine >= 0)
					{
						PaintTile(xLine,yLine);
						xLine--;
						yLine--;
					}

					xLine = x+1;
					yLine = y+1;
					while (xLine < Data.WorldSettings.WorldSizeX && yLine < Data.WorldSettings.WorldSizeY)
					{
						PaintTile(xLine,yLine);
						xLine++;
						yLine++;
					}

					Data.RedoUVs();
				}
			}
		}

		private void DrawDiagonalUpward ()
		{
			if (Input.GetMouseButtonDown (0)) {
				Vector2 mousePos = TileUtils.GetMouseTilePos(Data);
				if ( IsValidPos(mousePos) )
				{
					int x = (int)mousePos.x;
					int y = (int)mousePos.y;
					
					int xLine = x, yLine = y;
					while (xLine < Data.WorldSettings.WorldSizeX && yLine >= 0)
					{
						PaintTile(xLine,yLine);
						xLine++;
						yLine--;
					}
					
					xLine = x-1;
					yLine = y+1;
					while (xLine >= 0 && yLine < Data.WorldSettings.WorldSizeY)
					{
						PaintTile(xLine,yLine);
						xLine--;
						yLine++;
					}
					
					Data.RedoUVs();
				}
			}
		}

		// Use this for initialization
		void Start () {
			Data = GetComponent<TileMapData> ();
		}


		private void DrawTypes()
		{

			const int SIZE = 40;

			Vector2 v = TileUtils.GetTileInScreenPos ( Data, new Vector2 ( 0.5f * Screen.width, 0.5f * Screen.height ));

			int minX = ((int)(v.x - SIZE) > 0) ? (int) (v.x - SIZE) : 0;
			int maxX = ((int)(v.x + SIZE) < Data.WorldSettings.WorldSizeX) ? (int)(v.x + SIZE) : Data.WorldSettings.WorldSizeX-1;

			int minY = ((int)(v.y - SIZE) > 0) ? (int) (v.y - SIZE) : 0;
			int maxY = ((int)(v.y + SIZE) < Data.WorldSettings.WorldSizeY) ? (int) (v.y + SIZE) : Data.WorldSettings.WorldSizeY-1;


			///////////////////////
			/// get cell pixel size
			/// 
			Vector3 size;
			{ 
			
				Vector3 cell = TileUtils.TileToWorldPos (Data, minX, minY, true);
				Vector3 worldPos = new Vector3 (cell.x + Data.transform.position.x, 0, cell.z + Data.transform.position.z);
				Vector3 screenPos = Camera.main.WorldToScreenPoint (worldPos);		
				Vector3 cell2 = TileUtils.TileToWorldPos (Data, minX + 1, minY, true);
				Vector3 worldPos2 = new Vector3 (cell2.x + Data.transform.position.x, 0, cell2.z + Data.transform.position.z);
				Vector3 screenPos2 = Camera.main.WorldToScreenPoint (worldPos2);
				size = screenPos2 - screenPos;
			}

			for (int i = minY; i <= maxY; ++i) {
				for (int j = minX; j <= maxX; ++j) {

					Vector3 cell = TileUtils.TileToWorldPos(Data, j, i, true);
					Vector3 worldPos = new Vector3(cell.x+Data.transform.position.x,0,cell.z+Data.transform.position.z);
					Vector3 screenPos = Camera.main.WorldToScreenPoint (worldPos);

					string typeLabel = Data.TileMap[i,j].GetTileInfo();
					switch (typeLabel)
					{
					case "O":
					case "B":				
						GUI.color = Color.red;
						break;
					default:
						GUI.color = Color.green;
						break;
					}
					GUI.Label(new Rect( screenPos.x - size.x/8.0f, Screen.height - (screenPos.y + size.y/2.0f), 40, 40), typeLabel);
					GUI.color = Color.white;
				}				
			}
		}



		private void UpdateDraw()
		{
			switch (DrawStyle) {
			case EDrawStyle.Fill:
				DrawFill();
				break;
			case EDrawStyle.HLine:
				DrawHLine();
				break;
			case EDrawStyle.VLine:
				DrawVLine();
				break;
			case EDrawStyle.DDownward:
				DrawDiagonalDownward();
				break;
			case EDrawStyle.DUpward:
				DrawDiagonalUpward();
				break;
			default:
				DrawPoint();
				break;
			}
		}

		void OnGUI()
		{
			if (SelectedTab == 1)
				DrawTypes ();
		}
		
		// Update is called once per frame
		void Update () {
			UpdateDraw ();

		}
	}
}
