using UnityEngine;
using System.Collections;

public class RaycastTestScript : MonoBehaviour {

	public LayerMask collisionMask; 

	private BoxCollider2D _collider;

	public void Start() {
		//	_collider = GetComponent<BoxCollider2D> ();
	}

	public void OnDrawGizmos() {

		_collider = GetComponent<BoxCollider2D> ();



		var localScale = this.transform.localScale;
		var halfSize = new Vector3 (_collider.size.x * Mathf.Abs(localScale.x), _collider.size.y * Mathf.Abs(localScale.y), 2) / 2f;
		var center = new Vector3 (_collider.center.x * localScale.x, _collider.center.y * localScale.y);

		Vector2 from = transform.position - new Vector3(center.x, center.y + halfSize.y);

		Vector2 to = new Vector2 (from.x, from.y-4);

		//Debug.DrawRay (from, new Vector2 (0, -4), Color.red);
		var hit = Physics2D.Raycast (from, new Vector2 (0, -1), 4, collisionMask);

		if (hit) {


			Debug.DrawLine (from, hit.point, Color.blue);

		} else {
			Debug.DrawLine (from, to, Color.red);
		}

	}
}
