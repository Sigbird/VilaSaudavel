using UnityEngine;
using System.Collections;
using YupiStudios.VilaSaudavel.Tiles.TileMap;

namespace YupiStudios.VilaSaudavel.Tiles {

	public class TileObjectFactory : MonoBehaviour {



		public TileMapInputController tileInputController;
		
		public TileUtils.MatSettings MovingMaterialSettings;

		public TileUtils.MatSettings SelectedMaterialSettings;

		public GameObject obj;

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
				//Debug.Log("teste");
				movingObject.FinishPlacement ();
				movingObject = null;
			}


		}

		public void RotateSprite(){
			if (obj.GetComponent<TileObject>().spriterot< 3 && obj.GetComponent<TileObject>().CurrentState == TileObject.ETileObjectState.Moving) {
				obj.GetComponent<TileObject>().spriterot ++;
				obj.GetComponent<TileObject>().Sprite.sprite = obj.GetComponent<TileObject>().SpriteRotation [obj.GetComponent<TileObject>().spriterot];
			} else if(obj.GetComponent<TileObject>().CurrentState == TileObject.ETileObjectState.Moving) {
				obj.GetComponent<TileObject>().spriterot = 0;
				obj.GetComponent<TileObject>().Sprite.sprite = obj.GetComponent<TileObject>().SpriteRotation [obj.GetComponent<TileObject>().spriterot];
			}
			
		}

		public void TryInstantiateObject(int objectIndex)
		{
			Vector2 position = tileInputController.GetHighlightPosition ();

			if (Vector3.Distance(position,new Vector2 (-1,-1)) <=1 ) {
				position = new Vector2 (31,30);
			}

			//Debug.Log (position);

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


				obj = (Instantiate(tileObjects[objectIndex]) as Transform).gameObject;
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
