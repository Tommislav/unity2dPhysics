using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.CharacterController2D.Platform.Process {
	public class ProcessMovement : Processable {

		public override bool IsRunning() {
			return true;
		}

		public override void Process() {

			if (data.inputMap.GetIsDown(JoypadCode.LEFT)) {
				data.velocity.x = -0.1f;
				data.dirX = -1;
			}
			if (data.inputMap.GetIsDown(JoypadCode.RIGHT)) {
				data.velocity.x = 0.1f;
				data.dirX = 1;
			}

			//data.debug.AddLine("Horiz: " + Input.GetAxis("Horizontal"));
		}

	}
}
