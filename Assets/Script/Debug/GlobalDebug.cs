using System.Collections.Generic;
using UnityEngine;
class GlobalDebug:MonoBehaviour {


	public static string debug = "";
	public static Dictionary<int, string> dict = new Dictionary<int,string>();

	public static void AddDebugLine(int uid, string str) {
		str = "<" + uid + "> " + Time.frameCount + "   " + str;
		if (!dict.ContainsKey(uid)) {
			dict.Add(uid, str);
		} else {
			dict[uid] = str;
		}
	}


	public void Start() {
		debug = "";
	}


	public void Update() {

		string s = "";
		foreach (string str in dict.Values) {
			s += str + "\n";
		}
		s += debug;

		guiText.text = s;
		debug = "";
	}

}

