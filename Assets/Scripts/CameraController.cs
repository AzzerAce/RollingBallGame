using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	public float cameraRotateSpeed;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		float moveHorizontal = Input.GetAxisRaw ("Horizontal");
		offset = Quaternion.AngleAxis (moveHorizontal * cameraRotateSpeed * Time.deltaTime, Vector3.up) * offset;

		Transform camPlace = transform;
		camPlace.position = player.transform.position + offset;
		camPlace.LookAt (player.transform.position);
		transform.position = camPlace.position;
		transform.rotation = camPlace.rotation;

		//transform.RotateAround(player.transform.position, Vector3.up, moveHorizontal * cameraRotateSpeed * Time.deltaTime);
	}
}
