using System;
using System.Collections.Generic;

namespace Assets.Script.CharacterController2D.Platform.Process {
	public class ProcessGravity : Processable {

		public override bool IsRunning() {
			return true;
		}

		public override void Process() {
			if (data.collisionState.IsOnGround) {
				data.velocity.y = 0;
			} else {
				data.velocity.y -= 0.1f;
			}
		}

	}
}
