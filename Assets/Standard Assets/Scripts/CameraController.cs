using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;

	private Vector3 offset;
	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position; //How far away we are from the player

	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = player.transform.position + offset; //We want to be offset away from whereever the player is. 
	}
}
