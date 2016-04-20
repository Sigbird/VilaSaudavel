using UnityEngine;
using System.Collections;
using YupiStudios.VilaSaudavel.Tiles.TileMap;

namespace YupiStudios.VilaSaudavel.Tiles {

	public class TileObject : MonoBehaviour {



		/////////////////////////////////////////////////
		// Enumeração dos estados do tile object
		/////////////////////////////////////////////////
		[System.Serializable]
		public enum ETileObjectState
		{

			Building,

			/*
			 * O objeto esta posicionado no mapa
			 */
			Placed, // Is Placed on the map


			/*
			 * O objeto esta sendo movido pelo mapa
			 */
			Moving, // Is Being moved


			/*
			 * O objeto esta fixado no mapa (nao pode ser removido)
			 */
			Fixed // Can't Be Deleted / Moved
		}





		/////////////////////////////////////////////////
		// Objeto de Dados do mapa de tiles
		/////////////////////////////////////////////////
		public TileMapData WorldMap;

		public TileMapInputController controller;




		/////////////////////////////////////////////////
		// Property para alteraçao da posiçao do objeto
		/////////////////////////////////////////////////
		public Vector2 _tilePosition;
		public Vector2 TilePosition
		{
			get 
			{
				return _tilePosition;
			}
			set 
			{
				if (WorldMap.TileExists((int)value.x,(int)value.y))
				{
					UpdatePositionFromTile(value);
				}
			}
		}



		/////////////////////////////////////////////////
		// Tamanho em tiles do Objeto
		/////////////////////////////////////////////////
		public Vector2 ObjectTileSize;




		/////////////////////////////////////////////////
		// Estado atual do objeto (ver ETileObjectState)
		/////////////////////////////////////////////////
		public ETileObjectState CurrentState;





		/////////////////////////////////////////////////
		// Malha para renderizaçao no terreno 
		// (usado para mostrar tiles obstruidos, 
		// quando movendo o objeto)
		/////////////////////////////////////////////////
		public TileObjectMesh movingMesh;




		/////////////////////////////////////////////////
		// Malha para renderizaçao no terreno 
		// (quando selecionado)
		/////////////////////////////////////////////////
		public TileObjectMesh selectedMesh;





		/////////////////////////////////////////////////
		// GUI com setas de auxilio que e acionado 
		// quando movendo o objeto
		/////////////////////////////////////////////////
		public GameObject MovingGUI;



		/////////////////////////////////////////////////
		// GUI quando selecionado
		/////////////////////////////////////////////////
		public GameObject SelectionGUI;

		public YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo BuildingInfo;



		/////////////////////////////////////////////////
		// Sprite do objeto
		// Alterado para vermelhor quando movimentado para 
		// posicao invalida
		/////////////////////////////////////////////////
		public SpriteRenderer Sprite;

		///////////////////////////////////////////////////
		//Sprites de Rotaçao do Objeto
		///////////////////////////////////////////////////
		public Sprite[] SpriteRotation;
		public Sprite[] SpriteRotationUpgrade;
		public int spriterot;


		/////////////////////////////////////////////////
		// Animador do Objeto
		/////////////////////////////////////////////////
		public Animator Anim;
		public Animator Smoke;





		/////////////////////////////////////////////////
		// Colliders do GUI para auxilio de movimentaçao
		/////////////////////////////////////////////////
		public Collider ButtonLeft;
		public Collider ButtonRight;
		public Collider ButtonTop;
		public Collider ButtonBottom;





		/////////////////////////////////////////////////
		// Collider referente ao objeto central
		/////////////////////////////////////////////////
		public Collider Center;



		/////////////////////////////////////////////////
		/// Mesh Material Settings
		/////////////////////////////////////////////////
		public TileUtils.MatSettings TileMaterialSettings;


		// ultima localizaçao valida
		public Vector2 LastPlacedPosition;


