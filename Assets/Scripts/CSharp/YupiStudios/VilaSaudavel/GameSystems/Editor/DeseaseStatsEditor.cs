using UnityEditor;
using System.Collections;

using YupiStudios.VilaSaudavel.GameSystems;

[CustomEditor(typeof(DeseaseStats))]
public class DeseaseStatsEditor : Editor {

	private DeseaseStats deseaseStatistics;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		deseaseStatistics = target as DeseaseStats;

		// Ajuste das doencas permitidas na fase
		EditorGUILayout.BeginVertical ();

		if (deseaseStatistics.AllowDengue)
			deseaseStatistics.DenguePerSec = EditorGUILayout.FloatField ("Dengue per Secs", deseaseStatistics.DenguePerSec);

		if (deseaseStatistics.AllowFatness)
			deseaseStatistics.FatnessPerSec = EditorGUILayout.FloatField ("Fatness per Secs", deseaseStatistics.FatnessPerSec);

		EditorGUILayout.EndVertical ();


	}

}
