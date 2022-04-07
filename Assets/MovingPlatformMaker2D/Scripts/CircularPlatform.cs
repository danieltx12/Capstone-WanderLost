using UnityEngine;
using System.Collections;

namespace MovingPlatformMaker2D {

	#if UNITY_5_1
	[HelpURL("http://atgamestudio.github.io/mpm2d.html#circularplatformcontroller")]
	#endif
	[AddComponentMenu("Moving Platform Maker 2D/Circular Platform")]
	public class CircularPlatform : MonoBehaviour {
		
		public float degreesPerSecond;

		void Update () {
			transform.localPosition = Quaternion.AngleAxis (degreesPerSecond * Time.deltaTime, Vector3.forward) * transform.localPosition;
		}

		public float DegreesPerSecond {
			set {
				degreesPerSecond = value;
			}
		}
	}

}