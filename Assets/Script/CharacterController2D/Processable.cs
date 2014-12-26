using System;
using UnityEngine;

namespace Assets.Script.CharacterController2D {

	public abstract class Processable {
		protected SharedProcessData data { get; private set; }

		public void Init(SharedProcessData data) {
			this.data = data;
		}



		public abstract bool IsRunning();

		public abstract void Process();

		
	}
}
