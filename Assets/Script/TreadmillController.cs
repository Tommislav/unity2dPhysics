using UnityEngine;
using System.Collections;
using Assets.Script.CharacterController2D;

public class TreadmillController : MonoBehaviour {

	public float movement = 0.05f;

	//private GameObject _collidingObject;

	public void CharacterController2dEnterY(GameObject colliding) {
		//_collidingObject = colliding;
		DispatchPhysicsEvent(colliding, new Vector2(movement, 0.0f));
	}

	public void CharacterController2dExitY(GameObject colliding) {
		DispatchPhysicsEvent(colliding, new Vector2(0.0f, 0.0f));
		//_collidingObject = null;
	}

	private void DispatchPhysicsEvent(GameObject toObj, Vector2 force) {
		PhysicsEvent e = new PhysicsEvent(PhysicsEvent.EXTERNAL_FORCE, gameObject.GetInstanceID(), force);
		toObj.SendMessage("OnPhysicsEvent", e, SendMessageOptions.DontRequireReceiver);
	}
}
