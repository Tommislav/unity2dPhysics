using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.CharacterController2D.Platform.Process {
	public class ProcessFriction : Processable {

		public override bool IsRunning() {
			return (!data.inputMap.GetIsDown(KeyCode.LeftArrow) && !data.inputMap.GetIsDown(KeyCode.RightArrow));
		}

		public override void Process() {
			data.velocity.x *= 0.8f;
		}

	}
}
