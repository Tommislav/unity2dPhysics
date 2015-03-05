
namespace Assets.Script.CharacterController2D.Platform.Process {
	class ProcessJumping : Processable {

		private const int JUMP_TIME = 16;
		private const float JUMP_STR = 0.08f;
		private const float MAX_JUMP_STR = 0.30f;
		private float _currJumpTimeLeft;
		private float _jumpVelocityY;
		private float _jumpVelocityX;

		protected override void Setup() {
			SetJumpFlag(0);
			_jumpVelocityY = 0;
			_jumpVelocityX = 0;
			data.flags["jumpDisabled"] = 0;
			data.flags["doJump"] = 0;
		}

		public override bool IsRunning() {

			bool forcedJump = data.flags["doJump"] == 1;
			if (forcedJump) { return true;  }

			bool wasJumping = (GetJumpFlag() == 1);
			bool jumpKeyPressedThisFrame = JumpKeyPressedThisFrame();
			bool climbingLadder = data.GetFlag("onLadder");
			bool onGround = data.collisionInfo.IsOnGround;
			onGround |= climbingLadder;

			bool jumpDisabled = data.GetFlag("jumpDisabled");
			
			
			data.debug.AddLine("isJumping: " + wasJumping);
			data.debug.AddLine("onGround: " + onGround);

			if (jumpDisabled) { return false; }
			return wasJumping || (onGround && jumpKeyPressedThisFrame);
		}


		public override void Process() {
			if (GetJumpFlag() == 0 && JumpKeyPressedThisFrame()) { // Started the jump this frame!
				_currJumpTimeLeft = JUMP_TIME;
				SetJumpFlag(1);
				applyJumpVelocity();

				if (data.flags["jumpX"] != 0) {
					_jumpVelocityX = (float)data.flags["jumpX"] / 100f;
					data.flags["jumpX"] = 0;
				}

				return;
			}

			if (GetJumpFlag() == 1) {
				_currJumpTimeLeft--;
				applyJumpVelocity();
			}

			if (_currJumpTimeLeft <= 0 || !JumpKeyDown()) {
				SetJumpFlag(0);
				_jumpVelocityY = 0;
			}

		}

		private void applyJumpVelocity() {
			_jumpVelocityY += JUMP_STR;
			if (_jumpVelocityY > MAX_JUMP_STR) {
				_jumpVelocityY = MAX_JUMP_STR;
			} else if (_jumpVelocityY < -MAX_JUMP_STR) {
				_jumpVelocityY = -MAX_JUMP_STR;
			}

			_jumpVelocityX *= 0.5f;

			data.velocity.y = _jumpVelocityY;
			data.velocity.x += _jumpVelocityX;
			
		}



		private int GetJumpFlag() {
			return data.flags["isJumping"];
		}
		private void SetJumpFlag(int flag) {
			data.flags["isJumping"] = flag;
		}
		private bool JumpKeyPressedThisFrame() {
			if (data.flags["doJump"] == 1) {
				data.flags["doJump"] = 0;
				return true;
			}
			return data.inputMap.GetDownThisFrame(JoypadCode.JUMP);
		}
		private bool JumpKeyDown() {
			return data.inputMap.GetIsDown(JoypadCode.JUMP);
		}
	}
}
