using UnityEngine;
using UnityEditor;
using System.Collections;

namespace YupiStudios.VilaSaudavel.Tiles.TileMap {

	[CustomEditor(typeof(TileMapPainter))]
	public class TileMapPainterEditor : Editor {

		private TileMapPainter _painter;
		private TileMapPainter Painter
		{
			get 
			{
				if (_painter == null)
				{
					_painter = ((TileMapPainter) target);
				}
				
				return _painter;
			}
		}

		public override void OnInspectorGUI ()
		{

			if (GUILayout.Button ("Painter Options")) {
				TileMapPainterOptionsWindow window = (TileMapPainterOptionsWindow) EditorWindow.GetWindow(typeof(TileMapPainterOptionsWindow));
				window.SetPainter(Painter);
				window.Init();
			}

		}
	}

}