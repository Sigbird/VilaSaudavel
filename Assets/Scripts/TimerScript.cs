using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {
	public static float timercount;
	private int monthcount;
	public static int days;
	public static bool month;
	public GameObject TimerUI;
	public Animator Alert;
	// Use this for initialization
	void Start () {
		days = 120;
		month = false;
	}
	
	// Update is called once per frame
	void Update () {
		timercount = timercount + Time.deltaTime;
		Alert.SetFloat ("Alert", days);	
		//Debug.Log (days);
		if (timercount >= 5) {
			Events.creatures = true;
			days--;
			monthcount++;
			timercount = 0;

		}

		if (monthcount > 30) {
			monthcount = 0;
			month = true;
		}

		if (days <= 0) {
			GameObject.Find("Events").GetComponent<Events>().GameOver();
		}

		TimerUI.GetComponent<Text> ().text = "Restam " + (int)days + " dias";
	}
}
