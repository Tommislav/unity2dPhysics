using UnityEngine;
using System.Collections;

public class CameraFollowScript : MonoBehaviour {

	public Transform follow;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
		this.transform.position = new Vector3(follow.transform.position.x, follow.transform.position.y, this.transform.position.z);
	}
}
