using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpSpeed;
	public float maxSpeed;
	public Camera cam;
	public Text introText;
	public Text countText;
	public Text winText;

	private Rigidbody rb;
	private int count;
	private int maxCount;
	private float moveVertical;
	private bool jump;

	void Start() {
		rb = GetComponent<Rigidbody> ();
		count = 0;
		maxCount = GameObject.FindGameObjectsWithTag ("Pick Up").Length;
		SetCountText ();
		introText.text = "W,A,S,D - move \r\n Space - jump \r\n R - reset \r\n Pick up the coins!";
		winText.text = "";
		moveVertical = 0.0f;
		jump = true;
	}

	void FixedUpdate () {
		//float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		moveVertical = Input.GetAxisRaw ("Vertical");
		Vector3 mov = ((transform.position - cam.transform.position) * moveVertical).normalized;
		mov.y = 0.0f;
		mov = mov * speed * Time.deltaTime;

		if (rb.velocity.magnitude < maxSpeed) {
			rb.AddForce (mov);
		}

		//rb.MovePosition(transform.position + mov);

		if (Input.GetKeyDown (KeyCode.Space) && !jump) {
			rb.AddForce (new Vector3 (0.0f, jumpSpeed, 0.0f));
			jump = true;
		}

	}

	void Update() {
		if (Input.GetKey (KeyCode.R)) {
			RestartScene ();
		}

		if (moveVertical == 0) {
			float x = rb.velocity.x / 10;
			float z = rb.velocity.z / 10;

			rb.velocity = new Vector3 (x, rb.velocity.y, z);
		}

		if (transform.position.y < -20.0f) {
			rb.transform.position = Vector3.zero;
			rb.velocity = Vector3.zero;
		}

		if (Input.anyKey) {
			GameObject[] go = GameObject.FindGameObjectsWithTag ("Intro");
			foreach (GameObject obj in go) {
				obj.SetActive(false);
			}
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag ("Pick Up")) {
			other.gameObject.SetActive (false);
			count++;
			SetCountText ();
		}
	}

	void OnCollisionEnter(Collision col) {
		jump = false;
	}

	void SetCountText() {
		countText.text = "Coins: " + count.ToString () + "/" + maxCount.ToString ();
		if (count >= maxCount) {
			winText.text = "You Win! \r\n \r\n Press 'R' to restart";
		}
	}

	void RestartScene() {
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}
}
