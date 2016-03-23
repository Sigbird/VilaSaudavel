using UnityEngine;
using System.Collections;

public class HabitantMovement : MonoBehaviour {
	public float speed;
	public GameObject next;
	public Animator sprite;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

//		this.transform.position = Vector3.MoveTowards(transform.position, next.transform.position, Time.deltaTime * speed);
//
//		if (Vector3.Distance (this.transform.position, next.transform.position) <= )

		FollowTargetWitouthRotation (this.next, Random.Range (0.1f, 0.3f), speed);
		   
		Debug.Log (GetComponent<Rigidbody> ().velocity);

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
