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

	public Text StatisticCoins;

	public Text StatisticCoinsGoal;

	public Text StatisticCoinsMount;

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

		if (GameObject.Find ("StatisticCash") != null)
			GameObject.Find ("StatisticCash").GetComponent<Text> ().text = Cash.ToString ();

		if (GameObject.Find ("StatisticCashGoal") != null)
			GameObject.Find ("StatisticCashGoal").GetComponent<Text> ().text = "1000";

		if (GameObject.Find ("StatisticCashTitle") != null)
			GameObject.Find ("StatisticCashTitle").GetComponent<Text> ().text = "A Vila gera " + ((Pop/10)*120).ToString() + " moedas por mês";

		if (GameObject.Find ("StatisticHealt") != null)
			GameObject.Find ("StatisticHealt").GetComponent<Text> ().text = "A Vila possui" + Health.ToString() + " de Saude";

	}

	public void InstantiateHabitant(int i){

		switch (i) {
		case 1:
			StartCoroutine("estanciar");
			break;
		case 2:
			StartCoroutine("estanciarAgente");
			break;
		case 3:
			StartCoroutine("estanciarDr");
			break;
		default:
			//nada
			break;
		}

		}

	IEnumerator estanciar(){
		GameObject A = (GameObject)Instantiate (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
		A.GetComponent<HabitantMovement> ().character = 0;
		MaxPop = MaxPop + 5;
		Pop = Pop + 5;
		yield return new WaitForSeconds (2f);
		A = (GameObject) Instantiate  (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
		A.GetComponent<HabitantMovement> ().character = 2;
		MaxPop = MaxPop + 5;
		Pop = Pop + 5;
	}

	IEnumerator estanciarAgente(){
		GameObject A = (GameObject)Instantiate (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
		A.GetComponent<HabitantMovement> ().character = 1;
		Health = Health + 5;
		yield return new WaitForSeconds (2f);
		A = (GameObject) Instantiate  (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
		A.GetComponent<HabitantMovement> ().character = 1;
		Health = Health + 5;	
	}

	IEnumerator estanciarDr(){
		GameObject A = (GameObject)Instantiate (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
		A.GetComponent<HabitantMovement> ().character = 3;
		Health = Health + 5;
		yield return new WaitForSeconds (2f);
		A = (GameObject) Instantiate  (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
		A.GetComponent<HabitantMovement> ().character = 3;
		Health = Health + 5;	
	}


}
