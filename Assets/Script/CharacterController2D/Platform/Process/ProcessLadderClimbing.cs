using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.CharacterController2D.Platform.Process {
	class ProcessLadderClimbing : Processable {

		private bool _onLadder;
		private float _ladderX;
		private int _ladderKeyPress;

		public override bool IsRunning() {
			return _onLadder;
		}

		public override bool AbortRestOfSequence() {
			return false;
		}

		protected override void Setup() {
			data.flags["onLadder"] = 0;
		}

		public override void OnTriggerEnter2D(string layerName, UnityEngine.Collider2D other) {
			if (layerName == "Ladder") {
				SetIsOnLadder(true);
				_ladderX = other.transform.position.x;
			}
		}

		public override void OnTriggerExit2D(string layerName, UnityEngine.Collider2D other) {
			if (layerName == "Ladder") {
				SetIsOnLadder(false);
			}
		}


		public override void Process() {
			if (GetIsClimbing() && data.GetFlag("isJumping")) {
				// both jumping and climbing - can't do
				SetIsClimbing(false);
			}

			if (!GetIsClimbing()) {
				if (GetShouldStartClimbLadder()) {
					SetIsClimbing(true);
				}
			}

			if (GetIsClimbing()) {

				bool keyUp = data.inputMap.GetIsDown(JoypadCode.UP);
				bool keyDown = data.inputMap.GetIsDown(JoypadCode.DOWN);

				if (keyUp) {
					data.velocity.y = 0.1f;
				} else if (keyDown) {
					data.velocity.y = -0.1f;
				} else {
					data.velocity.y = 0;
				}

				if (keyUp || keyDown) {
					Transform t = data.gameObject.transform;
					data.gameObject.transform.position = new Vector3(_ladderX, t.position.y, t.position.z);
				}
			}
		}


		private bool GetShouldStartClimbLadder() {
			int frameUp = data.inputMap.GetFrameKeyPressed(JoypadCode.UP);
			int frameDown = data.inputMap.GetFrameKeyPressed(JoypadCode.DOWN);

			int frame = (frameUp > frameDown) ? frameUp : frameDown;
			if (frame > 0 && frame != _ladderKeyPress) {
				_ladderKeyPress = frame;
				return true;
			}
			return false;
		}



		private void SetIsOnLadder(bool flag) {
			if (flag) {
				data.velocity.x = 0.0f;
				data.velocity.y = 0.0f;
			} else {
				SetIsClimbing(false);
			}
			_onLadder = flag;
		}

		private void SetIsClimbing(bool flag) {
			data.flags["onLadder"] = flag ? 1 : 0;
			data.disableCloudCollision = flag;
		}

		private bool GetIsClimbing() {
			return data.GetFlag("onLadder");
		}
	}
}
