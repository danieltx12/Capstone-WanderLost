using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif 
using System.Collections;
using System.Collections.Generic;

namespace MovingPlatformMaker2D {

	#if UNITY_5_1
	[HelpURL("http://atgamestudio.github.io/mpm2d.html#circularplatformcontroller")]
	#endif
	[AddComponentMenu("Moving Platform Maker 2D/Circular Platform Controller")]
	public class CircularPlatformController : MonoBehaviour {

		public Color gizmoColor = Color.black;
		public float degreesPerSecond = 65.0f;
		public float radius = 3f;
		public int numberOfPlatforms = 1;
		public GameObject platformPrefab;

		void Awake() {
			if (platformPrefab == null) {
				Debug.LogError ("CircularPlatformController must have a platform prefab");
				enabled = false;
				return;
			}
		}

		void OnValidate() {
			radius = Mathf.Clamp (radius, 0f, float.MaxValue);
			numberOfPlatforms = Mathf.Clamp (numberOfPlatforms, 0, int.MaxValue);
		}

		void OnDrawGizmos() {
			#if UNITY_EDITOR
			Handles.color = gizmoColor;
			Handles.DrawWireDisc (transform.localPosition, Vector3.forward, transform.GetComponent<CircularPlatformController> ().radius);
			#endif 
		}

	}

}