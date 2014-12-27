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

		if (Input.GetKey (KeyCode.LeftArrow)) {
			characterController.OnKeyDown(KeyCode.LeftArrow);
		} 
		
		if (Input.GetKey (KeyCode.RightArrow)) {
			characterController.OnKeyDown(KeyCode.RightArrow);
		}


		if (Input.GetKey(KeyCode.UpArrow)) {
			characterController.OnKeyDown(KeyCode.UpArrow);
		} 
		if (Input.GetKey(KeyCode.DownArrow)) {
			characterController.OnKeyDown(KeyCode.DownArrow);
		}
		if (Input.GetKey(KeyCode.Space)) {
			characterController.OnKeyDown(KeyCode.Space);
		}
	}
}
