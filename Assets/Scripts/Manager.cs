using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public static int Cash;

	// Use this for initialization
	void Start () {
		Cash = 100;
	}
	
	// Update is called once per frame
	void Update () {
	
		GameObject.Find ("Money_text").GetComponent<Text> ().text = Cash.ToString();

	}
}
