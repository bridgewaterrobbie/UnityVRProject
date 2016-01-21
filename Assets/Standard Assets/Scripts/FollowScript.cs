using UnityEngine;
using System.Collections;
//Modified from
//http://answers.unity3d.com/questions/26177/how-to-create-a-basic-follow-ai.html
public class FollowScript : MonoBehaviour {
	public Transform target; //the enemy's target
	public int moveSpeed = 10; //move speed
	public int rotationSpeed = 3; //speed of turning
	public bool flying = true; //If gravity is on, this is genrally move like jumping as long as movement speed is less than 10ish
	public float minDistanceFollow = .5f;
	public float maxDistanceFollow = 100f;

	public Transform myTransform; //current transform data of this enemy

	void Awake()
	{
		myTransform = transform; //cache transform data for easy access/preformance
	}

	void Start()
	{
		target = GameObject.FindWithTag("Player").transform; //target the player

	}

	void Update () {
		float distance = Vector3.Distance (target.position, myTransform.position);
		if ((distance > maxDistanceFollow || distance < minDistanceFollow)) {
			return;
		}
		Vector3 targetPosition = target.position;
		Vector3 movement = myTransform.forward;
		if (flying == false) {
			movement.y = 0;
			targetPosition.y = 0;
		}
		//rotate to look at the player
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation,
			Quaternion.LookRotation(targetPosition - myTransform.position), rotationSpeed*Time.deltaTime);

		//move towards the player
		myTransform.position += movement * moveSpeed * Time.deltaTime;


	}
}
