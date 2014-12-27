using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.CharacterController2D.Platform.Process {
	public class ProcessGravity : Processable {

		private World _world;
		private float _veclocityY;

		protected override void Setup() {
			_world = getWorldComponent();
			_veclocityY = 0;
		}

		public override bool IsRunning() {
			if (data.GetFlag("onLadder")) { return false; } // no gravity while climbing ladders

			return true;
		}

		public override void Process() {
			if (data.collisionState.IsOnGround) {
				_veclocityY = -_world.gravity;
			} else {
				_veclocityY = data.velocity.y - _world.gravity;
			}

			if (_veclocityY < -0.4f) { _veclocityY = -0.4f;	} // clamp
			data.velocity.y = _veclocityY;
		}





		private World getWorldComponent() {
			var worldGO = GameObject.FindWithTag("world");
			if (worldGO == null) {
				throw new UnityException("Could not find tagged object 'world'");
			}
			World world = worldGO.GetComponent<World>();
			if (world == null) {
				throw new UnityException("could not find component World in game object world");
			}
			return world;
		}

	}
}
