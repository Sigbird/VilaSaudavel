using UnityEngine;
using System.Collections;

public class CallScene : MonoBehaviour {

	public Animator FadeScreen;
	public float ConfigVolume{get; set;}
	// Use this for initialization
	void Start () {
		if (Application.loadedLevelName == "Splash")
			StartCoroutine ("Splash");
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
		yield return new WaitForSeconds (0.35f);
		Application.LoadLevel ("Loading");
	}

}
