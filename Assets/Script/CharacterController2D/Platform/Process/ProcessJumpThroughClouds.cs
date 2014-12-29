using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.CharacterController2D.Platform.Process {


	class ProcessJumpThroughClouds : Processable {

		private int _fallingThrough;

		public override bool IsRunning() {
			return data.collisionInfo.IsOnGround || (_fallingThrough > 0);
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

				if (data.inputMap.GetIsDown(JoypadCode.DOWN) && data.inputMap.GetDownThisFrame(JoypadCode.JUMP)) {
					data.disableCloudCollision = true;
					_fallingThrough = 4;
					data.flags["jumpDisabled"] = 1;
				}
			}
		}
	}
}
