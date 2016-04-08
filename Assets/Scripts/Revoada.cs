using UnityEngine;
using System.Collections;

public class Revoada : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 20f);
	}
	
	// Update is called once per frame
	void Update () {
	  
		transform.Translate (Vector3.down * Time.deltaTime * 1);
		transform.Translate (Vector3.left * Time.deltaTime * 2);

	}
}
