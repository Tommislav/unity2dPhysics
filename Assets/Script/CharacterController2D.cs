using UnityEngine;
using System.Collections;


[RequireComponent (typeof (BoxCollider2D))]
public class CharacterController2D : MonoBehaviour {

	public LayerMask collisionMask;

	public CollisionState2D Collision { get { return _state; } }

	private BoxCollider2D _collider;
	private CollisionState2D _state;
	private Vector2 _velocity;

	private Rect _rect;
	private Vector2 _tl;
	private Vector2 _bl;
	private Vector2 _br;
	private Vector2 _tr;
	private float _skinWidth = 0.02f;



	void Start () {
		_state = new CollisionState2D();
		_collider = GetComponent<BoxCollider2D>();
		_velocity = new Vector2 ();
	}

	void LateUpdate () {

		PreCalcPos ();

		if (_velocity.x != 0) {
			int numChecksY = 5;
			_velocity.x = DoCollisionX((_velocity.x < 0 ? -1 : 1), numChecksY, _velocity.x);
		}

		int numChecksX = 3;
		_velocity.y = DoCollisionY ((_velocity.y <= 0 ? -1 : 1), numChecksX, _velocity.y);


		this.transform.Translate (_velocity.x, _velocity.y, 0f, Space.World);
	}



	private void PreCalcPos() {
		var localScale = this.transform.localScale;
		var size = new Vector3 (_collider.size.x * Mathf.Abs(localScale.x), _collider.size.y * Mathf.Abs(localScale.y), 2);
		var halfSize = size / 2f;
		var center = new Vector3 (_collider.center.x * localScale.x, _collider.center.y * localScale.y);
		var pos = new Vector2(transform.position.x, transform.position.y);

		_rect = new Rect (pos.x + center.x - halfSize.x, pos.y + center.y + halfSize.y, size.x, size.y);
		_tl = new Vector2 ( _rect.x + _skinWidth, _rect.y - _skinWidth );
		_tr = new Vector2 (_rect.x + _rect.width - _skinWidth, _rect.y - _skinWidth);
		_bl = new Vector2 (_rect.x + _skinWidth, _rect.y - _rect.height + _skinWidth);
		_br = new Vector2 (_rect.x + _rect.width - _skinWidth, _rect.y - _rect.height + _skinWidth);
	}



	private float DoCollisionY(int dir, int numChecks, float rayLength) {
		float nSkin = _skinWidth * dir;
		Vector2 checkFrom = (dir <= 0) ? _bl : _tl;
		var normalVec = new Vector2 (0, dir);
		float absLen = Mathf.Abs (rayLength);
		absLen += _skinWidth;

		float spacing = (_rect.width -  2 * _skinWidth) / (numChecks - 1);

		for (int i=0; i < numChecks; i++) {
			var from = new Vector2(checkFrom.x + (spacing*i), checkFrom.y);
			var rayHit = Physics2D.Raycast(from, normalVec, absLen, collisionMask);
			if (rayHit.collider) {
				var dist = Mathf.Abs(from.y - rayHit.point.y);
				if (dist < absLen) absLen = dist;
				Debug.DrawLine(new Vector3(from.x, from.y, 0f), new Vector3(rayHit.point.x, rayHit.point.y, 0f));
			} else {
				Debug.DrawLine(new Vector3(from.x, from.y, 0f), new Vector3(from.x, from.y + (absLen*dir), 0f));
			}

		}
		return (absLen-_skinWidth)*dir;
	}

	private float DoCollisionX(int dir, int numChecks, float rayLength) {
		Vector2 checkFrom = (dir < 0) ? _bl : _br;
		var normalVec = new Vector2 (dir, 0);
		var absLen = Mathf.Abs (rayLength) + _skinWidth;
		float spacing = (_rect.height - 2 * _skinWidth) / (numChecks - 1);

		for (int i=0; i < numChecks; i++) {
			var from = new Vector2(checkFrom.x, checkFrom.y + (i * spacing));
			var rayHit = Physics2D.Raycast(from, normalVec, absLen, collisionMask);
			if (rayHit) {
				var dist = Mathf.Abs(from.x - rayHit.point.x);
				if (dist < absLen) absLen = dist;
				Debug.DrawLine(new Vector3(from.x, from.y, 0f), new Vector3(rayHit.point.x, rayHit.point.y, 0f));
			} else {
				Debug.DrawLine(new Vector3(from.x, from.y, 0f), new Vector3(from.x+(absLen*dir), from.y, 0f));
			}
		}
		return (absLen-_skinWidth) * dir;
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
