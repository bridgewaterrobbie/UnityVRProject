using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
	void Update () {
		//Called right before rendering a frame, where most of our game code goes
	}

	public float speed;

	private int count;
	public Text countText;
	public Text winText;

	private Rigidbody rb;

	private void updateCountText() {
		countText.text = "Count: " + count.ToString ();
		if (count >= 26) {
			winText.text = "You win";
		}
		//Gotta get vcs set up for this directory soon.

	}

	void Start() {
		rb = GetComponent<Rigidbody> ();
		count = 0;
		updateCountText ();
		winText.text = "";

	}

	void FixedUpdate () {
		//called before performing any physics operations, where physics code goes
		//We use fixed update, as we our moving our ball by applying forces
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		float jump = 0.0f;
		if (Input.GetKey ("space")) {
			jump = 2.0f;
		}

		Vector3 movement = new Vector3 (moveHorizontal, jump, moveVertical);

		rb.AddForce (movement*speed);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) { 
			other.gameObject.SetActive (false);
			++count;
		}
		updateCountText ();

	}
}
