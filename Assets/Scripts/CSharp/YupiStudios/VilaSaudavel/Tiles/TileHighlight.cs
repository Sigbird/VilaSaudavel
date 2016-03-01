using UnityEngine;
using System.Collections;
using YupiStudios.VilaSaudavel.Tiles.TileMap;

namespace YupiStudios.VilaSaudavel.Tiles {

	[RequireComponent(typeof(TileObjectMesh))]
	public class TileHighlight : MonoBehaviour {


		public TileUtils.MatSettings MaterialSettings;
		public TileMapData Data;

		public Vector2 _tilePos;
		public Vector2 TilePos
		{
			get {
				return _tilePos;
			}
			set {
				_tilePos = value;
				transform.position = TileUtils.TileToWorldPos(Data,_tilePos);
				mesh.SetMapOriginalStep ((int)TilePos.x, (int)TilePos.y);
				mesh.RefreshUVs ();
			}
		}

		private TileObjectMesh mesh;

		// Use this for initialization
		private void Initialize () {

			if (Data == null)
				Data = TileUtils.WorldMap;

			mesh = GetComponent<TileObjectMesh> (); // RequireComponent

			/* CreateTileMesh */
			mesh.SetTileMapData (Data);
			mesh.CreateObject (Vector3.zero, 1, 1, 0, 0, MaterialSettings);
		}

		void OnEnable() 
		{
			if (mesh == null) {
				Initialize ();
			}
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}

}