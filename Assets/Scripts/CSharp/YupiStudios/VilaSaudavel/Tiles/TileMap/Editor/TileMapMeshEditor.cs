using UnityEngine;
using UnityEditor;
using System.Collections;

namespace YupiStudios.VilaSaudavel.Tiles.TileMap {

	[CustomEditor(typeof(TileMapMesh))]
	public class TileMapMeshEditor : Editor {

		public override void OnInspectorGUI()
		{
			DrawDefaultInspector ();

			if (GUILayout.Button ("Refresh UV")) {
				((TileMapMesh) target).RefreshUVs();
			}
		}
	}


}