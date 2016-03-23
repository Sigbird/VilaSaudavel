using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {

	public GameObject[] Waypoints;


	// Use this for initialization
	void Start () {
		//StartCoroutine ("senoid");
	}
	
	// Update is called once per frame
	void Update () {

	}

	public GameObject NextPoint(){
		return Waypoints [Random.Range (0, Waypoints.Length)];
	}

	IEnumerator senoid(){
		transform.Translate (Vector3.forward * Time.deltaTime * 20);
		yield return new WaitForSeconds (0.5f);
		transform.Translate (Vector3.back* Time.deltaTime * 20);
		yield return new WaitForSeconds (0.5f);
		StartCoroutine ("senoid");
	}
}
