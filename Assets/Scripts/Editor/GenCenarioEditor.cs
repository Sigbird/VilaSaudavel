using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(GenCenario))]
public class GenCenarioEditor : Editor {

	public override void OnInspectorGUI (){
		DrawDefaultInspector ();

		GenCenario gc = (GenCenario)target;

		if (GUILayout.Button ("Create")) {
			gc.Clear();
			gc.Create();
		}
		if (GUILayout.Button ("Clear")) {
			gc.Clear();
		}

	}

}
