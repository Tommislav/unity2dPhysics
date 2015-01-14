using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.CharacterController2D {
	public class PhysicsEvent {

		public const string EXTERNAL_FORCE = "externalForce";
		public const string MOVE_BY = "moveBy";

		private string _type;
		private int _uid;
		private Vector2 _vector;

		public PhysicsEvent(string type, int uid, Vector2 vector) {
			_type	= type;
			_uid	= uid;
			_vector = vector;
		}

		public string type { get { return _type;  } }
		public int uid { get { return _uid; } }
		public Vector2 vector { get { return _vector; } }

	}
}