		/////////////////////////////////////////////////
		// Variaveis auxiliares para a movimentaçao do
		// objeto
		/////////////////////////////////////////////////
		private Vector2 touchStartTile;
		private Vector2 moveStartTile;



		private bool wasInitialized = false;
		private bool corout = false;
		

		/// <summary>
		/// Refreshs the UVs.
		/// </summary>
		/// <param name="originalMapStep">Posicao do tile que o objeto se encontra</param>
		public void RefreshUVs(Vector2 originalMapStep)
		{
			movingMesh.SetMapOriginalStep ((int) originalMapStep.x, (int) originalMapStep.y);	
			movingMesh.RefreshUVs();
		}


		public void Upgrade(){
			Sprite.sprite = SpriteRotationUpgrade [spriterot];
			BuildingInfo.renda = 200;
			
		}

		/// <summary>
		/// Calcula a posicao do objeto com base no tile que se encontra
		/// </summary>
		/// <param name="value">Value.</param>
		public void UpdatePositionFromTile(Vector2 value)
		{
			if (WorldMap != null) {
				Vector2 intValue = value;
				intValue.x = (int)value.x;
				intValue.y = (int)value.y;
				transform.position = WorldMap.TileToWorldPos(intValue);
				_tilePosition = value;
				if (Application.isPlaying)
					RefreshUVs(value);
			}
		}


		public void CreateMeshes() 
		{
			if (selectedMesh != null) {
				selectedMesh.SetTileMapData (WorldMap);
				selectedMesh.CreateObject (new Vector3 (0, 0, 0), (int)ObjectTileSize.x, (int)ObjectTileSize.y, 0, 0, TileMaterialSettings, TileObjectMesh.ETileRenderType.RenderTile0);
			}

			if (movingMesh != null) {
				movingMesh.SetTileMapData (WorldMap);			
				movingMesh.CreateObject (new Vector3 (0, 0, 0), (int)ObjectTileSize.x, (int)ObjectTileSize.y, 0, 0, TileMaterialSettings);
			}
		}




		/// <summary>
		/// Initialize the specified WorldMap and MaterialSettings.
		/// </summary>
		/// <param name="WorldMap">World map.</param>
		/// <param name="MaterialSettings">Material settings.</param>
		public void Initialize(TileMapData WorldMap, TileUtils.MatSettings MaterialSettings)
		{
			this.WorldMap = WorldMap;
			this.TileMaterialSettings = MaterialSettings;
			wasInitialized = true;
			CreateMeshes();
		}





		/// <summary>
		/// Tries the position while moving.
		/// </summary>
		/// <param name="position">Position.</param>
		public void TryPosition(Vector2 position)
		{
			ChangeState (ETileObjectState.Moving);
			if (CurrentState == ETileObjectState.Moving)
				UpdateTryPosition (position);
		}


		public void TryMoveInX(int x)
		{
			Vector2 position = TilePosition;
			position.x += x;
			TryPosition (position);
		}

		public void TryMoveInY(int y)
		{
			Vector2 position = TilePosition;
			position.y += y;
			TryPosition (position);
		}

		public void TryMoveInDeltaPos(Vector2 v)
		{
			Vector2 position = TilePosition;
			position += v;
			TryPosition (position);
		}


		/// <summary>
		/// Gets the TileObjectMesh.
		/// </summary>
		/// <returns>The mesh.</returns>
		public TileObjectMesh GetMesh()
		{
			return movingMesh;
		}


		public Rect GetTileRect()
		{
			return new Rect (TilePosition.x, TilePosition.y, ObjectTileSize.x, ObjectTileSize.y);
		}
		

		/// <summary>
		/// Liga mesh de tiles para posicionamento.
		/// </summary>
		private void TurnObjectTiles(bool turnOn)
		{
			movingMesh.enabled = turnOn;
		}

