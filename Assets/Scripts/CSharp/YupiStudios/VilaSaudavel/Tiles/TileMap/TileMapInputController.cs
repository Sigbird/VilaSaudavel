using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace YupiStudios.VilaSaudavel.Tiles.TileMap {
	
	public class TileMapInputController : MonoBehaviour {
		
		public enum EInputType{
			InputCamera,
			InputGrass,
			InputRoad,
			InputBuilding,
		}
		
		public enum EInputState{
			Idle,
			Zooming,
			MovingCamera,
			Constructing,
		}
		
		private int buildID;
		public DialogInfoPanel WindowPanel;
		public GameObject DialogWindow;
		public Manager GameManager;
		public bool Examin	{ get; set; }

		public UIInGameController UIController;
		private TileMapData Data;
		private Camera RefCamera;
		
		private Vector2 MinCamPos;
		private Vector2 MaxCamPos;
		
		public TileObjectFactory objectFactory;
		
		public float ZoomingSpeedFactor;
		public float MinCamSize;
		public float MaxCamSize;
		
		public TileHighlight TileHighlighter;
		
		private EventSystem eventSystem;
		
		private EInputState state;
		
		private Vector2 touch1;
		private Vector2 touch2;
		
		private Vector2 movingSpeedFactor;
		
		private TileObject selectedObject;
		
		public static bool movingbuilding;
		
		private void ClearSelected()
		{
			if (selectedObject != null) {
				selectedObject.SetSelected(false);
				selectedObject = null;
			}
		}
		
		private void SelectObject(TileObject obj)
		{
			ClearSelected ();
			
			selectedObject = obj;
			selectedObject.SetSelected (true);
		}
		
		
		private void RecalcMovingSpeed()
		{
			Vector3 p1 = RefCamera.ScreenToWorldPoint ( new Vector3 (0, 0, RefCamera.transform.position.y) );
			
			Vector3 p2 = RefCamera.ScreenToWorldPoint ( new Vector3 (Screen.width-1, Screen.height-1, RefCamera.transform.position.y) );
			
			movingSpeedFactor = new Vector2( (p2.x - p1.x) / Screen.width, (p2.z - p1.z) / Screen.height);
		}
		
		private void SetCameraPos(Vector3 pos)
		{
			if (pos.x < MinCamPos.x )
				pos.x = MinCamPos.x ;
			if (pos.x > MaxCamPos.x )
				pos.x = MaxCamPos.x ;
			
			if (pos.z < MinCamPos.y )
				pos.z = MinCamPos.y ;
			if (pos.z > MaxCamPos.y )
				pos.z = MaxCamPos.y ;
			
			RefCamera.transform.position = pos;
		}
		
		private void MovingCameraUpdate ()
		{
			
			Vector2 newTouch = Input.mousePosition;
			
			Vector3 pos = RefCamera.transform.position;
			float x = pos.x +  movingSpeedFactor.x * (touch1.x - newTouch.x);
			float z = pos.z + movingSpeedFactor.y * (touch1.y - newTouch.y);
			if (x >= 45 && x <= 74 && z <= 11 && z >= -10) {
				SetCameraPos (new Vector3 (x, pos.y, z));
			}
			touch1 = Input.mousePosition;
		}
		
		private void ZoomingUpdate (Vector2 t1, Vector2 t2)
		{
			
			float pos = RefCamera.orthographicSize;
			float diff = (Vector2.Distance (touch1, touch2) - Vector2.Distance (t1, t2)) * ZoomingSpeedFactor;
			
			pos += diff;
			
			if (pos < MinCamSize)
				pos = MinCamSize;
			if (pos > MaxCamSize)
				pos = MaxCamSize;
			
			touch1 = t1;
			touch2 = t2;
			
			RefCamera.orthographicSize = pos;
			
			
			RecalcMovingSpeed ();
		}
		
		
		
		private void MobileInput()
		{
			switch (Input.touchCount ) {
				
			case 2:
				if (state == EInputState.Zooming)
				{
					ZoomingUpdate (Input.touches[0].position,Input.touches[1].position);
				} else
				{
					if (touch1 == Vector2.zero)
					{
						touch1 = Input.touches[0].position;
					}
					if (touch2 == Vector2.zero)
					{
						touch2 = Input.touches[1].position;
					}
					state = EInputState.Zooming;
				}
				break;
				
			case 1:
				if (state == EInputState.MovingCamera)
				{
					MovingCameraUpdate ();
				} else
				{
					if (touch1 == Vector2.zero)
					{
						touch1 = Input.mousePosition;
						state = EInputState.MovingCamera;
					}
				}
				break;
				
			default:
				if (state != EInputState.Idle)
				{
					state = EInputState.Idle;
					touch1 = Vector2.zero;
					touch2 = Vector2.zero;
				}
				break;
				
			}
		}
		
		private void DesktopInput()
		{
			if (Input.GetMouseButton (0)) {
				if (state == EInputState.MovingCamera) {
					MovingCameraUpdate ();
				} else {
					if (touch1 == Vector2.zero) {
						touch1 = Input.mousePosition;
						state = EInputState.MovingCamera;
					}
				}
			} else if (Input.GetMouseButton (1)) {
				
				if (state == EInputState.Zooming)
				{
					ZoomingUpdate (Input.mousePosition, touch2);
				} else
				{
					if (touch1 == Vector2.zero)
					{
						touch1 = Input.mousePosition;
					}
					if (touch2 == Vector2.zero)
					{
						touch2 = new Vector2(Screen.width/2, Screen.height/2);
					}
					state = EInputState.Zooming;
				}
				
			} else {
				if (state != EInputState.Idle)
				{
					state = EInputState.Idle;
					touch1 = Vector2.zero;
					touch2 = Vector2.zero;
				}
			}
		}
		
		private bool ProcessMovingMap()
		{
			if (Input.GetMouseButton (0) && Input.touchCount < 2) {
				
				//UpdateHighlight();
				
				if (state == EInputState.MovingCamera) {
					MovingCameraUpdate ();
				} else {
					if (touch1 == Vector2.zero) {
						touch1 = Input.mousePosition;
						state = EInputState.MovingCamera;
					}
				}
				
				return true;
			}
			return false;
		}
		
		
		private bool ProcessZoomingDesktop()
		{
			if (Input.GetMouseButton (1)) {
				
				if (state == EInputState.Zooming)
				{
					ZoomingUpdate (Input.mousePosition, touch2);
				} else
				{
					if (touch1 == Vector2.zero)
					{
						touch1 = Input.mousePosition;
					}
					if (touch2 == Vector2.zero)
					{
						touch2 = new Vector2(Screen.width/2, Screen.height/2);
					}
					state = EInputState.Zooming;
				}
				
				return true;
			}
			
			return false;
		}
		
		private bool ProcessZoomingMobile()
		{
			if (Input.touchCount == 2) {
				
				if (state == EInputState.Zooming)
				{
					ZoomingUpdate (Input.touches[0].position,Input.touches[1].position);
				} else
				{
					if (touch1 == Vector2.zero)
					{
						touch1 = Input.touches[0].position;
					}
					if (touch2 == Vector2.zero)
					{
						touch2 = Input.touches[1].position;
					}
					state = EInputState.Zooming;
				}
				return true;
				
			}
			
			return false;
		}
		
		
		private void UpdateHighlight()
		{
			#if UNITY_EDITOR
			if (Input.GetMouseButtonDown(0))
				#else
				if (Input.touchCount == 1 && Input.GetMouseButtonDown(0))
					#endif
			{
				Vector3 desloc = Data.transform.position;
				
				
				
				Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit info;
				Physics.Raycast(r, out info, Mathf.Infinity);
				
				if (info.collider != null)
				{
					//Debug.Log (info.collider.tag);
					if (info.collider.tag == "WorldMap")
					{
						Vector3 p = info.point - desloc;
						Vector2 tile = TileUtils.WorldPosToTile(Data,new Vector3(p.x,0,p.z)); 
						
						//TileHighlighter.gameObject.SetActive(true);
						TileHighlighter.TilePos = tile;
					} else 
					{
						//TileHighlighter.gameObject.SetActive(true);
					}



				}
				
				
				
			}
		}
		
		private bool ProcessIdleState()
		{
			if (state != EInputState.Idle)
			{
				state = EInputState.Idle;
				touch1 = Vector2.zero;
				touch2 = Vector2.zero;
				return true;
			}
			
			return false;
		}
		
		private bool ProcessSelecting()
		{
			#if UNITY_EDITOR
			if (Input.GetMouseButtonDown(0))
				#else
				if (Input.touchCount == 1 && Input.GetMouseButtonDown(0))
					#endif
			{
				Vector3 desloc = Data.transform.position;
				
				Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit info;
				Physics.Raycast(r, out info, Mathf.Infinity);
				
				if (info.collider != null)
				{

					if (info.collider.tag == "Building")
					{
						if(Examin == true){
						WindowPanel.Renda.text = "Renda\n" + info.transform.gameObject.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().renda;
						WindowPanel.Saude.text = "Saude \n" + info.transform.gameObject.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().saude;
						WindowPanel.Descricao.text = info.transform.gameObject.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().descriçao;
						WindowPanel.Info.text = info.transform.gameObject.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().info;
						WindowPanel.Status.text = info.transform.gameObject.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().status;
						WindowPanel.ilustracao.sprite = info.transform.gameObject.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().ilustracao;
							if(info.transform.gameObject.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().status == "Contaminada"){
								WindowPanel.Info.color = Color.red;
								WindowPanel.Status.color = Color.red;
								WindowPanel.contaminAlert.enabled = true;
								WindowPanel.UpgradeButton.GetComponent<Button>().interactable = false;
								WindowPanel.DestroyButton.GetComponent<Button>().interactable = false;
							}
							else{
								WindowPanel.Info.color = Color.black;
								WindowPanel.Status.color = Color.white;
								WindowPanel.contaminAlert.enabled = false;
								WindowPanel.UpgradeButton.GetComponent<Button>().interactable = true;
								WindowPanel.DestroyButton.GetComponent<Button>().interactable = true;
							}
						if(info.transform.gameObject.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().buildingType == YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo.EBuildingType.SimpleHouse){
								WindowPanel.UpgradeButton.SetActive(true);
								if(PlayerPrefs.GetInt("fase")<1){
									WindowPanel.UpgradeButton.GetComponent<Button>().interactable = false;
								}else if(info.transform.gameObject.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().status == "Contaminada" || info.transform.gameObject.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().Upgraded == true){
									WindowPanel.UpgradeButton.GetComponent<Button>().interactable = false;
								}else{
									WindowPanel.UpgradeButton.GetComponent<Button>().interactable = true;
								}
							}else{
								WindowPanel.UpgradeButton.SetActive(false);
							}
						WindowPanel.DestroyButton.SetActive(true);
						DialogWindow.SetActive(true);
						}

						Vector3 p = info.point - desloc;
						Vector2 tile = TileUtils.WorldPosToTile(Data,new Vector3(p.x,0,p.z));
						
						TileInfo tileInfo = Data.TileMap[(int)tile.y,(int)tile.x];
						
						if (tileInfo.Type != TileInfo.ETileType.Blocked)
						{
							
							if (tileInfo.Occupied)
							{					
								TileHighlighter.gameObject.SetActive(false);
								SelectObject(tileInfo.ObjectOwner);

								return true;
							} else 
							{
								ClearSelected();
								TileHighlighter.gameObject.SetActive(true);
								TileHighlighter.TilePos = tile;
							}
							
						} else 
						{
							TileHighlighter.gameObject.SetActive(false);
						}
					
					}

					if (info.collider.tag == "WorldMap")
					{
						Vector3 p = info.point - desloc;
						Vector2 tile = TileUtils.WorldPosToTile(Data,new Vector3(p.x,0,p.z));
						
						TileInfo tileInfo = Data.TileMap[(int)tile.y,(int)tile.x];
						
						if (tileInfo.Type != TileInfo.ETileType.Blocked)
						{
							
							if (tileInfo.Occupied)
							{					
								TileHighlighter.gameObject.SetActive(false);
								SelectObject(tileInfo.ObjectOwner);
								return true;
							} else 
							{
								ClearSelected();
								TileHighlighter.gameObject.SetActive(true);
								TileHighlighter.TilePos = tile;
							}
							
						} else 
						{
							TileHighlighter.gameObject.SetActive(false);
						}
					}
				}
				
			}
			
			return false;
		}
		
		
		private bool HitMap()
		{
			#if UNITY_EDITOR
			if (Input.GetMouseButton(0))
				#else
				if (Input.touchCount == 1 && Input.GetMouseButton(0))
					#endif
			{
				
				Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit info;
				Physics.Raycast(r, out info, Mathf.Infinity);
				
				if (info.collider != null)
				{
					if (info.collider.tag == "WorldMap")
					{
						return true;
					}
				}
				
			}
			
			return false;
		}
		
		
		
		
		private bool ProcessButtons()
		{
			
			/*retorna true se mouse em algum botao*/
			
			#if UNITY_EDITOR
			
			if (eventSystem.IsPointerOverGameObject ()) {
				return true;
			} else {
				return false;
			}
			
			#else 
			// mobile
			for (int i = 0; i < Input.touchCount; ++i)
				if (eventSystem.IsPointerOverGameObject (Input.GetTouch(i).fingerId))
					return true;
			
			return false;
			#endif
			
		}
		// Start Creation Manager
		
		public void CreateCustom(int x){

			Camera.main.GetComponent<Animator>().enabled = true;
			Camera.main.GetComponent<Animator>().SetTrigger("Pan");


			switch (x) {
			case 1:
				if (Manager.Cash >= 20) {
					UIController.ActiveBuildingOptions();
					CreateHouse();
				}else{
					Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
				}
				break;
			case 2:
				if (Manager.Cash >= 50) {
					UIController.ActiveBuildingOptions();
					CreateHealthCenter();
				}else{
					Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
				}
				break;
			case 3:
				if (Manager.Cash >= 100) {
					UIController.ActiveBuildingOptions();
					CreateHospital ();
				}else{
					Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
				}
				break;
			case 4:
				if (Manager.Cash >= 100) {
					UIController.ActiveBuildingOptions();
					CreateCondiminio();
				}else{
					Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
				}
				break;
			case 5:
				if (Manager.Cash >= 100) {
					UIController.ActiveBuildingOptions();
					CreatePraca ();
				}else{
					Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
				}
				break;
			case 6:
				if (Manager.Cash >= 100) {
					UIController.ActiveBuildingOptions();
					CreateLixeira ();
				}else{
					Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
				}
				break;
			case 7:
				if (Manager.Cash >= 100) {
					UIController.ActiveBuildingOptions();
					CreateTratamento ();
				}else{
					Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
				}
				break;
			case 8:
				if (Manager.Cash >= 100) {
					UIController.ActiveBuildingOptions();
					CreateMercado ();
				}else{
					Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
				}
				break;
			case 9:
				if (Manager.Cash >= 100) {
					UIController.ActiveBuildingOptions();
					CreateColeta();
				}else{
					Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
				}
				break;
			default:
				print("CreateCustom error");
				break;
			}
		}
		
		public void CreateHouse()
		{
			if (Manager.Cash >= 20) {
				this.buildID = 1;
				objectFactory.TryInstantiateObject (0);
				TileHighlighter.gameObject.SetActive (false);
			} else {
				Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
			}
		}
		
		public void CreateHealthCenter()
		{
			if (Manager.Cash >= 50) {
				this.buildID = 2;
				objectFactory.TryInstantiateObject (1);
				TileHighlighter.gameObject.SetActive (false);
			} else {
				Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
			}
		}
		
		public void CreateHospital()
		{
			if (Manager.Cash >= 100) {
				this.buildID = 3;
				objectFactory.TryInstantiateObject (2);
				TileHighlighter.gameObject.SetActive (false);
			} else {
				Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
			}
		}
		
		public void CreateCondiminio()
		{
			if (Manager.Cash >= 0) {
				this.buildID = 1;
				objectFactory.TryInstantiateObject (3);
				TileHighlighter.gameObject.SetActive (false);
			} else {
				Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
			}
		}
		
		public void CreatePraca()
		{
			if (Manager.Cash >= 100) {
				this.buildID = 4;
				objectFactory.TryInstantiateObject (4);
				TileHighlighter.gameObject.SetActive (false);
			} else {
				Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
			}
		}
		
		public void CreateLixeira()
		{
			if (Manager.Cash >= 0) {
				this.buildID = 0;
				objectFactory.TryInstantiateObject (5);
				TileHighlighter.gameObject.SetActive (false);
			} else {
				Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
			}
		}
		
		public void CreateTratamento()
		{
			if (Manager.Cash >= 50) {
				this.buildID = 5;
				objectFactory.TryInstantiateObject (6);
				TileHighlighter.gameObject.SetActive (false);
			} else {
				Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
			}
		}
		
		public void CreateMercado()
		{
			if (Manager.Cash >= 0) {
				this.buildID = 0;
				objectFactory.TryInstantiateObject (7);
				TileHighlighter.gameObject.SetActive (false);
			} else {
				Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
			}
		}
		
		public void CreateColeta()
		{
			if (Manager.Cash >= 0) {
				this.buildID = 1;
				objectFactory.TryInstantiateObject (8);
				TileHighlighter.gameObject.SetActive (false);
			} else {
				Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.CantDo);
			}
		}

		public void Upgrade(){
			selectedObject.gameObject.GetComponent<YupiStudios.VilaSaudavel.Tiles.TileObject> ().Upgrade ();
		}

		public void DestroySelected()
		{
			if (selectedObject != null) {
				if(selectedObject.gameObject.GetComponent<YupiStudios.VilaSaudavel.Tiles.TileObject> ().BuildingInfo.contaminada == false){
//				Debug.Log (selectedObject.gameObject.name);
				GameObject.Destroy (selectedObject.gameObject);
				selectedObject = null;
				}
			}
		}

		public void DestroySelected3(){
			
			if (selectedObject != null) {
				if(selectedObject.gameObject.GetComponent<YupiStudios.VilaSaudavel.Tiles.TileObject> ().BuildingInfo.contaminada == false){

				//				Debug.Log (selectedObject.gameObject.name);
				selectedObject.gameObject.GetComponent<SpriteRenderer>().enabled = false;
				selectedObject = null;
				}
			}
		}

		public void DestroySelected2(){
			
			foreach (GameObject x in GameObject.FindGameObjectsWithTag("tileobject")) {
				if(x.GetComponent<TileObject>().CurrentState == TileObject.ETileObjectState.Moving)
					Destroy(x);
			}
		}
		
		public void FinishCreate()
		{
			
			switch (buildID) {
			case 1:
				
				//Casa
				//Manager.Pop = Manager.Pop + 20;
				GameManager.InstantiateHabitant(1);
				objectFactory.FinishMoving();
				this.buildID = 0;
				
				break;
			case 2:
				
				//Posto
				GameManager.InstantiateHabitant(2);
				objectFactory.FinishMoving();
				this.buildID = 0;
				
				break;
			case 3:
				
				//Hospital
				GameManager.InstantiateHabitant(3);
				objectFactory.FinishMoving();
				this.buildID = 0;
				
				break;
			case 4:
				
				//Praça
				Events.DialogSequence = 3;
				objectFactory.FinishMoving();
				this.buildID = 0;
				
				break;
			case 5:
				
				//Tratamento
				Events.DialogSequence = 3;
				objectFactory.FinishMoving();
				this.buildID = 0;
				
				break;
			default:
				objectFactory.FinishMoving();
				this.buildID = 0;
				break;
			}
			
		}
		
		private void UpdateInputType()
		{
			bool inputProcessed = false;
			
			inputProcessed = ProcessButtons ();
			
			if (!inputProcessed && !objectFactory.IsConstructing ()) {
				
				inputProcessed = ProcessSelecting ();
				
			} 
			
			if (!inputProcessed) {
				#if UNITY_EDITOR
				inputProcessed = ProcessZoomingDesktop ();
				#else
				inputProcessed = ProcessZoomingMobile ();
				#endif
			}
			
			
			if (!inputProcessed && HitMap())
				inputProcessed = ProcessMovingMap ();
			
			if (!inputProcessed)
				inputProcessed = ProcessIdleState ();
			
		}
		
		public Vector2 GetHighlightPosition()
		{
			if (TileHighlighter.gameObject.activeInHierarchy) {
				return TileHighlighter.TilePos;
			}
			
			return TileUtils.TILE_NULL_POSITION;
		}
		
		
		
		void Start ()
		{
			if (RefCamera == null)
				RefCamera = Camera.main;
			
			if (Data == null) {
				Data = TileUtils.WorldMap;
			}
			
			MinCamPos.x = Data.gameObject.transform.position.x;
			MaxCamPos.x = Data.gameObject.transform.position.x + Data.WorldSettings.TileSizeX * Data.WorldSettings.WorldSizeX;
			
			MinCamPos.y = Data.gameObject.transform.position.y - (Data.WorldSettings.TileSizeY * Data.WorldSettings.WorldSizeY / 2);
			MaxCamPos.y = Data.gameObject.transform.position.y + (Data.WorldSettings.TileSizeY * Data.WorldSettings.WorldSizeY / 2);
			
			RecalcMovingSpeed ();
			
			eventSystem = GameObject.FindObjectOfType <EventSystem> ();
			
		}
		
		// Update is called once per frame
		void Update () {
			if (!movingbuilding) {
				UpdateInputType ();
			} 
			
			if(Input.GetMouseButtonDown(0))
				Camera.main.GetComponent<Animator>().enabled = false;
		}
		
	}
}