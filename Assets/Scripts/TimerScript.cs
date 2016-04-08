using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour {
	public static float timercount;
	private int monthcount;
	public int days;
	public static bool month;
	public GameObject TimerUI;
	// Use this for initialization
	void Start () {
		days = 120;
		month = false;
	}
	
	// Update is called once per frame
	void Update () {
		timercount = timercount + Time.deltaTime;

		if (timercount >= 5) {
			Events.birds = true;
			days--;
			monthcount++;
			timercount = 0;

		}

		if (monthcount > 30) {
			monthcount = 0;
			month = true;
		}

		TimerUI.GetComponent<Text> ().text = "Restam " + (int)days + " dias";
	}
}
