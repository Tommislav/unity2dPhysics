using UnityEngine;
using System.Collections;

public class GravityController : MonoBehaviour {


	public float GravityScale = 1.0f;
	public float MaxFallSpeed = 1.0f;

	private World _world;
	private CharacterController2D _characterController;
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

		_characterController = GetComponent<CharacterController2D> ();
	}

	void Update () 
	{
		if (!_characterController.Collision.IsOnGround) {
			_force.y += _world.gravity * GravityScale;
			if (_force.y < -MaxFallSpeed) _force.y = -MaxFallSpeed;
			_characterController.AddForce(_force, CharacterController2D.FORCE_MOVEMENT);
		} else {
			_force.y = 0f;
			_characterController.AddForce(_force, CharacterController2D.FORCE_MOVEMENT);
		}
	}


}
