using UnityEngine;
using System.Collections;

public class CharacterDebug : MonoBehaviour {

	public GameObject debugOutput;
	private string _text = "";
	private float _startY;


	
	void Start () {
		TextMesh tm = debugOutput.GetComponent<TextMesh>();
		_startY = tm.transform.localPosition.y;
	}
	
	void Update () {
		TextMesh tm = debugOutput.GetComponent<TextMesh>();
		tm.text = _text;

		float height = debugOutput.GetComponent<Renderer>().bounds.size.y;
		tm.transform.localPosition = new Vector3(tm.transform.localPosition.x, _startY + height, 0);
	}


	public void Clear() {
		SetText("");
	}

	public void SetText(string text) {
		_text = text;
	}

	public void AddLine(string text) {
		if (_text == null) { _text = ""; }
		if (_text.Length > 0) {
			_text += "\n";
		}
		_text += text;
	}
}
