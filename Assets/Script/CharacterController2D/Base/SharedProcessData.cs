using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Script.CharacterController2D {
	public class SharedProcessData {

		public GameObject gameObject;
		public CollisionInfo collisionState;
		public InputMap inputMap;
		public Dictionary<string, int> flags = new Dictionary<string,int>();
		public CharacterDebug debug;

		public Vector2 velocity = new Vector2();
		public Dictionary<int, Vector2> externalVelocity = new Dictionary<int, Vector2>();

		public int dirX = 1;
		public int dirY = 1;
		public bool disableCollision;
		public bool disableCloudCollision;

		public bool GetFlag(string key) {
			if (!flags.ContainsKey(key)) {
				Debug.LogWarning("No flag with key [" + key + "] found!");
				flags.Add(key, 0);
			}
			return flags[key] == 1;
		}
	}
}
