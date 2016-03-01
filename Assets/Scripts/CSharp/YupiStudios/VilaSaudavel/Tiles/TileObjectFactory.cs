using UnityEngine;
using System.Collections;
using YupiStudios.VilaSaudavel.Tiles.TileMap;

namespace YupiStudios.VilaSaudavel.Tiles {

	public class TileObjectFactory : MonoBehaviour {

		public TileMapInputController tileInputController;

		public TileUtils.MatSettings MovingMaterialSettings;

		public TileUtils.MatSettings SelectedMaterialSettings;

		public Transform [] tileObjects;

		private TileMapData TileMapData;
		private TileObject movingObject;

		public bool IsConstructing()
		{
			return movingObject != null;
		}

		public void FinishMoving()
		{
			if (movingObject != null) {
				movingObject.FinishPlacement ();
				movingObject = null;
			}
		}

		public void TryInstantiateObject(int objectIndex)
		{
			Vector2 position = tileInputController.GetHighlightPosition ();

			Debug.Log (position);

			if (position == TileUtils.TILE_NULL_POSITION) {
				return;
			}

			if (movingObject != null) {
				FinishMoving();
				return;
			}

			if (TileMapData == null || objectIndex < 0 || objectIndex >= tileObjects.Length)
				return;

			if (TileMapData.CurrentState == TileMapData.EWorldMapState.Idle) {

				GameObject obj = (Instantiate(tileObjects[objectIndex]) as Transform).gameObject;
				TileObject tileObject = obj.GetComponent<TileObject>();

				if (tileObject == null)
				{
					DestroyObject(obj);
				} else 
				{
					tileObject.Initialize(TileMapData, MovingMaterialSettings);

					tileObject.TryPosition(position);

					movingObject = tileObject;
				}

			}
		}

		// Use this for initialization
		void Start () {
			TileMapData = TileUtils.WorldMap;
		}
		
		// Update is called once per frame
		void Update () {
		
		}
	}

}