		private void RotateSprite(){
			if (this.spriterot < 3 && CurrentState == ETileObjectState.Moving) {
				this.spriterot ++;
				Sprite.sprite = SpriteRotation [spriterot];
			} else if(CurrentState == ETileObjectState.Moving) {
				this.spriterot = 0;
				Sprite.sprite = SpriteRotation [spriterot];
			}
		
		}



		

		/// <summary>
		/// Muda posicao do objeto, e testa se pode ser posicionado.
		/// Sprite muda para vermelho caso nao possa ficar nesta posicao
		/// </summary>
		/// <param name="tile">Tile.</param>
		private void UpdateTryPosition(Vector2 tile)
		{
			TilePosition = tile;	

			tile = TilePosition; // Update New Tile Position

			if ( WorldMap.CanPlaceObject (GetTileRect()) ) {
				Sprite.color = Color.green;
			} else {
				Sprite.color = Color.red;
			}

		}



		/// <summary>
		/// Levanta objeto para ficar a frente dos outros.
		/// Util para que o objeto seja sempre visivel quando movendo
		/// </summary>
		private void MoveUp()
		{
			Vector3 pos = Sprite.gameObject.transform.localPosition;
			pos.y = WorldMap.transform.position.y + 1;
			Sprite.gameObject.transform.localPosition = pos;
		}




		/// <summary>
		/// Abaixa objeto, retornando ao plano padrao dos objetos.
		/// </summary>
		private void MoveDown()
		{
			Vector3 pos = Sprite.gameObject.transform.localPosition;
			pos.y = WorldMap.transform.position.y;
			Sprite.gameObject.transform.localPosition = pos;
		}




		/// <summary>
		/// Retorna a posicao original, ou destroi objeto.
		/// </summary>
		public void CancelPlacement()
		{
			if (CurrentState == ETileObjectState.Moving) {
				if (LastPlacedPosition.x == -1)
				{
					DestroyObject(gameObject);
				} else 
				{
					TilePosition = LastPlacedPosition;
				}
			}
			TileMapInputController.movingbuilding = false;
			SetMoving (false);
			CurrentState = ETileObjectState.Placed;
		}



		/// <summary>
		/// Finaliza o posicionamento do objeto.
		/// Caso nao seja possivel posicionar, cancela a operacao
		/// </summary>
		public void FinishPlacement()
		{
			Sprite.color = Color.white;
			TileMapInputController.movingbuilding = false;
			if (CurrentState == ETileObjectState.Moving) {

				if (!WorldMap.CanPlaceObject(GetTileRect()))
				{
					CancelPlacement();
				} else 
				{
					if(BuildingInfo.buildingType == YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo.EBuildingType.SimpleHouse){
						Manager.Cash = Manager.Cash - 20;
						SetObjectToMap();
					}

					if(BuildingInfo.buildingType == YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo.EBuildingType.HealthCenter){
						Manager.Cash = Manager.Cash - 50;
						SetObjectToMap();
					}

					if(BuildingInfo.buildingType == YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo.EBuildingType.Hospital){
						Manager.Cash = Manager.Cash - 100;
						SetObjectToMap();
					}

				}

			}

			SetMoving (false);
			SetSelected (false);
			CurrentState = ETileObjectState.Building;
			Anim.SetBool ("Build", true);
		}



		public void SetSelected(bool selected)
		{
			SelectionGUI.SetActive (selected);
		}



		/// <summary>
		/// Configura o objeto para mover, conforme active
		/// </summary>
		/// <param name="active">If set to <c>true</c> active.</param>
		private void SetMoving(bool active)
		{
			MovingGUI.SetActive (active);

			if (active)
				MoveUp ();
			else
				MoveDown ();

		}




		/// <summary>
		/// Muda o estado do objeto
		/// </summary>
		/// <param name="newState">New state.</param>
		private void ChangeState (ETileObjectState newState)
		{
			SetSelected (false);

			if (CurrentState == ETileObjectState.Fixed) 
				return;
			else 
				CurrentState = newState;

			if (CurrentState != ETileObjectState.Moving) {

				SetMoving(false);
			} else {

				SetMoving(true);
			}
		}


