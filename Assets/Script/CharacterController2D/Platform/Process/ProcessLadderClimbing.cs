using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.CharacterController2D.Platform.Process {
	class ProcessLadderClimbing : Processable {

		private bool _onLadder;
		private float _ladderX;

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


			bool keyUp = data.inputMap.GetIsDown(KeyCode.UpArrow);
			bool keyDown = data.inputMap.GetIsDown(KeyCode.DownArrow);

			if (!GetIsClimbing()) {
				if (keyUp || keyDown) {
					SetIsClimbing(true);
				}
			}

			if (GetIsClimbing()) {
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
