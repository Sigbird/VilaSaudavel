using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Events : MonoBehaviour {
	private static bool first_time;
	private GameObject DialogText;
	private GameObject TextDialog;
	// Use this for initialization
	void Start () {

		DialogText = GameObject.Find ("DialogText");
		TextDialog = GameObject.Find ("TextDialog");

		first_time = true;

		if (!first_time) {
			EndDialog();
		} else {
			StartDialog("Ola! Bem vindo a vila saudavel! Comece sua vila construindo com botao ao lado.");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void StartDialog(string text){
		Time.timeScale = 0;
		DialogText.SetActive(true);
		TextDialog.GetComponent<Text> ().text = text;
	}

	public void EndDialog(){
		Time.timeScale = 1;
		DialogText.SetActive(false);
	}
}
