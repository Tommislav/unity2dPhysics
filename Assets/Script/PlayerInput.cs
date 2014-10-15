using UnityEngine;
using System.Collections;



public class PlayerInput : MonoBehaviour {

	public float MaxSpeed = 3.0f;
	public float Acceleration = 1.0f;
	public float Friction = 0.8f;
	public float JumpStrength = 1.0f;
	public float MaxFallSpeed = 4.0f;


	private CharacterController2D _controller;
	private Vector2 _movement;

	private bool _isJumping;
	private float _jumpTime;
	private bool _canAddJumpForce;



	// Use this for initialization
	void Start () {
		_controller = GetComponent<CharacterController2D>();
		_movement = new Vector2();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKey (KeyCode.LeftArrow)) {
			_movement.x -= Acceleration;
		} else if (Input.GetKey (KeyCode.RightArrow)) {
			_movement.x += Acceleration;
		} else {
			_movement.x *= Friction;
		}


		_movement.y = 0;
		if (Input.GetKey (KeyCode.UpArrow)) {
			Debug.Log("on ground: " + _controller.Collision.IsOnGround);
			if (!_isJumping && _controller.Collision.IsOnGround) {
				_isJumping = true;
				_canAddJumpForce = true;
				_jumpTime = Time.realtimeSinceStartup;
			}

			_canAddJumpForce = (Time.realtimeSinceStartup - _jumpTime <= 0.1f);

			if (_canAddJumpForce) {
				_movement.y = JumpStrength;
			}
		} else {
			if (_controller.Collision.IsOnGround) {
				_isJumping = false;
			}
		}
		/*
		} else if (Input.GetKey (KeyCode.DownArrow)) {
				_movement.y = -0.1f;
		} else {
				_movement.y = 0f;
		}
		*/

		if (Input.GetKey (KeyCode.LeftShift)) {
			_movement.x *= 2f;
		}

		ClampMovement();
		_controller.AddForce(_movement, CharacterController2D.FORCE_MOVEMENT);

	}

	private void ClampMovement() {
		if (_movement.x < -MaxSpeed) _movement.x = -MaxSpeed;
		if (_movement.x > MaxSpeed) _movement.x = MaxSpeed;
		if (_movement.y < -MaxFallSpeed) _movement.y = -MaxFallSpeed;
	}
}
