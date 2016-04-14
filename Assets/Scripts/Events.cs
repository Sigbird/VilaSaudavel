using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Events : MonoBehaviour {
	private static bool first_time;
	private static bool first_time_agent;
	private static bool first_time_dengue;
	private static bool second_intro;
	private static bool third_intro;
	public static bool creatures;
	public bool examin;
	public bool contamination;
	public GameObject DialogText;
	public GameObject DialogText2;
	public GameObject WastedText;
	public GameObject TextDialog;
	public GameObject GameOverWindow;
	public GameObject VictoryWindow;
	public static int DialogSequence;
	public static int FaseControler;
	public GameObject Hand;
	public Sprite Image;
	public Sprite Agent;
	public Sprite Dr;
	public Sprite Man;
	public Sprite Woman;
	public GameObject SelectedBuilding;

	public GameObject House2;
	public GameObject House3;
	public GameObject Revoada;
	public Animator CamController;


	// Use this for initialization
	void Start () {

		DialogSequence = -1;
		StartCoroutine ("WaitFade");
		if (PlayerPrefs.HasKey ("fase")) {
			FaseControler = PlayerPrefs.GetInt ("fase");
		}
//		Debug.Log ("fase: " + FaseControler);
		//DialogText = GameObject.Find ("DialogText");
		//DialogText = GameObject.Find ("DialogText2");
		//TextDialog = GameObject.Find ("TextDialog");
		switch (FaseControler) {
		case 0:
			first_time_agent = true;
			first_time = true;
			first_time_dengue = true;
			second_intro = false;
			third_intro = false;
			break;
		case 1:
			first_time_agent = false;
			first_time = false;
			first_time_dengue = false;
			second_intro = true;
			third_intro = false;
			break;
		case 2:
			first_time_agent = false;
			first_time = false;
			first_time_dengue = false;
			second_intro = false;
			third_intro = true;
			break;
		default:
			break;
		}
		

//		if (!first_time) {
//			EndDialog();
//		} else {
//			StartDialog("Ola! Bem vindo a vila saudavel! Comece sua vila construindo com botao ao lado.");
//		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if(creatures == true){
			TesteRevoada();
			TesteSeaCreature();
			creatures = false;
		}

//		Debug.Log(Hand.GetComponent<Animator>().GetInteger("TutorialID"));
		if (first_time) {
			if(DialogSequence == 0){
				if(GameObject.Find("DialogImage") != null){
					GameObject.Find("DialogImage").GetComponent<Image>().sprite = Dr; 
				}
				StartDialog("Ola! Bem vindo a vila saudavel! Comece sua vila construindo com a ferramenta a esquerda.", Screen.height/2, Screen.width/2 - 400);
				Tutorial(1);
				House2.GetComponent<Button>().interactable = false;
				House3.GetComponent<Button>().interactable = false;
			}
			if(DialogSequence == 2){
				if(GameObject.Find("DialogImage") != null){
					GameObject.Find("DialogImage").GetComponent<Image>().sprite = Dr; 
				}
				StartDialog("Muito Bem! Agora moradores estão chegando a sua vila!", -100, -100);
				CamController.enabled = true;
				CamController.SetTrigger("Entrance");
				House2.GetComponent<Button>().interactable = true;
				House3.GetComponent<Button>().interactable = true;
				first_time = false;
			}
		}

		if (this.contamination == true && first_time_dengue == true) {
			if(GameObject.Find("DialogImage") != null){
				GameObject.Find("DialogImage").GetComponent<Image>().sprite = Dr; 
			}
			StartDialog ("Oh Não! Um foco de dengue foi detectado! Rápido vamos contruir um posto de saúde!", 0, 0);
			first_time_dengue = false;
		}

		if (DialogSequence == 4 && first_time_agent == true) {
			if(GameObject.Find("DialogImage") != null){
				GameObject.Find("DialogImage").GetComponent<Image>().sprite = Agent; 
			}

			StartDialog ("Parabéns! Postos de saúde ajudam a manter a saúde nas casas, uma família doente contribui menos para os recursos da vila.", 0, 0);
			first_time_agent = false;
		}
//		Debug.Log (DialogSequence);
		if (second_intro) {
			if(DialogSequence == 0){
				StartDialog("Prepare-se! Agora novas ameaças afetarão a saúde da sua vila!", Screen.height/2, Screen.width/2 - 400);
			}
		}

		if (third_intro) {
			if(DialogSequence == 0){
				StartDialog("Muito Bem! Agora precisará usar tudo que aprendeu até aqui para firmar sua vila!", Screen.height/2, Screen.width/2 - 400);
			}
		}

		foreach (GameObject n in GameObject.FindGameObjectsWithTag("Building")) {
		if(n.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo>().contaminada == true){
				this.contamination = true;
			}else{
				this.contamination = false;
		}

		}

		if (Input.GetMouseButtonDown (0) && examin == true) {
			Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit info;
			Physics.Raycast (r, out info, Mathf.Infinity);
		
			if (info.collider != null ) {
				//Debug.Log (info.collider.tag);
				StartDialog2 (info.transform.gameObject);
				//Debug.Log(info.transform.tag);
			}

	
		}
	}

	public void Tutorial(int x){
	
		if (first_time) {
			Hand.GetComponent<Animator>().SetInteger("TutorialID", x );
		}

		if (first_time_agent) {
			Hand.GetComponent<Animator>().SetInteger("TutorialID", x );
		}

	}

	public void StartDialog(string text, int x, int y){
		Time.timeScale = 0;
		DialogText.SetActive(true);
		TextDialog.GetComponent<Text> ().text = text;
	//	Hand.transform.position = new Vector3 (x, y, 0);
		DialogSequence = -1;
	}

	public void PlayDialog(string text){
		Time.timeScale = 0;
		DialogText.SetActive(true);
		TextDialog.GetComponent<Text> ().text = text;
		//	Hand.transform.position = new Vector3 (x, y, 0);
	}

	public void StartDialog2(GameObject House){

//		if (House.tag == "Building" && House.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().tileObject.CurrentState == YupiStudios.VilaSaudavel.Tiles.TileObject.ETileObjectState.Placed) {
//			Time.timeScale = 0;
//			DialogText2.GetComponent<DialogInfoPanel> ().Renda.text = "Renda\n" + House.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().renda;
//			DialogText2.GetComponent<DialogInfoPanel> ().Saude.text = "Saude \n" + House.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().saude;
//			DialogText2.GetComponent<DialogInfoPanel> ().Descricao.text = House.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().descriçao;
//			DialogText2.GetComponent<DialogInfoPanel> ().Info.text = House.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().info;
//			DialogText2.GetComponent<DialogInfoPanel> ().ilustracao.sprite = House.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().ilustracao;
//			SelectedBuilding = House;
//			DialogText2.SetActive(true);
//		}

		if (House.tag == "WastedTerrain") {

			if(House.GetComponent<SpriteRenderer>().enabled == true){
			Time.timeScale = 0;
				WastedText.GetComponent<DialogInfoPanel> ().Renda.text = "Renda\n" + 0;
				WastedText.GetComponent<DialogInfoPanel> ().Saude.text = "Saude \n" + 0 ;
				WastedText.GetComponent<DialogInfoPanel> ().Descricao.text = "Terreno Baldio";
				WastedText.GetComponent<DialogInfoPanel> ().Descricao.color = Color.red;
				WastedText.GetComponent<DialogInfoPanel> ().Status.text = "Contaminado";
				WastedText.GetComponent<DialogInfoPanel> ().Status.color = Color.red;
				WastedText.GetComponent<DialogInfoPanel> ().Info.text = "Aumenta o risco de contaminaçao de moradores proximos.";
				WastedText.GetComponent<DialogInfoPanel> ().Info.color = Color.red;
				WastedText.GetComponent<DialogInfoPanel> ().ilustracao.sprite = House.GetComponent<SpriteRenderer>().sprite ;
				SelectedBuilding = House;
				WastedText.SetActive(true);
			}
		}

		if (House.tag == "Habitant" || House.tag == "Jaleco" ) {
			Time.timeScale = 0;
			DialogText2.GetComponent<DialogInfoPanel> ().Renda.text = "Idade\n" + House.GetComponent<HabitantMovement>().age;
			DialogText2.GetComponent<DialogInfoPanel> ().Saude.text = "Saude \n" + House.GetComponent<HabitantMovement>().healt;
			DialogText2.GetComponent<DialogInfoPanel> ().Descricao.text = House.GetComponent<HabitantMovement>().name;
			DialogText2.GetComponent<DialogInfoPanel> ().Info.text = House.GetComponent<HabitantMovement>().info;
			DialogText2.GetComponent<DialogInfoPanel> ().Status.text = House.GetComponent<HabitantMovement>().status;
			DialogText2.GetComponent<DialogInfoPanel> ().ilustracao.sprite = House.GetComponent<HabitantMovement>().ilustracao;
			DialogText2.GetComponent<DialogInfoPanel> ().DestroyButton.SetActive(false);
			DialogText2.GetComponent<DialogInfoPanel> ().UpgradeButton.SetActive(false);
			if(House.GetComponent<HabitantMovement>().status == "Contaminado"){
				DialogText2.GetComponent<DialogInfoPanel> ().Info.color = Color.red;
				DialogText2.GetComponent<DialogInfoPanel> ().Status.color = Color.red;
				DialogText2.GetComponent<DialogInfoPanel> ().contaminAlert.enabled = true;
			}else{
				DialogText2.GetComponent<DialogInfoPanel> ().Info.color = Color.black;
				DialogText2.GetComponent<DialogInfoPanel> ().Status.color = Color.white;
				DialogText2.GetComponent<DialogInfoPanel> ().contaminAlert.enabled = false;
			}
			DialogText2.SetActive(true);
		}
			//DialogText2.GetComponent<DialogInfoPanel> ().ilustracao.

		//TextDialog.GetComponent<Text> ().text = text;

	}

	public void EndDialog(){
		Time.timeScale = 1;
		DialogText.SetActive(false);
		DialogText2.SetActive(false);
		WastedText.SetActive(false);
		Hand.transform.position = new Vector3 (-100, -100, 0);
		CamController.enabled = false;
	}

	public void CloseWasteLand(){
		SelectedBuilding.GetComponent<SpriteRenderer> ().enabled = false;
	}


	public void TesteRevoada(){
		
		float x = Random.value;
		if (x <= 0.1f) {
			Instantiate(Revoada,new Vector3(Random.Range(69,79),0,Random.Range(11,2)),Quaternion.Euler(90,0,0));
		}
		
	}

	public void TesteSeaCreature(){
		
		float x = Random.value;
		if (x <= 0.1f) {
			foreach(GameObject anim in GameObject.FindGameObjectsWithTag("SeaCreature")){
				anim.GetComponent<Animator>().SetTrigger("Whale");
			}
		}
		
	}


	public void SetExamin(bool x){
		this.examin = x;
	}

	IEnumerator WaitFade(){
		yield return new WaitForSeconds (0.4f);
		DialogSequence = 0;
	}

	public void GameOver(){
		GameOverWindow.SetActive (true);
		Time.timeScale = 0;
		
	}

	public void Victory(){
		VictoryWindow.SetActive (true);
		Time.timeScale = 0;
	}



}
