using UnityEngine;
using System.Collections;

namespace MovingPlatformMaker2D {

	#if UNITY_5_1
	[HelpURL("http://atgamestudio.github.io/mpm2d.html#fallingplatform")]
	#endif
	[AddComponentMenu("Moving Platform Maker 2D/Falling Platform")]
	[RequireComponent(typeof(Collider2D))]
	[RequireComponent(typeof(Rigidbody2D))]
	public class FallingPlatform : MonoBehaviour {

		public float fallDelay = 1f;

		private Collider2D coll;
		private Rigidbody2D body;

		void Awake() {
			coll = GetComponent<Collider2D>();
			body = GetComponent<Rigidbody2D>();
		}

		void OnCollisionEnter2D (Collision2D other) {
			StartCoroutine(Fall());
		}

		IEnumerator Fall() {
			yield return new WaitForSeconds(fallDelay);
			coll.isTrigger = true;
			body.isKinematic = false;
		}

	}

}