using UnityEngine;
using System.Collections;

public class HabitantMovement : MonoBehaviour {
	public float speed;
	public GameObject next;
	public Animator sprite;
	public SpriteRenderer renderer;
	public int character;

	//Statistics
	public int healt;
	public int age;
	public string name;
	public string info;
	public Sprite ilustracao;
	public Sprite Sprite0;
	public Sprite Sprite1;
	public Sprite Sprite2;
	public Sprite Sprite3;

	// Use this for initialization
	void Start () {
		next = GameObject.Find ("WayPoint");
		Events.DialogSequence = 2;
	}
	
	// Update is called once per frame
	void Update () {

		sprite.SetInteger ("Caracter", character);

		if (character == 1) {
			this.gameObject.tag = "Jaleco";
			this.name = "Rebeca";
			this.age = 20;
			this.healt = 100;
			this.info = "Agente de Saude";
			this.ilustracao = Sprite1;

		}

		if (character == 3) {

			this.name = "Jaleco";
			this.age = 30;
			this.healt = 100;
			this.info = "Doutor Jaleco";
			this.ilustracao = Sprite3;
		}
		if (character == 2) {
			
			this.name = "Vanessa";
			this.age = 20;
			this.healt = 100;
			this.info = "Cidada de vila Saudavel";
			this.ilustracao = Sprite2;
		}
		if (character == 0) {
			
			this.name = "Tony";
			this.age = 20;
			this.healt = 100;
			this.info = "Cidadao de vila Saudavel";
			this.ilustracao = Sprite0;
		}



//		if (Input.GetKeyDown (KeyCode.M))
//			sprite.SetInteger ("Caracter", 1);
//
//		if (Input.GetKeyDown (KeyCode.K))
//			sprite.SetInteger ("Caracter", 0);

//		this.transform.position = Vector3.MoveTowards(transform.position, next.transform.position, Time.deltaTime * speed);
//
//		if (Vector3.Distance (this.transform.position, next.transform.position) <= )

		FollowTargetWitouthRotation (this.next, Random.Range (0.1f, 0.3f), Time.deltaTime * speed);
		   
//		Debug.Log (GetComponent<Rigidbody> ().velocity);

		sprite.SetFloat ("Speedx", rigidbody.velocity.x);
		sprite.SetFloat ("Speedy", rigidbody.velocity.z);
	}

	void FollowTargetWitouthRotation(GameObject target, float distanceToStop, float speed)
	{
		var direction = Vector3.zero;
		if (Vector3.Distance (transform.position, target.transform.position) > distanceToStop) {
			direction = target.transform.position - transform.position;
			rigidbody.AddRelativeForce (direction.normalized * speed, ForceMode.Force);
		} else {
			this.next = next.GetComponent<Waypoint> ().NextPoint ();
			//FollowTargetWitouthRotation (this.next, Random.Range (0.1f, 0.3f), speed);
		}
	}

}
