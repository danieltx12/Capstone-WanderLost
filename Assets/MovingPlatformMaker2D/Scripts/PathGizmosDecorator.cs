using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MovingPlatformMaker2D {

	#if UNITY_5_1
	[HelpURL("http://atgamestudio.github.io/mpm2d.html#decorators")]
	#endif
	[AddComponentMenu("Moving Platform Maker 2D/Path Gizmos Decorator")]
	public class PathGizmosDecorator : MonoBehaviour {

		public Color color = Color.green;

		void OnDrawGizmos() {
			Path path = GetComponent<Path> ();
			if (path == null) {
				Debug.LogError ("Path not found for this decorator");
				return;
			}

			Vector2[] points = path.GetWorldPoints (transform);

			Gizmos.color = color;

			for (int i = 0; i < points.Length-1; i++) {
				Gizmos.DrawLine (points [i], points [i+1]);
			}

			if (path.Type == PathType.Connected && path.connected != null) {
				Gizmos.DrawLine (points [points.Length - 1], path.connected.GetWorldPoints (path.connected.transform) [0]);
			}
		}

	}

}