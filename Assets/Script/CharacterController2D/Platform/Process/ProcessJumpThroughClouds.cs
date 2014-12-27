using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.CharacterController2D.Platform.Process {


	class ProcessJumpThroughClouds : Processable {

		private int _fallingThrough;

		public override bool IsRunning() {
			return data.collisionState.IsOnGround || (_fallingThrough > 0);
		}

		public override void Process() {

			if (_fallingThrough > 0) {
				_fallingThrough--;

				if (_fallingThrough <= 0) {
					data.disableCloudCollision = false;
					_fallingThrough = 0;
					data.flags["jumpDisabled"] = 0;
				}
				
			} else {

				if (data.inputMap.GetIsDown(KeyCode.DownArrow) && data.inputMap.GetDownThisFrame(KeyCode.Space)) {
					data.disableCloudCollision = true;
					_fallingThrough = 4;
					data.flags["jumpDisabled"] = 1;
				}
			}
		}
	}
}
