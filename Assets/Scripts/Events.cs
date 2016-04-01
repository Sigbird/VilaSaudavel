using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Events : MonoBehaviour {
	private static bool first_time;
	public GameObject DialogText;
	public GameObject DialogText2;
	public GameObject TextDialog;
	public static int DialogSequence;
	public GameObject Hand;

	// Use this for initialization
	void Start () {

		//DialogText = GameObject.Find ("DialogText");
		//DialogText = GameObject.Find ("DialogText2");
		//TextDialog = GameObject.Find ("TextDialog");

		first_time = true;
//
//		if (!first_time) {
//			EndDialog();
//		} else {
//			StartDialog("Ola! Bem vindo a vila saudavel! Comece sua vila construindo com botao ao lado.");
//		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (first_time) {
			if(DialogSequence == 0)
				StartDialog("Ola! Bem vindo a vila saudavel! Comece sua vila construindo com a ferramenta a esquerda.", Screen.height/2, Screen.width/2 - 400);

			if(DialogSequence == 2)
				StartDialog("Muito Bem! Agora moradores estao chegando a sua vila!", -100, -100);
		}

		if(DialogSequence == 3)
			StartDialog("Oh Nao! Um foco de dengue foi detectado! Rapido vamos contruir um posto de saude!", 0, 0);

//		Debug.Log (DialogSequence);
	
		if (Input.GetMouseButtonDown (0)) {
			Ray r = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit info;
			Physics.Raycast (r, out info, Mathf.Infinity);
		
			if (info.collider != null) {
				//Debug.Log (info.collider.tag);
				StartDialog2 (info.transform.gameObject);
				//Debug.Log(info.transform.tag);
			}
	
		}
	}

	public void StartDialog(string text, int x, int y){
		Time.timeScale = 0;
		DialogText.SetActive(true);
		TextDialog.GetComponent<Text> ().text = text;
		Hand.transform.position = new Vector3 (x, y, 0);
		DialogSequence = -1;
	}

	public void StartDialog2(GameObject House){

		if (House.tag == "Building" && House.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().tileObject.CurrentState == YupiStudios.VilaSaudavel.Tiles.TileObject.ETileObjectState.Placed) {
			Time.timeScale = 0;
			DialogText2.GetComponent<DialogInfoPanel> ().Renda.text = "Renda\n" + House.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().renda;
			DialogText2.GetComponent<DialogInfoPanel> ().Saude.text = "Saude \n" + House.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().saude;
			DialogText2.GetComponent<DialogInfoPanel> ().Descricao.text = House.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().descriçao;
			DialogText2.GetComponent<DialogInfoPanel> ().Info.text = House.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().info;
			DialogText2.SetActive(true);
		}
		if (House.tag == "Habitant" || House.tag == "Jaleco" ) {
			Time.timeScale = 0;
			DialogText2.GetComponent<DialogInfoPanel> ().Renda.text = "Idade\n" + House.GetComponent<HabitantMovement>().age;
			DialogText2.GetComponent<DialogInfoPanel> ().Saude.text = "Saude \n" + House.GetComponent<HabitantMovement>().healt;
			DialogText2.GetComponent<DialogInfoPanel> ().Descricao.text = House.GetComponent<HabitantMovement>().name;
			DialogText2.GetComponent<DialogInfoPanel> ().Info.text = House.GetComponent<HabitantMovement>().info;
			DialogText2.SetActive(true);
		}
			//DialogText2.GetComponent<DialogInfoPanel> ().ilustracao.

		//TextDialog.GetComponent<Text> ().text = text;

	}

	public void EndDialog(){
		Time.timeScale = 1;
		DialogText.SetActive(false);
		DialogText2.SetActive(false);
		Hand.transform.position = new Vector3 (-100, -100, 0);
	}
}
