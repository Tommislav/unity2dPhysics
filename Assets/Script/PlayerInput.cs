using UnityEngine;
using System.Collections;
using Assets.Script.CharacterController2D;



public class PlayerInput : MonoBehaviour {

	//private MoveController2D _controller;
	public Assets.Script.CharacterController2D.AbsCharacterController characterController;

	void Start () 
	{
		//_controller = GetComponent<MoveController2D>();
	}
	

	void Update () 
	{

		float horis = Input.GetAxis("Horizontal");
		float vert = Input.GetAxis("Vertical");

		float threshold = 0.1f;

		if (horis < -threshold || Input.GetKey(KeyCode.LeftArrow)) {
			characterController.OnKeyDown(JoypadCode.LEFT);
		}
		if (horis > threshold || Input.GetKey(KeyCode.RightArrow)) {
			characterController.OnKeyDown(JoypadCode.RIGHT);
		}


		if (vert > threshold || Input.GetKey(KeyCode.UpArrow)) {
			characterController.OnKeyDown(JoypadCode.UP);
		}
		if (vert < -threshold || Input.GetKey(KeyCode.DownArrow)) {
			characterController.OnKeyDown(JoypadCode.DOWN);
		}
		if (Input.GetButton("Jump")) {
			characterController.OnKeyDown(JoypadCode.JUMP);
		}
	}
}
