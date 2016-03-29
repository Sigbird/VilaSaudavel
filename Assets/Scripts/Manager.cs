using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public static int Cash;

	public static float Pop;

	public static float MaxPop;

	public static float Health;

	public static float MaxHealth;

	public Animator PopulationSphere;

	public Text PopulationText;

	public Animator HealthSphere;

	public Text HealthText;

	public GameObject Habitant;

	public GameObject RespawnHabitant;

	// Use this for initialization
	void Start () {
		Cash = 100;

		MaxPop = 0;

		MaxHealth = 100;

		Health = 0;

		Pop = 0;
	}
	
	// Update is called once per frame
	void Update () {



		if (Input.GetKey (KeyCode.Space))
			Cash++;



		PopulationSphere.SetFloat("Value",((Pop / MaxPop) * 100));
		PopulationText.text = Pop + "/" + MaxPop;

		HealthSphere.SetFloat("Value",((Health / MaxHealth) * 100));
		HealthText.text = Health + "/" + MaxHealth;

		GameObject.Find ("Money_text").GetComponent<Text> ().text = Cash.ToString();

	}

	public void InstantiateHabitant(int i){

		switch (i) {
		case 1:
			StartCoroutine("estanciar");
			break;
		case 2:
			StartCoroutine("estanciardr");
			break;
		default:
			//nada
			break;
		}

		}

	IEnumerator estanciar(){
		Instantiate (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
		MaxPop = MaxPop + 5;
		Pop = Pop + 5;
		yield return new WaitForSeconds (2f);
		Instantiate (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
		MaxPop = MaxPop + 5;
		Pop = Pop + 5;
	}

	IEnumerator estanciardr(){
		GameObject A = (GameObject)Instantiate (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
		A.GetComponent<HabitantMovement> ().character = 1;
		Health = Health + 5;
		yield return new WaitForSeconds (2f);
		A = (GameObject) Instantiate  (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
		A.GetComponent<HabitantMovement> ().character = 1;
		Health = Health + 5;	
	}


}
