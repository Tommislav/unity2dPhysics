using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Script.CharacterController2D.Platform.Process {
	class ProcessJumping : Processable {

		private const int JUMP_TIME = 16;
		private const float JUMP_STR = 0.08f;
		private const float MAX_JUMP_STR = 0.30f;
		private float _currJumpTimeLeft;
		private float _jumpVelocity;

		protected override void Setup() {
			SetJumpFlag(0);
			_jumpVelocity = 0;
			data.flags["jumpDisabled"] = 0;
		}

		public override bool IsRunning() {
			bool wasJumping = (GetJumpFlag() == 1);
			bool jumpKeyPressedThisFrame = JumpKeyPressedThisFrame();
			bool climbingLadder = data.GetFlag("onLadder");
			bool onGround = data.collisionState.IsOnGround;
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
				return;
			}

			if (GetJumpFlag() == 1) {
				_currJumpTimeLeft--;
				applyJumpVelocity();
			}

			if (_currJumpTimeLeft <= 0 || !JumpKeyDown()) {
				SetJumpFlag(0);
				_jumpVelocity = 0;
			}

		}

		private void applyJumpVelocity() {
			_jumpVelocity += JUMP_STR;
			if (_jumpVelocity > MAX_JUMP_STR) {
				_jumpVelocity = MAX_JUMP_STR;
			} else if (_jumpVelocity < -MAX_JUMP_STR) {
				_jumpVelocity = -MAX_JUMP_STR;
			}

			data.velocity.y = _jumpVelocity;
		}



		private int GetJumpFlag() {
			return data.flags["isJumping"];
		}
		private void SetJumpFlag(int flag) {
			data.flags["isJumping"] = flag;
		}
		private bool JumpKeyPressedThisFrame() {
			return data.inputMap.GetDownThisFrame(JoypadCode.JUMP);
		}
		private bool JumpKeyDown() {
			return data.inputMap.GetIsDown(JoypadCode.JUMP);
		}
	}
}
