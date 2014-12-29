using UnityEngine;
using System.Collections;

public class CollisionInfoComponent : MonoBehaviour {

	private CollisionInfo _collisionState = new CollisionInfo();
	public CollisionInfo CollisionState { get { return _collisionState; } }
}
