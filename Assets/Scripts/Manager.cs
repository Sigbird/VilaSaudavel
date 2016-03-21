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

	public Animator HealthSphere;

	// Use this for initialization
	void Start () {
		Cash = 100;

		MaxPop = 100;

		MaxHealth = 100;

		Health = 0;

		Pop = 0;
	}
	
	// Update is called once per frame
	void Update () {

		PopulationSphere.SetFloat("Value",((Pop / MaxPop) * 100));

		HealthSphere.SetFloat("Value",((Health / MaxHealth) * 100));

		GameObject.Find ("Money_text").GetComponent<Text> ().text = Cash.ToString();

	}
}
