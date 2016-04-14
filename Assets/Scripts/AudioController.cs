using UnityEngine;
using System.Collections;

public class AudioController : MonoBehaviour {

	[System.Serializable]
	public enum EAudioState
	{
		
		Click,

		Building, // Is Placed on the map

		Background, // Is Being moved

		CantDo, // Can't Be Deleted / Moved
	
		Disease,

		Payment
	}


	private AudioSource Source;

	public AudioClip UIClick;
	public AudioClip building;
	public AudioClip backGroundSound;
	public AudioClip cantDoIt;
	public AudioClip disease;
	public AudioClip payment;
	//public EAudioState AudioState;

	// Use this for initialization
	void Start () {
		Source = GetComponent<AudioSource> ();
		Source.volume = 0.2f;
		//PlayState (EAudioState.Background);
		Source.clip = backGroundSound;
		Source.Play ();
		Source.loop = true;
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void PlayState(AudioController.EAudioState state){

		switch (state) {
		case EAudioState.Background:
			this.Source.PlayOneShot (this.backGroundSound);
			break;
		case EAudioState.Building:
			this.Source.PlayOneShot (this.building);
			break;
		case EAudioState.CantDo:
			this.Source.PlayOneShot (this.cantDoIt);
			break;
		case EAudioState.Click:
			this.Source.PlayOneShot (this.UIClick);
			break;
		case EAudioState.Disease:
			this.Source.PlayOneShot (this.disease);
			break;
		case EAudioState.Payment:
			this.Source.PlayOneShot (this.payment);
			break;
		default:
			print ("Incorrect Audio Source.");
			break;

		}
	}




}
