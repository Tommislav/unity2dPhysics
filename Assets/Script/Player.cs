using UnityEngine;
using System.Collections;



public class Player : MonoBehaviour {

	[Range (-10f,10f)]
	public float Gravity = 2.0f;



	private CharacterController2D _controller;




	// Use this for initialization
	void Start () {
		_controller = GetComponent<CharacterController2D>();
	}
	
	// Update is called once per frame
	void Update () {

		float vx = 0f;
		float vy = 0f;

		if (Input.GetKey (KeyCode.LeftArrow)) {
			vx = -0.1f;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			vx = 0.1f;
		} else {
			vx = 0;
		}


		if (Input.GetKey (KeyCode.UpArrow)) {
				vy = 0.1f;
		} else if (Input.GetKey (KeyCode.DownArrow)) {
				vy = -0.1f;
		} else {
				vy = 0f;
		}

		if (Input.GetKey (KeyCode.LeftShift)) {
			vx *= 10f;
			vy *= 10f;
		}

		_controller.SetForceTo(vx,vy);

	}
}
