using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(TileProperties))]
public class TilePropertiesEditor : Editor {

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();

		TileProperties tileProp = (TileProperties)target;


		if (GUILayout.Button ("Change Tile")) {
			tileProp.ChangeTile();
		}
		if(GUILayout.Button("Add Tree")){
			tileProp.AddTree();
		}
		if (GUILayout.Button ("Remove Element")) {
			tileProp.RemoveElement();
		}

	}
}
