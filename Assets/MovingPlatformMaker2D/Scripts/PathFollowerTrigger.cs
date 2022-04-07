using UnityEngine;
using System.Collections;

namespace MovingPlatformMaker2D {

	#if UNITY_5_1
	[HelpURL("http://atgamestudio.github.io/mpm2d.html#pathfollowertrigger")]
	#endif
	[AddComponentMenu("Moving Platform Maker 2D/Path Follower Trigger")]
	[RequireComponent(typeof(BoxCollider2D))]
	public class PathFollowerTrigger : MonoBehaviour {

		public LayerMask layerMask;
		public PathFollower[] followers = new PathFollower[0];
		public bool activeOnlyWhenInside = false;
		public bool changeDirectionOnActivate = false;

		void Reset() {
			Collider2D col = GetComponent<Collider2D>();
			if (col == null)
				col = gameObject.AddComponent<BoxCollider2D> ();
			col.isTrigger = true;

			layerMask = LayerMask.GetMask(new string[]{"Player"});
		}

		void Start () {
			foreach (PathFollower follower in followers)
				follower.active = false;
		}

		void OnTriggerEnter2D (Collider2D other) {
			if(!IsInLayerMask(other.gameObject.layer))
				return;
			
			foreach (PathFollower follower in followers) {
				follower.active = true;
			}
		}

		void OnTriggerExit2D (Collider2D other) {
			if (!IsInLayerMask(other.gameObject.layer) || !activeOnlyWhenInside)
				return;

			foreach (PathFollower follower in followers) {
				follower.active = false;
				if (changeDirectionOnActivate)
					follower.Reverse ();
			}
		}

		bool IsInLayerMask(int layer) {
			return ((1 << layer) & layerMask) != 0;
		}
	}

}