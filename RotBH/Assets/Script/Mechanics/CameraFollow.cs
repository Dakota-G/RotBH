using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mechanics
{
	public class CameraFollow : MonoBehaviour {

		public GameObject FollowTarget;
		private float _elasticity = 0.2f;
		private Vector3 _velocity = Vector3.zero;
		private float _cameraZ;
		private Camera _camera;

		void Start () {
			_cameraZ = transform.position.z;
			_camera = GetComponent<Camera>();
		}
		
		void FixedUpdate () {
			if (FollowTarget) {
				Vector3 delta = FollowTarget.transform.position - _camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, _cameraZ));
				Vector3 destination = transform.position + delta;
				destination.z = _cameraZ;
				transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, _elasticity);
			}
		}
	}
}