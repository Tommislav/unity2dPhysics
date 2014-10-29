using UnityEngine;
using System.Collections;

public class TreadmillController : MonoBehaviour {

	public float movement = 0.05f;

	private GameObject _collidingObject;

	void Update () {
		if (_collidingObject != null) {
			MoveController2D move = _collidingObject.GetComponent<MoveController2D>();
			move.SetExternalForce(new Vector2(movement, 0));
			//if (move.Collision.IsOnGround) {
			//	move.AddMoveVelocity(new Vector2(movement, 0));
			//}

		}
	}


	public void CharacterController2dEnterY(GameObject colliding) 
	{
		_collidingObject = colliding;
	}

	public void CharacterController2dExitY(GameObject colliding) 
	{
		MoveController2D move = _collidingObject.GetComponent<MoveController2D>();
		move.SetExternalForce(new Vector2(0, 0));
		_collidingObject = null;
	}
}
