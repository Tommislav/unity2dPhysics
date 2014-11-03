using UnityEngine;
using System.Collections;

public class PlayerPunchBehaviour : MonoBehaviour {

	public GameObject Fist;

	private int _punchEndFrame;
	private float _fistPosX;


	void Start () {
		Fist.renderer.enabled = false;
		_fistPosX = Fist.transform.localPosition.x;
	}
	

	void Update () {

		if (Input.GetKey("left ctrl")) {
			Fist.renderer.enabled = true;
			_punchEndFrame = 7;
		}

		if (--_punchEndFrame <= 0) {
			Fist.renderer.enabled = false;
		}

		if (Fist.renderer.enabled) {
			// turn left or right
		}
	}

	void MoveHor(int dir) {
		float rotation = (dir > 0) ? 0 : 180;
		Fist.transform.localRotation = Quaternion.AngleAxis(rotation, new Vector3(0,1,0));


		Vector3 pos = Fist.transform.localPosition;
		pos.x = (dir > 0) ? _fistPosX : -_fistPosX;
		Fist.transform.localPosition = pos;

		//Fist.transform.localPosition = new Vector3(0,0,1);
	}
}
