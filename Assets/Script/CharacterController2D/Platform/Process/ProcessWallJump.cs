
namespace Assets.Script.CharacterController2D.Platform.Process {
	class ProcessWallJump : Processable {

		public override bool IsRunning() {
			return true;
		}

		protected override void Setup() {
			// data.flags["jumpDisabled"] = 0;
			// bool onGround = data.collisionInfo.IsOnGround;
			data.flags["wallGlide"] = 0; 
		}

		public override void Process() {

			int wallGlide = 0;

			if (!data.collisionInfo.IsOnGround) {
				if ((data.collisionInfo.IsCollidingLeft || data.collisionInfo.IsCollidingRight)) {

					if (data.inputMap.GetIsDown(JoypadCode.LEFT) || data.inputMap.GetIsDown(JoypadCode.RIGHT)) {
						wallGlide = 1;
					}

				}
			}


			data.flags["jumpX"] = 0;

			if (wallGlide == 1 && data.inputMap.GetDownThisFrame(JoypadCode.JUMP)) {
				wallGlide = 0;
				data.flags["doJump"] = 1;
				data.flags["jumpX"] = data.inputMap.GetIsDown(JoypadCode.RIGHT) ? -90 : 90;
			}

			data.flags["wallGlide"] = wallGlide;
		}

		

	}
}
