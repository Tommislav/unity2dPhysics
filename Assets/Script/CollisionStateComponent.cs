using UnityEngine;
using System.Collections;

public class CollisionStateComponent : MonoBehaviour {

	private CollisionState2D _collisionState = new CollisionState2D();
	public CollisionState2D CollisionState { get { return _collisionState; } }
}
