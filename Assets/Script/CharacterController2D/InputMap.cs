using UnityEngine;
using System.Collections.Generic;

namespace Assets.Script.CharacterController2D {
	public class InputMap {

		private Dictionary<KeyCode, InputStatus>_map;

		public InputMap() {
			_map = new Dictionary<KeyCode, InputStatus>();
		}


		public void ResetStatus() {
			foreach (KeyValuePair<KeyCode, InputStatus> entry in _map) {
				if (entry.Value.isReset) {
					entry.Value.isReset = false;
				} else if (entry.Value.isDown) {
					entry.Value.isDown = false;
					entry.Value.isReset = true;
				}
			}
		}


		public void SetKeyIsDown(KeyCode keyCode) {
			if (!_map.ContainsKey(keyCode)) {
				_map[keyCode] = new InputStatus(keyCode);
			}
			InputStatus status = _map[keyCode];
			if (!status.isDown && !status.isReset) {
				status.downFrame = Time.frameCount;
			}
			status.isDown = true;
		}

		public bool GetIsDown(KeyCode keyCode) {
			return GetInputStatus(keyCode).isDown;
		}

		public bool GetDownThisFrame(KeyCode keyCode) {
			InputStatus status = GetInputStatus(keyCode);
			return (status.isDown && status.downFrame == Time.frameCount);
		}



		private InputStatus GetInputStatus(KeyCode keyCode) {
			if (!_map.ContainsKey(keyCode)) {
				_map[keyCode] = new InputStatus(keyCode);
			}
			return _map[keyCode];
		}



		class InputStatus {
			public InputStatus(KeyCode keyCode) { this.keyCode = keyCode; }

			public KeyCode keyCode;
			public bool isDown;
			public int downFrame;
			public bool isReset;
		}
	}
}
