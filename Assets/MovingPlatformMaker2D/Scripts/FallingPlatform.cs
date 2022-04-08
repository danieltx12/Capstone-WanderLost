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
        public float fallingDelay = 1f;

        private Collider2D coll;
		private Rigidbody2D body;
        public Transform originalPos;
        SpriteRenderer spriteRenderer;

        void Start() {
			coll = GetComponent<Collider2D>();
			body = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
		}

		void OnCollisionEnter2D (Collision2D collision) {
            if (collision.gameObject.tag == "Player")
            {
                StartCoroutine(Fall());
            }
		}

		IEnumerator Fall() {
			yield return new WaitForSeconds(fallDelay);
            //coll.isTrigger = true;
            body.bodyType = RigidbodyType2D.Dynamic;
            yield return new WaitForSeconds(fallingDelay);
            coll.enabled = !coll.enabled;
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(2f);
            body.bodyType = RigidbodyType2D.Static;
            transform.position = originalPos.position;
            coll.enabled = !coll.enabled;
            spriteRenderer.enabled = !spriteRenderer.enabled;

        }

	}

}