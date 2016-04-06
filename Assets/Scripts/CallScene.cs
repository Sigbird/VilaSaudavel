using UnityEngine;
using System.Collections;

public class CallScene : MonoBehaviour {

	public Animator FadeScreen;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ExitGame(){
		Application.Quit ();
	}

	public void Scene(string scenename){
		StartCoroutine (FadeScene(scenename));
	}

	public IEnumerator FadeScene(string scenename){
		FadeScreen.SetTrigger ("Fade");
		yield return new WaitForSeconds (0.35f);
		Application.LoadLevel (scenename);
	}

}
