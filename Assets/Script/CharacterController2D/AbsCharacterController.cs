using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Script.CharacterController2D {
	public class AbsCharacterController : MonoBehaviour {

		public PhysicsController2D physicsController;

		private List<Processable> _processables;
		private SharedProcessData _sharedData;
		private CharacterDebug _debug;


		protected virtual void SetupProcesses() {}


		public void Start() {
			_debug = gameObject.GetComponent<CharacterDebug>();
			if (_debug == null) { throw new Exception("AbsCharacterController cannot find debug component");  }

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
			_sharedData.collisionState = getCollisionStateComponent();
			_sharedData.debug = _debug;
			for (int i = 0; i < _processables.Count; i++) {
				_processables[i].Init(_sharedData);
			}
			return this;
		}

		private CollisionState2D getCollisionStateComponent() {
			if (gameObject.GetComponent<CollisionStateComponent>() == null) {
				gameObject.AddComponent<CollisionStateComponent>();
			}
			return gameObject.GetComponent<CollisionStateComponent>().CollisionState;
		}



		public void OnKeyDown(int joypadCode) {
			_sharedData.inputMap.SetKeyIsDown(joypadCode);
		}



		public void Update() {
			_debug.Clear();

			for (int i = 0; i < _processables.Count; i++) {

				if (_processables[i].IsRunning()) {
					_processables[i].Process();
					if (_processables[i].AbortRestOfSequence()) { break; }
				}
			}

			_sharedData.inputMap.ResetStatus();
			physicsController.SetDisableCollision(_sharedData.disableCollision);
			physicsController.SetDisableCloudCollision(_sharedData.disableCloudCollision);
			physicsController.SetVelocity(GetVelocity());

			_debug.AddLine(_sharedData.collisionState.Debug());
		}

		private Vector2 GetVelocity() {
			Vector2 v = new Vector2();
			v += _sharedData.velocity;

			foreach (Vector2 extForce in _sharedData.externalVelocity.Values) {
				v += extForce;
			}
			return v;
		}





		public void OnTriggerEnter2D(Collider2D other) {
			string layerName = LayerMask.LayerToName(other.gameObject.layer);
			for (int i = 0; i < _processables.Count; i++) {
				_processables[i].OnTriggerEnter2D(layerName, other);
			}
		}

		public void OnTriggerExit2D(Collider2D other) {
			string layerName = LayerMask.LayerToName(other.gameObject.layer);
			for (int i = 0; i < _processables.Count; i++) {
				_processables[i].OnTriggerExit2D(layerName, other);
			}
		}

		public void OnPhysicsEvent(PhysicsEvent e) {
			if (e.type == PhysicsEvent.EXTERNAL_FORCE) {
				int id = e.uid;
				Vector2 force = e.vector;
				bool hasForce = !(force.x == 0 && force.y == 0);

				if (!_sharedData.externalVelocity.ContainsKey(id)) {
					if (hasForce) {
						_sharedData.externalVelocity[id] = force;
					}
				} else {

					if (hasForce) {
						_sharedData.externalVelocity[id] = force;
					} else {
						_sharedData.externalVelocity.Remove(id);
					}
				}
			}
		}
	}
}
