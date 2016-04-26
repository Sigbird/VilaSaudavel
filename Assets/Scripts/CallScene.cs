using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CallScene : MonoBehaviour {

	public Animator FadeScreen;
	public float ConfigVolume{get; set;}
	// Use this for initialization
	void Start () {
		if (Application.loadedLevelName == "Splash")
			StartCoroutine ("Splash");

		if (PlayerPrefs.GetInt ("fase") == 0) {
			if(GameObject.Find ("SavedGameBtn") != null)
			GameObject.Find ("SavedGameBtn").GetComponent<Button> ().interactable = false;
		} else {
			if(GameObject.Find ("SavedGameBtn") != null)
			GameObject.Find ("SavedGameBtn").GetComponent<Button> ().interactable = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void ExitGame(){
		Application.Quit ();
	}

	public void Scene(string scenename){

		PlayerPrefs.SetFloat("volume",ConfigVolume);

		if (scenename == "NewGame") {

			PlayerPrefs.SetInt("fase",0);
			StartCoroutine (FadeScene("Base"));
		}


//		if (scenename == "Next" && Events.FaseControler == 0) {
//
//			StartCoroutine (FadeScene ("Base"));
//		} else if (scenename == "Next" && Events.FaseControler == 1) {
//			PlayerPrefs.SetInt ("fase", Events.FaseControler + 1);
//			StartCoroutine (FadeScene ("Base"));
//		} else if (scenename == "Next" && Events.FaseControler == 2) {
//			StartCoroutine (FadeScene ("Menu"));
//		} else {
//			StartCoroutine (FadeScene(scenename));
//		}

		if (scenename == "Next" && Events.FaseControler < 2) {
			PlayerPrefs.SetInt ("fase", ++Events.FaseControler);
			StartCoroutine (FadeScene ("Base"));
		} else if (scenename == "Next" && Events.FaseControler == 2) {
			PlayerPrefs.SetInt ("fase", 0);
			StartCoroutine (FadeScene ("Menu"));
		}else if (scenename == "Base") {
			StartCoroutine (FadeScene(scenename));
		}


		if (scenename == "Menu") {
			PlayerPrefs.SetInt("fase",Events.FaseControler);
			StartCoroutine (FadeScene(scenename));
		}

	}

	public IEnumerator Splash(){
		yield return new WaitForSeconds (2f);
		FadeScreen.SetTrigger ("Fade");
		yield return new WaitForSeconds (0.35f);
		Application.LoadLevel ("Menu");
	}

	public IEnumerator FadeScene(string scenename){
		FadeScreen.SetTrigger ("Fade");
		PlayerPrefs.SetString ("SceneName", scenename);
		PlayerPrefs.SetFloat ("volume", AudioListener.volume);
		yield return new WaitForSeconds (0.35f);
		Application.LoadLevel ("Loading");
	}

}
