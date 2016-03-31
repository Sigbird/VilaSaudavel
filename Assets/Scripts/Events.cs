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
				StartDialog("Ola! Bem vindo a vila saudavel! Comece sua vila construindo com a ferramenta a esquerda.", 140, 50);

			if(DialogSequence == 2)
				StartDialog("Muito Bem! Agora moradores estao chegando a sua vila!", 0, 0);
		}

		if(DialogSequence == 3)
			StartDialog("Oh Nao! Um foco de dengue foi detectado! Rapido vamos contruir um posto de saude!", 0, 0);

		Debug.Log (DialogSequence);
	}

	public void StartDialog(string text, int x, int y){
		Time.timeScale = 0;
		DialogText.SetActive(true);
		TextDialog.GetComponent<Text> ().text = text;
		Hand.transform.position = new Vector3 (x, y, 0);
		DialogSequence = -1;
	}

	public void StartDialog2(GameObject House){
		Time.timeScale = 0;
		DialogText2.SetActive(true);
		//TextDialog.GetComponent<Text> ().text = text;
		DialogSequence = -1;
	}

	public void EndDialog(){
		Time.timeScale = 1;
		DialogText.SetActive(false);
		Hand.transform.position = new Vector3 (-20, -20, 0);
	}
}
