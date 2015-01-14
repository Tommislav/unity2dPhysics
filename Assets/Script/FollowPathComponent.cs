

using Assets.Script.CharacterController2D;
using System.Collections.Generic;
using UnityEngine;

class FollowPathComponent : MonoBehaviour {

	public enum FollowType {
		MoveTowards,
		Lerp
	}

	public FollowType Type = FollowType.MoveTowards;
	public PathDefinition Path;
	public float Speed = 1.0f;
	public float MaxDistanceToGoal = 0.1f;
	public bool IsMovingPlatform = true;

	private IEnumerator<Transform> _currentPoint;
	private Vector3 _lastPos;
	private GameObject _colliding;
	private CollisionInfo _collidingColInfo;

	public void Start() {
		if (Path == null) {
			Debug.LogError("PATH IS NULL!", gameObject);
			return;
		}

		_lastPos = Vector3.zero;
		_currentPoint = Path.GetPathsEnumerator();
		_currentPoint.MoveNext();

		if (_currentPoint.Current == null) return;

		transform.position = _currentPoint.Current.position;
	}

	public void Update() {
		if (_currentPoint == null || _currentPoint.Current == null) {
			return;
		}

		if (Type == FollowType.MoveTowards) {
			transform.position = Vector3.MoveTowards(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
		} else if (Type == FollowType.Lerp) {
			transform.position = Vector3.Lerp(transform.position, _currentPoint.Current.position, Time.deltaTime * Speed);
		}

		var distanceSqr = (transform.position - _currentPoint.Current.position).sqrMagnitude;
		if (distanceSqr < MaxDistanceToGoal * MaxDistanceToGoal) {
			transform.position = _currentPoint.Current.position;
			_currentPoint.MoveNext();
		}

		if (IsMovingPlatform) {
			HandleMovingPlatforms();
			_lastPos = transform.position;
		}
	}

	private void HandleMovingPlatforms() {
		if (_colliding != null) {
			Vector2 diff = transform.position - _lastPos;
			DispatchPhysicsEvent(_colliding, diff);
		}
	}






	public void CharacterController2dEnterY(GameObject colliding) {
		if (colliding.GetComponent<CollisionInfoComponent>() != null) {
			_colliding = colliding;
			_collidingColInfo = colliding.GetComponent<CollisionInfoComponent>().CollisionState;
		}

		//DispatchPhysicsEvent(colliding, new Vector2(movement, 0.0f));
	}

	public void CharacterController2dExitY(GameObject colliding) {
		_colliding = null;
		_collidingColInfo = null;
		//DispatchPhysicsEvent(colliding, new Vector2(0.0f, 0.0f));
	
	}

	private void DispatchPhysicsEvent(GameObject toObj, Vector2 force) {
		PhysicsEvent e = new PhysicsEvent(PhysicsEvent.MOVE_BY, gameObject.GetInstanceID(), force);
		toObj.SendMessage("OnPhysicsEvent", e, SendMessageOptions.DontRequireReceiver);
	}

}

