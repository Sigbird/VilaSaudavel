using UnityEngine;
using System.Collections;

public class LoadingScript : MonoBehaviour {
	public Animator FadeScreen;
	// Use this for initialization
	void Start () {
		StartCoroutine ("Loading");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Loading(){
		yield return new WaitForSeconds(3);
		FadeScreen.SetTrigger ("Fade");
		yield return new WaitForSeconds (0.35f);
		Application.LoadLevel (PlayerPrefs.GetString ("SceneName"));
	}
}
