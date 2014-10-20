using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {


	public float GravityScale = 1.0f;
	public float MaxFallSpeed = 1.0f;

	private World _world;
	private MoveController2D _moveController;
	private Vector2 _force;

	void Start () 
	{
		_force = new Vector2();
		var worldGO = GameObject.FindWithTag ("world");
		if (worldGO == null) 
		{
			throw new UnityException ("Could not find tagged object 'world'");
		}
		_world = worldGO.GetComponent<World> ();
		if (_world == null) 
		{
			throw new UnityException ("could not find component World in game object world");
		}

		_moveController = GetComponent<MoveController2D> ();
	}

	void Update () 
	{
		if (!_moveController.Collision.IsOnGround) {
			_force.y += _world.gravity * GravityScale;
			if (_force.y < -MaxFallSpeed) _force.y = -MaxFallSpeed;
			_moveController.AddMoveVelocity(_force);
		} else {
			//_force.y = 0f;
			//_moveController.AddMoveVelocity(_force);
		}
	}


}
