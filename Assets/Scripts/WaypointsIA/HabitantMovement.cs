using UnityEngine;
using System.Collections;

public class HabitantMovement : MonoBehaviour {
	public float speed;
	public GameObject next;
	public Animator sprite;
	public Animator notifications;
	public SpriteRenderer renderer;
	public int character;
	private float timer;
	public float percentage;

	//Statistics
	public int healt;
	public int age;
	public string name;
	public string info;
	public string status;
	public Sprite ilustracao;
	public bool contaminado;
	private bool contaminadocd;
	public Sprite Sprite0;
	public Sprite Sprite1;
	public Sprite Sprite2;
	public Sprite Sprite3;

	// Use this for initialization
	void Start () {
		next = GameObject.Find ("WayPoint");
		Events.DialogSequence = 2;
		this.healt = 100;
		contaminadocd = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (contaminado == true && contaminadocd == true) {
			StartCoroutine("Sickness");
			contaminadocd = false;
		}

		timer = timer + Time.deltaTime;
		
		if (timer >= 5) {

			if (this.character == 0 || this.character == 2) {
				Teste ();
			}
			timer = 0;
		}

		sprite.SetInteger ("Caracter", character);

		if (character == 1) {
			this.gameObject.tag = "Jaleco";
			this.name = "Rebeca";
			this.age = 20;
			this.info = "Pronta para Servir aos Moradores!";
			if(contaminado){
				this.status = "Contaminado";
			}else{
				this.status = "Agente";
			}
			this.ilustracao = Sprite1;

		}

		if (character == 3) {

			this.name = "Jaleco";
			this.age = 30;
			this.info = "Saude em primeiro lugar!";
			if(contaminado){
				this.status = "Contaminado";
			}else{
				this.status = "Doutor";
			}
			this.ilustracao = Sprite3;
		}
		if (character == 2) {
			
			this.name = "Vanessa";
			this.age = 20;
			if(contaminado){
				this.info = "Estou Doente preciso de atendimento Medico!";
				this.status = "Contaminado";
			}else{
				this.info = "Ola, sou nova moradora, que alegria!";
				this.status = "Moradora";
			}
			this.ilustracao = Sprite2;
		}
		if (character == 0) {
			
			this.name = "Tony";
			this.age = 20;
			if(contaminado){
				this.info = "Estou Doente preciso de atendimento Medico!";
				this.status = "Contaminado";
			}else{
				this.info = "Ola sou novo morador, que alegria!";
				this.status = "Morador";
			}
			this.ilustracao = Sprite0;
		}

//		if (TimerScript.month == true) {
//			this.percentage = this.percentage + 10;
//			TimerScript.month = false;
//		}

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

	public void Teste(){
		
		float x = Random.value;
		this.healt = 100 - (int)percentage ;
		if (x <= percentage / 100 && this.contaminado == false) {
			//Debug.Log ("contaminou");
			notifications.SetBool("Exclamation", true);

			//GameObject.Find ("Main Camera").GetComponent<Teste> ().infeccão++;
			this.contaminado = true;
		}
		
	}

	IEnumerator Sickness(){
		if (contaminado) {
			renderer.color = Color.magenta;
			yield return new WaitForSeconds (1);
			renderer.color = Color.white;
			yield return new WaitForSeconds (1);
			StartCoroutine("Sickness");
		} 

	
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
