using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.CharacterController2D.Platform.Process {
	public class ProcessFriction : Processable {

		public override bool IsRunning() {
			return (!data.inputMap.GetIsDown(JoypadCode.LEFT) && !data.inputMap.GetIsDown(JoypadCode.RIGHT));
		}

		public override void Process() {
			data.velocity.x *= 0.8f;
		}

	}
}