		private void RemoveFromMap()
		{
			if (LastPlacedPosition != TileUtils.TILE_NULL_POSITION)
				WorldMap.RemoveObject (this);
		}


		public void SetObjectToMap()
		{
			WorldMap.PlaceObject(this);
			LastPlacedPosition = TilePosition;
		}



		void OnDestroy()
		{
			RemoveFromMap ();
		}






		// Use this for initialization
		void Start () {



			TileMapInputController.movingbuilding = true;
			if (WorldMap == null)
				WorldMap = TileUtils.WorldMap;

			if (!wasInitialized) {
				Initialize (WorldMap, TileMaterialSettings);			
			}

			LastPlacedPosition = TileUtils.TILE_NULL_POSITION;

			if (CurrentState == ETileObjectState.Fixed) {

				SetObjectToMap();

			} else if (CurrentState == ETileObjectState.Placed)
			{
				SetObjectToMap();
			}

			ChangeState (CurrentState);
		}

		void FixedUpdate(){


			if (Input.GetKeyDown (KeyCode.K))
				RotateSprite ();


			Sprite.sortingOrder = (int)TilePosition.y - (int)TilePosition.x;
			//CONSTRUINDO
			if (CurrentState == ETileObjectState.Building) {
				Sprite.renderer.enabled = false;
			
				//PRONTA
			} else if (CurrentState == ETileObjectState.Placed && !corout) {

				corout = true;
				StartCoroutine ("building");


				//Anim.SetBool("Build",false);
				//Sprite.renderer.enabled = true;
				//MOVENDO
			} else if (CurrentState == ETileObjectState.Moving) {

				//TryMoveInDeltaPos (new Vector2(Input.GetAxis ("Horizontal"),-Input.GetAxis ("Vertical")));
			
				float pointer_x = Input.GetAxis ("Mouse X");
				float pointer_y = Input.GetAxis ("Mouse Y");

				if (Input.GetMouseButton(0)){

				Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit info;
				Physics.Raycast(r, out info, Mathf.Infinity);


				if ( info.collider != null) {

					//Debug.Log(info.collider.tag);
//					if (info.collider.tag == "ObjMovingButton")
//					{
//
//						pointer_x = Input.touches [0].deltaPosition.x;
//					
//						pointer_y = Input.touches [0].deltaPosition.y;
//
//						TryMoveInDeltaPos (new Vector2 (pointer_x * 0.025f, pointer_y * -0.025f));
//					
//					}
					if (info.collider.tag == "ObjMovingButtonUp")
						TryMoveInX(1);

					if (info.collider.tag == "ObjMovingButtonDown")
						TryMoveInX(-1);

					if (info.collider.tag == "ObjMovingButtonLeft")
						TryMoveInY(-1);

					if (info.collider.tag == "ObjMovingButtonRight")
						TryMoveInY(1);

				}
				}

			} else {

			}


			foreach (GameObject x in GameObject.FindGameObjectsWithTag("Habitant")) {
			
				if (x != null && Vector3.Distance (x.transform.position, transform.position) < 2 && CurrentState != ETileObjectState.Moving){
					CurrentState = ETileObjectState.Placed;

			}
			}




		}

		IEnumerator building(){

			Smoke.SetTrigger ("SmokeOn");
			yield return new WaitForSeconds(0.1f);
			Anim.SetBool("Build",false);
			Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.Building);
			yield return new WaitForSeconds(0.5f);
			Sprite.renderer.enabled = true;

		}

		IEnumerator destroying(){

			Smoke.SetTrigger ("SmokeOn");
			yield return new WaitForSeconds(0.1f);
			Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.Building);
			yield return new WaitForSeconds(0.5f);
			Sprite.renderer.enabled = false;
			 	
			
		}


	}


}
