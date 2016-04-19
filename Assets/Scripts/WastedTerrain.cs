using UnityEngine;
using System.Collections;

public class WastedTerrain : MonoBehaviour {
	private float distance = 2.2f;
	public Animator smoke;
	private float count;
	private int stage;

	public enum EEventType {
		
		WasteLand, // Terreno Baldio
		HotDog, // Cachorro Quente
		ManHole	// Bueiro
	}

	public EEventType EventType;

	void Start () {
		stage = PlayerPrefs.GetInt ("fase");
	}
	

	void FixedUpdate () {
	
		count = count + Time.deltaTime;
		if (count >= 10) {
			Teste ();
			count = 0;
		}

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Building")) {
		
			if (GetComponent<SpriteRenderer> ().enabled == true && this.EventType == EEventType.WasteLand && Vector3.Distance (transform.position, obj.transform.position) <= distance && obj.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().buildingType == YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo.EBuildingType.SimpleHouse) {
				obj.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().percentage = 20;
			}

			if (GetComponent<SpriteRenderer> ().enabled == true && this.EventType == EEventType.ManHole && Vector3.Distance (transform.position, obj.transform.position) <= distance && obj.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().buildingType == YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo.EBuildingType.SimpleHouse) {
				obj.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().percentage = 50;
			}
		
		}

		foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Habitant")) {
		
			if (GetComponent<SpriteRenderer> ().enabled == true && this.EventType == EEventType.HotDog && Vector3.Distance (transform.position, obj.transform.position) <= 1 && obj.name != "Jaleco" && obj.name != "Rebeca" ) {
				obj.GetComponent<HabitantMovement> ().obeso = true;
			}
		}

	}

	public void Teste(){
		
		float x = Random.value;
		if (x <= 0.03 && GetComponent<SpriteRenderer> ().enabled == false && this.EventType == EEventType.WasteLand && stage == 0) {
			StartCoroutine("building");
		}

		if (x <= 0.02 && GetComponent<SpriteRenderer> ().enabled == false && this.EventType == EEventType.HotDog && stage != 0) {
			StartCoroutine("building");
		}

		if (x <= 0.01 && GetComponent<SpriteRenderer> ().enabled == false && this.EventType == EEventType.ManHole && stage == 2) {
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
