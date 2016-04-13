using UnityEngine;
using System.Collections;

public class WastedTerrain : MonoBehaviour {
	private float distance = 2.2f;
	public Animator smoke;
	private float count;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		count = count + Time.deltaTime;
		if (count >= 10) {
			Teste();
			count = 0;
		}

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Building")) {
		
			if(GetComponent<SpriteRenderer> ().enabled == true && Vector3.Distance(transform.position, obj.transform.position) <= distance &&  obj.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo>().buildingType == YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo.EBuildingType.SimpleHouse){
				obj.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo>().contaminada = true;
//				Debug.Log("dentro");
			}
		
		}

	}

	public void Teste(){
		
		float x = Random.value;
		if (x <= 0.03 && GetComponent<SpriteRenderer> ().enabled == false) {
			StartCoroutine("building");
		}
		
	}

	IEnumerator building(){
		smoke.SetTrigger ("SmokeOn");
		yield return new WaitForSeconds(0.1f);
		Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.Building);
		yield return new WaitForSeconds(0.5f);
		GetComponent<SpriteRenderer> ().enabled = true;
	}

	IEnumerator destroying(){
		smoke.SetTrigger ("SmokeOn");
		yield return new WaitForSeconds(0.1f);
		Camera.main.GetComponent<AudioController> ().PlayState (AudioController.EAudioState.Building);
		yield return new WaitForSeconds(0.5f);
		GetComponent<SpriteRenderer> ().enabled = false;
	}



}
