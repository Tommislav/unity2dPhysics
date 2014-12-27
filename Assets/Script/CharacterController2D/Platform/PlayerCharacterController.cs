using System;
using System.Collections.Generic;
using Assets.Script.CharacterController2D;
using Assets.Script.CharacterController2D.Platform.Process;

namespace Assets.Script.CharacterController2D.Platform {
	public class PlayerCharacterController : AbsCharacterController {

		protected override void SetupProcesses() {

			this.AddProcessable(new ProcessLadderClimbing());
			this.AddProcessable(new ProcessGravity());
			this.AddProcessable(new ProcessMovement());
			this.AddProcessable(new ProcessFriction());
			this.AddProcessable(new ProcessJumping());
			

			this.AddSharedData(new SharedProcessData());
		}
		

	}
}
