using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour {

	public int habitants;

	private int houses;


	// Cash
	public static int Cash;

	public static int CashGoal;

	//Pop
	public static float Pop;

	public static float MaxPop;

	//Health
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

		houses = 0;

		if (PlayerPrefs.HasKey ("fase")) {
			switch (PlayerPrefs.GetInt ("fase")) {
			case 0:

				Cash = 100;
				CashGoal = 300;

				Health = 100;
				MaxHealth = 200;

				Pop = 0;
				MaxPop = 30;
				break;
			case 1:

				Cash = 100;
				CashGoal = 500;
				
				Health = 100;
				MaxHealth = 300;
				
				Pop = 0;
				MaxPop = 40;
				break;
			case 2:

				Cash = 100;
				CashGoal = 700;
				
				Health = 100;
				MaxHealth = 400;
				
				Pop = 0;
				MaxPop = 50;
				break;
			default:
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Cash >= 300 && Pop >= 30 && Health >= 200 && PlayerPrefs.GetInt ("fase") == 0) {
			GameObject.Find("Events").GetComponent<Events>().Victory();
			//PlayerPrefs.SetInt ("fase", Events.FaseControler + 1);
		}else if (Cash >= 500 && Pop >= 40 && Health >= 300 && PlayerPrefs.GetInt ("fase") == 1) {
			GameObject.Find("Events").GetComponent<Events>().Victory();
			//PlayerPrefs.SetInt ("fase", Events.FaseControler + 1);
		}else if (Cash >= 700 && Pop >= 50 && Health >= 400 && PlayerPrefs.GetInt ("fase") == 2) {
			GameObject.Find("Events").GetComponent<Events>().Victory();
			//PlayerPrefs.SetInt ("fase", Events.FaseControler + 1);
		}

		if (habitants >= 25) {
			Destroy( GameObject.FindGameObjectsWithTag("Habitant")[0]);
			habitants--;
		}


		if (Input.GetKey (KeyCode.Space))
			Cash++;

		Health = (houses - ContaminadeHouses ())*100;

		MaxHealth = houses *100;

		PopulationSphere.SetFloat("Value",((Pop / MaxPop) * 100));
		PopulationText.text = Pop + "/" + MaxPop;

		HealthSphere.SetFloat("Value",((Health / MaxHealth) * 100));
		HealthText.text = (Health) + "/" + (MaxHealth);

		GameObject.Find ("Money_text").GetComponent<Text> ().text = Cash.ToString();

		if (GameObject.Find ("VolumeSlider") != null)
			AudioListener.volume = GameObject.Find ("VolumeSlider").GetComponent<Slider> ().value;

		if (GameObject.Find ("GoalsTitle") != null)
			GameObject.Find ("GoalsTitle").GetComponent<Text> ().text = "Restam " + TimerScript.days + " Dias";

		if (GameObject.Find ("HouseQtd1") != null)
			GameObject.Find ("HouseQtd1").GetComponent<Text> ().text = GameObject.FindGameObjectsWithTag("Building").Length.ToString();

		if (GameObject.Find ("HouseBonusCash1") != null)
			GameObject.Find ("HouseBonusCash1").GetComponent<Text> ().text = "+" + (GameObject.FindGameObjectsWithTag ("Building").Length * 120).ToString ();

		if (GameObject.Find ("HouseBonusHealt1") != null)
			GameObject.Find ("HouseBonusHealt1").GetComponent<Text> ().text = "+" + Health.ToString ();

		if (GameObject.Find ("HouseBonusPop1") != null)
			GameObject.Find ("HouseBonusPop1").GetComponent<Text> ().text = "+" + (GameObject.FindGameObjectsWithTag ("Building").Length * 10).ToString ();

		//Cash Statistic
		if (GameObject.Find ("StatisticCash") != null)
			GameObject.Find ("StatisticCash").GetComponent<Text> ().text = Cash.ToString ();

		if (GameObject.Find ("StatisticCashGoal") != null)
			GameObject.Find ("StatisticCashGoal").GetComponent<Text> ().text = CashGoal.ToString();

		if (GameObject.Find ("StatisticCashTitle") != null)
			GameObject.Find ("StatisticCashTitle").GetComponent<Text> ().text = "A Vila gera " + ((Pop/10)*120).ToString() + " moedas por mês";

		if (GameObject.Find ("CashStatBar") != null)
			GameObject.Find ("CashStatBar").GetComponent<Animator> ().SetFloat ("Value", Cash);



		// Healt Statistic
		if (GameObject.Find ("StatisticHealt") != null)
			GameObject.Find ("StatisticHealt").GetComponent<Text> ().text = Health.ToString ();
		
		if (GameObject.Find ("StatisticHealtGoal") != null)
			GameObject.Find ("StatisticHealtGoal").GetComponent<Text> ().text = MaxHealth.ToString ();

		if (GameObject.Find ("StatisticHealtTitle") != null)
			GameObject.Find ("StatisticHealtTitle").GetComponent<Text> ().text = "A Vila possui " + Health.ToString() + " de Saude";

		if (GameObject.Find ("HealtStatBar") != null)
			GameObject.Find ("HealtStatBar").GetComponent<Animator> ().SetFloat ("Value", Health);


		// Population Statistic
		if (GameObject.Find ("StatisticPop") != null)
			GameObject.Find ("StatisticPop").GetComponent<Text> ().text = Pop.ToString ();
		
		if (GameObject.Find ("StatisticPopGoal") != null)
			GameObject.Find ("StatisticPopGoal").GetComponent<Text> ().text = MaxPop.ToString ();
		
		if (GameObject.Find ("StatisticPopTitle") != null)
			GameObject.Find ("StatisticPopTitle").GetComponent<Text> ().text = "A Vila possui " + Health.ToString() + " de Saude";
	
		if (GameObject.Find ("PopStatBar") != null)
			GameObject.Find ("PopStatBar").GetComponent<Animator> ().SetFloat ("Value", Pop);

	}

//	public void Testar(){
//		GameObject[] listacasas;
//		if (GameObject.FindGameObjectsWithTag ("Building") != null) {
//			listacasas = GameObject.FindGameObjectsWithTag ("Building");
//			if (GameObject.Find ("BuildingDetails1") != null) {
//				GameObject.Find ("BuildingDetails1").GetComponent<DialogInfoPanel> ().Renda.text = listacasas [1].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().renda.ToString ();
//				GameObject.Find ("BuildingDetails1").GetComponent<DialogInfoPanel> ().Descricao.text = listacasas [1].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().descriçao;
//				GameObject.Find ("BuildingDetails1").GetComponent<DialogInfoPanel> ().ilustracao.sprite = listacasas [1].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().ilustracao;
//			}
//			if (GameObject.Find ("BuildingDetails2") != null) {
//				GameObject.Find ("BuildingDetails2").GetComponent<DialogInfoPanel> ().Renda.text = listacasas [2].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().renda.ToString ();
//				GameObject.Find ("BuildingDetails2").GetComponent<DialogInfoPanel> ().Descricao.text = listacasas [2].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().descriçao;
//				GameObject.Find ("BuildingDetails2").GetComponent<DialogInfoPanel> ().ilustracao.sprite = listacasas [2].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().ilustracao;
//			}
//			if (GameObject.Find ("BuildingDetails3") != null) {
//				GameObject.Find ("BuildingDetails3").GetComponent<DialogInfoPanel> ().Renda.text = listacasas [3].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().renda.ToString ();
//				GameObject.Find ("BuildingDetails3").GetComponent<DialogInfoPanel> ().Descricao.text = listacasas [3].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().descriçao;
//				GameObject.Find ("BuildingDetails3").GetComponent<DialogInfoPanel> ().ilustracao.sprite = listacasas [3].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().ilustracao;
//			}
//			if (GameObject.Find ("BuildingDetails4") != null) {
//				GameObject.Find ("BuildingDetails4").GetComponent<DialogInfoPanel> ().Renda.text = listacasas [4].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().renda.ToString ();
//				GameObject.Find ("BuildingDetails4").GetComponent<DialogInfoPanel> ().Descricao.text = listacasas [4].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().descriçao;
//				GameObject.Find ("BuildingDetails4").GetComponent<DialogInfoPanel> ().ilustracao.sprite = listacasas [4].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().ilustracao;
//			}
//			if (GameObject.Find ("BuildingDetails5") != null) {
//				GameObject.Find ("BuildingDetails5").GetComponent<DialogInfoPanel> ().Renda.text = listacasas [5].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().renda.ToString ();
//				GameObject.Find ("BuildingDetails5").GetComponent<DialogInfoPanel> ().Descricao.text = listacasas [5].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().descriçao;
//				GameObject.Find ("BuildingDetails5").GetComponent<DialogInfoPanel> ().ilustracao.sprite = listacasas [5].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().ilustracao;
//			}
//			if (GameObject.Find ("BuildingDetails6") != null) {
//				GameObject.Find ("BuildingDetails6").GetComponent<DialogInfoPanel> ().Renda.text = listacasas [6].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().renda.ToString ();
//				GameObject.Find ("BuildingDetails6").GetComponent<DialogInfoPanel> ().Descricao.text = listacasas [6].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().descriçao;
//				GameObject.Find ("BuildingDetails6").GetComponent<DialogInfoPanel> ().ilustracao.sprite = listacasas [6].GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo> ().ilustracao;
//			}
//		}
//	}

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
		houses++;
		habitants = habitants + 2;
		GameObject A = (GameObject)Instantiate (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
		A.GetComponent<HabitantMovement> ().character = 0;
		//MaxPop = MaxPop + 5;
		Pop = Pop + 5;
		yield return new WaitForSeconds (2f);
		A = (GameObject) Instantiate  (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
		A.GetComponent<HabitantMovement> ().character = 2;
		///MaxPop = MaxPop + 5;
		Pop = Pop + 5;
	}

	IEnumerator estanciarAgente(){
		habitants = habitants + 1;
		GameObject A = (GameObject)Instantiate (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
		A.GetComponent<HabitantMovement> ().character = 1;
		Health = Health + 10;
		yield return new WaitForSeconds (1f);
		Events.DialogSequence = 4;
//		A = (GameObject) Instantiate  (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
//		A.GetComponent<HabitantMovement> ().character = 1;
//		Health = Health + 5;	
	}

	IEnumerator estanciarDr(){
		habitants = habitants + 2;
		GameObject A = (GameObject)Instantiate (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
		A.GetComponent<HabitantMovement> ().character = 3;
		Health = Health + 5;
		yield return new WaitForSeconds (2f);
		A = (GameObject) Instantiate  (Habitant, RespawnHabitant.transform.position, Quaternion.identity);
		A.GetComponent<HabitantMovement> ().character = 3;
		Health = Health + 5;	
	}

	public int ContaminadeHouses(){
		int x = 0;
		foreach (GameObject n in GameObject.FindGameObjectsWithTag("Building")) {
			if(n.GetComponent<YupiStudios.VilaSaudavel.Tiles.Buildings.TileBuildingInfo>().contaminada == true ){
				x++;
			}
		}
		return x;
	}

}
