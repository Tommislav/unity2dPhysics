using UnityEngine;
using System.Collections;


[RequireComponent (typeof (BoxCollider2D))]
public class CharacterController2D : MonoBehaviour {

	public LayerMask collisionMask;

	public CollisionState2D Collision { get { return _state; } }

	private BoxCollider2D _collider;
	private CollisionState2D _state;


	private Vector2 _velocity;


	void Start () {
		_state = new CollisionState2D();
		_collider = GetComponent<BoxCollider2D>();
		_velocity = new Vector2 ();
	}

	void LateUpdate () {

		var localScale = this.transform.localScale;
		var size = new Vector3 (_collider.size.x * Mathf.Abs(localScale.x), _collider.size.y * Mathf.Abs(localScale.y), 2);
		var halfSize = size / 2f;
		var center = new Vector3 (_collider.center.x * localScale.x, _collider.center.y * localScale.y);
		var pos = new Vector2(transform.position.x, transform.position.y);

		var bottomLeft = new Vector2 (pos.x + center.x - halfSize.x, pos.y + center.y - halfSize.y);
		var topLeft = new Vector2 (pos.x + center.x - halfSize.x, pos.y + center.y + halfSize.y);


		float spacing = size.x / 2;

		if (_velocity.y <= 0f) {
				_velocity.y = DoCollisionY (bottomLeft, 3, spacing, _velocity.y);
		} else {
				_velocity.y = DoCollisionY (topLeft, 3, spacing, _velocity.y);
		}



		this.transform.Translate (_velocity.x, _velocity.y, 0f, Space.World);
	}


	private float DoCollisionY(Vector2 startLeft, int numChecks, float spacing, float rayLength) {
		float normalY = (rayLength < 0) ? -1 : 1;
		var normalVec = new Vector2 (0, normalY);
		float absLen = Mathf.Abs (rayLength);

		for (int i=0; i < numChecks; i++) {
			var from = new Vector2(startLeft.x + (spacing*i), startLeft.y);
			var rayHit = Physics2D.Raycast(from, normalVec, absLen, collisionMask);
			if (rayHit.collider) {
				var dist = Mathf.Abs(from.y - rayHit.point.y);
				if (dist < absLen) absLen = dist;
				Debug.DrawLine(new Vector3(from.x, from.y, 0f), new Vector3(rayHit.point.x, rayHit.point.y, 0f));
			} else {
				Debug.DrawLine(new Vector3(from.x, from.y, 0f), new Vector3(from.x, from.y + (absLen*normalY), 0f));
			}

		}
		return (absLen*normalY);
	}





	public void AddForce(float x, float y) {
	}

	public void SetForceTo(float x, float y) {
		_velocity = new Vector2 (x, y);
	}

	public void MoveBy(float x, float y) {
	}

	public void MoveTo(float x, float y) {
	}



}
