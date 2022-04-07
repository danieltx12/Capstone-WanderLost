using UnityEngine;
using UnityEditor;
using System.Collections;

namespace MovingPlatformMaker2D {

	[CustomEditor (typeof(CircularPlatformController))]
	public class CircularPlatformControllerEditor : Editor {

		private const int circle=360;

		private CircularPlatformController controller;

		void Reset() {
			controller = (CircularPlatformController)target;
			UpdatePlatforms ();
		}

		void OnEnable() {
			controller = (CircularPlatformController)target;
		}

		public override void OnInspectorGUI(){
			EditorGUI.BeginChangeCheck ();

			DrawDefaultInspector ();

			if (EditorGUI.EndChangeCheck()) {
				Undo.RecordObject(controller, "Edit Circular Platform");
				UpdatePlatforms ();		
				EditorUtility.SetDirty (controller);
			}
		}

		public void UpdatePlatforms(){
			DestroyExistingPlatforms ();
			CreatePlatforms ();
		}

		private void DestroyExistingPlatforms(){
			CircularPlatform[] platforms = controller.GetComponentsInChildren<CircularPlatform> ();
			foreach (CircularPlatform platform in platforms) {
				Undo.DestroyObjectImmediate(platform.gameObject);
			}
		}

		private void CreatePlatforms(){
			if (controller.numberOfPlatforms <= 0)
				return;

			if (controller.platformPrefab == null)
				return;

			float angle = circle / controller.numberOfPlatforms;
			for (int x = 0; x < controller.numberOfPlatforms; x++) {
				GameObject platform = GameObject.Instantiate (controller.platformPrefab, controller.transform.localPosition, Quaternion.identity) as GameObject;
				platform.transform.parent = controller.transform; 

				CircularPlatform circularPlatform = platform.AddComponent<CircularPlatform> ();
				circularPlatform.DegreesPerSecond = controller.degreesPerSecond;

				platform.transform.localPosition = new Vector3(Mathf.Cos(Mathf.Deg2Rad*angle*x), Mathf.Sin(Mathf.Deg2Rad * angle*x), 0) * controller.radius;

				Undo.RegisterCreatedObjectUndo (platform, "Created circular platform");
			}
		}

	}

}