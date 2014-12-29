using System;
using UnityEngine;

namespace Assets.Script.CharacterController2D {

	public abstract class Processable {
		protected SharedProcessData data { get; private set; }

		public void Init(SharedProcessData data) {
			this.data = data;
			Setup();
		}

		protected virtual void Setup() { }

		public abstract bool IsRunning();

		public abstract void Process();

		public virtual bool AbortRestOfSequence() { return false; }



		public virtual void OnTriggerEnter2D(string layerName, Collider2D other) {}
		public virtual void OnTriggerExit2D(string layerName, Collider2D other) {}
	}
}
