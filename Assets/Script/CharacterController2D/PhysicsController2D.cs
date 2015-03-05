using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[RequireComponent (typeof (BoxCollider2D))]
public class PhysicsController2D : MonoBehaviour {


	public LayerMask collisionMask;
	public LayerMask cloudCollisionMask;
	private LayerMask _downwardLayerMask;



	public CollisionInfo Collision { get { return _state; } }
	public int NumRaysX = 3;
	public int NumRaysY = 5;


	private BoxCollider2D _collider;
	private CollisionInfo _state;

	private Rect _rect;
	private Vector2 _tl;
	private Vector2 _bl;
	private Vector2 _br;
	//private Vector2 _tr;
	private float _skinWidth = 0.02f;

	private GameObject _hitX;
	private GameObject _hitY;
	private GameObject _lastHitX;
	private GameObject _lastHitY;
	
	private Vector2 _velocity;
	private bool _disableCollision;
	private bool _disableCloudCollision;

	void Start () {
		_state = getCollisionStateComponent();
		_collider = GetComponent<BoxCollider2D>();
		_velocity = new Vector2();

		_downwardLayerMask = new LayerMask();
		_downwardLayerMask.value = collisionMask.value | cloudCollisionMask.value;
	}

	private CollisionInfo getCollisionStateComponent() {
		if (gameObject.GetComponent<CollisionInfoComponent>() == null) {
			gameObject.AddComponent<CollisionInfoComponent>();
		}
		return gameObject.GetComponent<CollisionInfoComponent>().CollisionState;
	}

	void LateUpdate () {

		_state.Reset();
		PreCalcPos ();
		PrepareHitInfo();

		if (!_disableCollision) {
			if (_velocity.x != 0) {
				_velocity.x = DoCollisionX((_velocity.x < 0 ? -1 : 1), NumRaysY, _velocity.x);
			}

			_velocity.y = DoCollisionY((_velocity.y <= 0 ? -1 : 1), NumRaysX, _velocity.y);
		}
		

		HandleHitInfo();

		MoveBy(_velocity.x, _velocity.y);
	}

	private void PreCalcPos() {
		var localScale = this.transform.localScale;
		var size = new Vector3 (_collider.size.x * Mathf.Abs(localScale.x), _collider.size.y * Mathf.Abs(localScale.y), 2);
		var halfSize = size / 2f;
		var center = new Vector3 (_collider.offset.x * localScale.x, _collider.offset.y * localScale.y);
		var pos = new Vector2(transform.position.x, transform.position.y);

		_rect = new Rect (pos.x + center.x - halfSize.x, pos.y + center.y + halfSize.y, size.x, size.y);
		_tl = new Vector2 ( _rect.x + _skinWidth, _rect.y - _skinWidth );
		//_tr = new Vector2 (_rect.x + _rect.width - _skinWidth, _rect.y - _skinWidth);
		_bl = new Vector2 (_rect.x + _skinWidth, _rect.y - _rect.height + _skinWidth);
		_br = new Vector2 (_rect.x + _rect.width - _skinWidth, _rect.y - _rect.height + _skinWidth);
	}



	private float DoCollisionY(int dir, int numChecks, float rayLength) {
		bool down = dir <= 0;
		Vector2 checkFrom = (down) ? _bl : _tl;
		var normalVec = new Vector2 (0, dir);
		float absLen = Mathf.Abs (rayLength);
		absLen += _skinWidth;

		float spacing = (_rect.width -  2 * _skinWidth) / (numChecks - 1);
		LayerMask collY = (down && !_disableCloudCollision) ? _downwardLayerMask : collisionMask;


		for (int i=0; i < numChecks; i++) {
			var from = new Vector2(checkFrom.x + (spacing*i), checkFrom.y);
			var rayHit = Physics2D.Raycast(from, normalVec, absLen, collY);
			if (rayHit) {
				var dist = Mathf.Abs(from.y - rayHit.point.y);
				if (dist < absLen) absLen = dist;
				_state.SetCollisionY(dir);
				Debug.DrawLine(new Vector3(from.x, from.y, 0f), new Vector3(rayHit.point.x, rayHit.point.y, 0f));

				_hitY = rayHit.collider.gameObject;
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
				_state.SetCollisionX(dir);
				Debug.DrawLine(new Vector3(from.x, from.y, 0f), new Vector3(rayHit.point.x, rayHit.point.y, 0f));

				_hitX = rayHit.collider.gameObject;
			} else {
				Debug.DrawLine(new Vector3(from.x, from.y, 0f), new Vector3(from.x+(absLen*dir), from.y, 0f));
			}
		}
		return (absLen-_skinWidth) * dir;
	}


	public Vector2 GetVelocity()
	{
		return _velocity;
	}

	public void SetVelocity(Vector2 velocity) {
		_velocity = velocity;
	}

	public void AddVelocity(Vector2 velocity) {
		_velocity += velocity;
	}

	public void ClearVelocity(string key) {
		_velocity = new Vector2();
	}

	public void SetDisableCollision(bool disabled) {
		_disableCollision = disabled;
	}

	public void SetDisableCloudCollision(bool disabled) {
		_disableCloudCollision = disabled;
	}

	public void MoveBy(float x, float y) {
		this.transform.Translate (x, y, 0f, Space.World);
	}

	public void MoveTo(float x, float y) {
		this.transform.position = new Vector3(x,y,0f);
	}






	void PrepareHitInfo ()
	{
		_lastHitX = _hitX;
		_lastHitY = _hitY;
		_hitX = null;
		_hitY = null;
	}
	
	void HandleHitInfo ()
	{
		// --- X axis ---

		/*if (_hitX != _lastHitX && _hitX != null && _lastHitX != null) {
			SendHitMessage("Exit", "X", _lastHitX);
			SendHitMessage("Enter", "X", _hitX);
			SendHitMessage("Update", "X", _hitX);
			return;
		}*/

		if (_hitX != null || _lastHitX != null) {
			if (_hitX != null) {
				if (_lastHitX == null) {
					SendHitMessage("Enter", "X", _hitX);
					SendHitMessage("Update", "X", _hitX);
				} else {
					SendHitMessage("Update", "X", _hitX);
				}
				
			} else if (_lastHitX != null) {
				SendHitMessage("Exit", "X", _lastHitX);
			}
		}


		// --- Y axis ---

		if (_hitY != null || _lastHitY != null) {

			// Went from one hitare directly to another
			if (_hitY != null && _lastHitY != null && _hitY != _lastHitY) {
				SendHitMessage("Exit", "Y", _lastHitY);
				SendHitMessage("Enter", "Y", _hitY);
				SendHitMessage("Update", "Y", _hitY);
				return;
			}
			
			if (_hitY != null) {
				if (_lastHitY == null) {
					SendHitMessage("Enter", "Y", _hitY);
					SendHitMessage("Update", "Y", _hitY);
				} else {
					SendHitMessage("Update", "Y", _hitY);
				}
				
			} else if (_lastHitY != null) {
				SendHitMessage("Exit", "Y", _lastHitY);
			}
		}
	}

	private string debugGO(GameObject obj) {
		if (obj == null) { return "null"; }
		return (obj.name + ", uid:" + obj.GetInstanceID());
	}

	void SendHitMessage(string enterUpdateExit, string axis, GameObject collideWith) {
		string methodName = "CharacterController2d" + enterUpdateExit + axis.ToUpper();
		collideWith.SendMessage(methodName, this.gameObject, SendMessageOptions.DontRequireReceiver);

	}

}
