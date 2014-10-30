using UnityEngine;
using System.Collections;



public class PlayerInput : MonoBehaviour {

	private MoveController2D _controller;

	void Start () 
	{
		_controller = GetComponent<MoveController2D>();
	}
	

	void Update () 
	{

		if (Input.GetKey (KeyCode.LeftArrow)) {
			_controller.MoveLeft();
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			_controller.MoveRight();
		}

		if (Input.GetKey (KeyCode.UpArrow)) {
			_controller.Jump();
		}

		if (Input.GetKey(KeyCode.UpArrow)) {
			_controller.ClimbUp();
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			_controller.ClimbDown();
		}
	}
}
