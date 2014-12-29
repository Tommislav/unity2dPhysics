using UnityEngine;
using System.Collections.Generic;

namespace Assets.Script.CharacterController2D {
	public class InputMap {

		private Dictionary<int, InputStatus>_map;

		public InputMap() {
			_map = new Dictionary<int, InputStatus>();
		}


		public void ResetStatus() {
			foreach (KeyValuePair<int, InputStatus> entry in _map) {
				if (entry.Value.isReset) {
					entry.Value.isReset = false;
				} else if (entry.Value.isDown) {
					entry.Value.isDown = false;
					entry.Value.isReset = true;
				}
			}
		}


		public void SetKeyIsDown(int joypadCode) {
			if (!_map.ContainsKey(joypadCode)) {
				_map[joypadCode] = new InputStatus(joypadCode);
			}
			InputStatus status = _map[joypadCode];
			if (!status.isDown && !status.isReset) {
				status.downFrame = Time.frameCount;
			}
			status.isDown = true;
		}

		public bool GetIsDown(int joypadCode) {
			return GetInputStatus(joypadCode).isDown;
		}

		public bool GetDownThisFrame(int joypadCode) {
			InputStatus status = GetInputStatus(joypadCode);
			return (status.isDown && status.downFrame == Time.frameCount);
		}

		public int GetFrameKeyPressed(int joypadCode) {
			return GetInputStatus(joypadCode).downFrame;
		}



		private InputStatus GetInputStatus(int joypadCode) {
			if (!_map.ContainsKey(joypadCode)) {
				_map[joypadCode] = new InputStatus(joypadCode);
			}
			return _map[joypadCode];
		}



		class InputStatus {
			public InputStatus(int joypadCode) { this.joypadCode = joypadCode; }

			public int joypadCode;
			public bool isDown;
			public int downFrame;
			public bool isReset;
		}
	}
}
