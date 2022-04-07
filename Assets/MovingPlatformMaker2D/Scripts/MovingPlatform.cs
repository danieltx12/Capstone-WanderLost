using UnityEngine;
using System.Collections;

namespace MovingPlatformMaker2D {

	#if UNITY_5_1
	[HelpURL("http://atgamestudio.github.io/mpm2d.html#movingplatform")]
	#endif
	[AddComponentMenu("Moving Platform Maker 2D/Moving Platform")]
	public class MovingPlatform : MonoBehaviour {

		void OnCollisionEnter2D(Collision2D other) {
			checkCollision (other);
		}

		void OnCollisionStay2D(Collision2D other) {
			checkCollision (other);
		}

		void OnCollisionExit2D(Collision2D other) {
			
			if (other.transform.parent != null && transform == other.transform.parent.transform) {
				other.transform.parent = null;
			}
		}

		private void checkCollision(Collision2D other) {
			foreach (ContactPoint2D contact in other.contacts) {
				Debug.DrawRay (contact.point, contact.normal, Color.red);
				if (contact.normal.y < 0) {
					if(other.rigidbody.velocity.y <= 0) {			
						other.transform.parent = transform;
					}
					return;
				}
			}
		}


	}

}