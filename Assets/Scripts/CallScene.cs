using UnityEngine;
using System.Collections;

public class CallScene : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Scene(string scenename){
		Application.LoadLevel (scenename);
	}

}
