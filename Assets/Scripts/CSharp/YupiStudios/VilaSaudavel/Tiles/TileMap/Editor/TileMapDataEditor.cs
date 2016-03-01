using UnityEngine;
using UnityEditor;
using System.Collections;
using YupiStudios.VilaSaudavel.Tiles.TileMap;

namespace YupiStudios.VilaSaudavel.Tiles.TileMap {

	[CustomEditor(typeof(TileMapData))]
	public class TileMapDataEditor : Editor {

		private TileMapData _data;
		private TileMapData Data
		{
			get 
			{
				if (_data == null)
				{
					_data = ((TileMapData) target);
				}

				return _data;
			}
		}



		public override void OnInspectorGUI()
		{

			DrawDefaultInspector ();  

			if (GUILayout.Button ("ReCreate")) {
				Data.CreateWorldMap();
			}



			/*if (GUILayout.Button ("Save")) {
				((TileMapData) target).SaveToDisk(1);
			}

			if (GUILayout.Button ("Load")) { 
				((TileMapData) target).LoadStage(1);
			}*/
		}
	}

}