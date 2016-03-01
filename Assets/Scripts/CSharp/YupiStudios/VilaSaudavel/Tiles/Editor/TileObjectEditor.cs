using UnityEngine;
using UnityEditor;
using System.Collections;
using YupiStudios.VilaSaudavel.Tiles;

namespace YupiStudios.VilaSaudavel.Tiles {

	[CustomEditor(typeof(TileObject))]
	public class TileObjectEditor : Editor {

		TileObject obj;

		Vector2 _lastTile;
		Vector3 _lastPos;
		
		public void OnEnable(){

			obj = target as TileObject;
			
		}

		public override void OnInspectorGUI()
		{

			DrawDefaultInspector ();

			if (! Application.isPlaying ) {

				if (obj.WorldMap != null) {

					Vector3 currentPos = obj.transform.position;

					if (_lastTile != obj._tilePosition) {

						obj.TilePosition = obj._tilePosition;
						_lastTile = obj._tilePosition;
						_lastPos = obj.transform.position;

						EditorUtility.SetDirty(target);

					} else if (_lastPos != currentPos) {
				
						Vector2 pos = TileUtils.WorldPosToTile (obj.WorldMap, currentPos);
						obj.TilePosition = pos;
						_lastTile = obj._tilePosition;
						_lastPos = obj.transform.position;

						EditorUtility.SetDirty(target);
					}

				}

			}



		}

	}

}
