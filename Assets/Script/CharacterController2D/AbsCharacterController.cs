using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.CharacterController2D {
	public class AbsCharacterController : MonoBehaviour {

		public PhysicsController2D physicsController;

		private List<Processable> _processables;
		private SharedProcessData _sharedData;


		protected virtual void SetupProcesses() {}


		public void Start() {
			_processables = new List<Processable>();
			this.SetupProcesses();
		}



		public AbsCharacterController AddProcessable(Processable processable) {
			_processables.Add(processable);
			if (_sharedData != null) {
				processable.Init(_sharedData);
			}
			return this;
		}

		public AbsCharacterController AddSharedData(SharedProcessData data) {
			_sharedData = data;
			_sharedData.gameObject = this.gameObject;
			_sharedData.inputMap = new InputMap();
			_sharedData.collisionState = new CollisionState2D();
			for (int i = 0; i < _processables.Count; i++) {
				_processables[i].Init(_sharedData);
			}
			return this;
		}



		public void OnKeyDown(KeyCode keyCode) {
			_sharedData.inputMap.SetKeyIsDown(keyCode);
		}



		public void Update() {

			for (int i = 0; i < _processables.Count; i++) {

				if (_processables[i].IsRunning()) {
					_processables[i].Process();
				}
			}

			_sharedData.inputMap.ResetStatus();

			physicsController.SetVelocity(_sharedData.velocity);
		}
	}
}
