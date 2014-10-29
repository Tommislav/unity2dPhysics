using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterController2D))]
public class MoveController2D : MonoBehaviour {

	
	public Vector2 MaxVelocity = new Vector2(1,1);
	public float MoveAcceleration = 0.1f;
	public float MoveFriction = 0.9f;
	public float JumpStrength = 1.0f;
	public float MaxFallSpeed = 4.0f;
	public float MaxWalkSpeed = 0.7f;
	public float GravityScale = 1.0f;
	public float JumpTime = 0.1f;




	private CharacterController2D _charController;
	private World _world;
	private Vector2 _moveVelocity;
	private Vector2 _externalForce;
	private Vector2 _velocity;

	private int _jumpPressedFrame;
	private int _leftPressedFrame;
	private int _rightPressedFrame;
	private bool _isJumping;
	private bool _canAddJumpForce;
	private float _jumpStartTime;


	public CollisionState2D Collision { get { return _charController.Collision; } }


	void Start () 
	{
		_charController = GetComponent<CharacterController2D>();
		_world = GameObject.FindWithTag ("world").GetComponent<World>();
		_moveVelocity = new Vector2();
		_externalForce = new Vector2();

	}



	public void SetMoveVelocity(Vector2 v)
	{
		_moveVelocity = v;
	}

	public void AddMoveVelocity(Vector2 v)
	{
		_moveVelocity += v;
	}

	public Vector2 GetMoveVelocity()
	{
		return _moveVelocity;
	}

	public void SetExternalForce(Vector2 v) {
		_externalForce = v;
	}


	void Update () 
	{
		ApplyGravity();
		ApplyFriction();
		ApplyMoveConstraints();
		_velocity = _moveVelocity + _externalForce;
		ClampVector(ref _velocity, MaxVelocity);
		_charController.SetVelocity(_velocity);
	}

	private void ClampVector(ref Vector2 value, Vector2 clamp) 
	{
		if (value.x < -clamp.x) { value.x = -clamp.x;}
		else if (value.x > clamp.x) { value.x = clamp.x; }

		if (value.y < -clamp.y) { value.y = -clamp.y; }
		else if (value.y > clamp.y) { value.y = clamp.y; }
	}




	public void MoveLeft()
	{
		_leftPressedFrame = Time.frameCount;
		_moveVelocity.x -= MoveAcceleration;
	}

	public void MoveRight()
	{
		_rightPressedFrame = Time.frameCount;
		_moveVelocity.x += MoveAcceleration;
	}

	public void Jump()
	{
		float normalizedGravity = _world.gravity < 0 ? -1 : 1;
		int frameCount = Time.frameCount;
		bool jumpKeyDown = isPressedSinceLast(_jumpPressedFrame);
		bool canJump = Collision.IsOnGround;
		float now = Time.realtimeSinceStartup;
		_jumpPressedFrame = frameCount;

		if(!jumpKeyDown && canJump) 
		{
			_jumpStartTime = now;
			_moveVelocity.y = 0.0f;
		}

		bool canAddJumpForce = now - _jumpStartTime <= JumpTime;

		if (canAddJumpForce)
		{
			_moveVelocity.y += JumpStrength * normalizedGravity;
		}
	}

	private bool isPressedSinceLast(int when) {
		int lastFrame = Time.frameCount - 1;
		return (when >= lastFrame);
	}

	protected void ApplyFriction() {
		float ALMOST_NO_MOVEMENT = 0.01f;
		float STANDING_STILL = 0.0f;
		if (_moveVelocity.x < ALMOST_NO_MOVEMENT && _moveVelocity.x > -ALMOST_NO_MOVEMENT) {
			_moveVelocity.x = STANDING_STILL;
		}

		if (_moveVelocity.x == STANDING_STILL) {
			return;
		}

		if (!isPressedSinceLast(_leftPressedFrame) && !isPressedSinceLast(_rightPressedFrame)) {
			_moveVelocity.x *= MoveFriction;
		}
	}

	protected void ApplyGravity() {
		float gravity = _world.gravity;
		if (GravityScale == 0.0f) {
			return;
		}

		if (GravityScale != 1.0f) {
			gravity *= GravityScale;
		}

		_moveVelocity.y -= gravity;

		if (Collision.IsOnGround && _moveVelocity.y < 0f) {
			_moveVelocity.y = 0f;
		}
	}


	void ApplyMoveConstraints ()
	{
		if (_moveVelocity.y < MaxFallSpeed) {
			_moveVelocity.y = MaxFallSpeed;
		}

		if (_moveVelocity.x > MaxWalkSpeed) { _moveVelocity.x = MaxWalkSpeed; }
		if (_moveVelocity.x < -MaxWalkSpeed) { _moveVelocity.x = -MaxWalkSpeed; }
	}
}
