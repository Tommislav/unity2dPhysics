using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.CharacterController2D.Platform.Process {
	public class ProcessMovement : Processable {

		public override bool IsRunning() {
			return true;
		}

		public override void Process() {

			if (data.inputMap.GetIsDown(KeyCode.LeftArrow)) {
				data.velocity.x = -0.1f;
			}
			if (data.inputMap.GetIsDown(KeyCode.RightArrow)) {
				data.velocity.x = 0.1f;
			}
		}

	}
}
