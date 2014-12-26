using System;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Script.CharacterController2D {
	public class SharedProcessData {

		public GameObject gameObject;
		public CollisionState2D collisionState;
		public InputMap inputMap;
		public Dictionary<string, int> flags;

		public Vector2 velocity = new Vector2();
		public int dirX = 1;
		public int dirY = 1;
	
	}
}
