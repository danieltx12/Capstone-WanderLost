using System;
using UnityEditor;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace MovingPlatformMaker2D {

	[CustomEditor(typeof(Path))]
	public class PathEditor : Editor {

		private const float handleSize = 0.05f;
		private const float pickSize = 0.05f;

		private Path path;
		private EdgeCollider2D col;
		private Vector2[] points;

		private bool edit = false;
		private Tool tool;

		private static GUIStyle toggleButtonStyleNormal = null;
		private static GUIStyle toggleButtonStyleToggled = null;

		void OnEnable() { 
			path = target as Path;
			if (path != null) {
				col = path.GetComponent<EdgeCollider2D> ();
				points = col.points;
			}
			tool = Tools.current;
		}

		void OnDisable() {
			Tools.current = tool;
			Tools.hidden = false;
		}

		public override void OnInspectorGUI() {

			AddEditButton ();

			EditorGUI.BeginChangeCheck ();
			Undo.RecordObject (path, "Changed properties");

			path.easingCurve = EditorGUILayout.CurveField ("Easing curve", path.easingCurve);
			path.Type = (PathType) EditorGUILayout.EnumPopup ("Type", path.Type);
			if (PathType.PingPong.Equals (path.Type)) {
				path.openStart = EditorGUILayout.ToggleLeft ("Open start", path.openStart);
				path.openEnd = EditorGUILayout.ToggleLeft ("Open end", path.openEnd);
			} else if (PathType.Connected.Equals (path.Type)) {
				path.connected = (Path) EditorGUILayout.ObjectField (path.connected, typeof(Path), true);
			}

			if (EditorGUI.EndChangeCheck () && !Application.isPlaying) {
				EditorUtility.SetDirty (path);
			}

		}

		void AddEditButton() {
			if ( toggleButtonStyleNormal == null ){
				toggleButtonStyleNormal = "Button";
				toggleButtonStyleToggled = new GUIStyle(toggleButtonStyleNormal);
				toggleButtonStyleToggled.normal.background = toggleButtonStyleToggled.active.background;
			}

			GUIStyle style = !edit
				? toggleButtonStyleNormal
				: toggleButtonStyleToggled;

			bool state = GUILayout.Toggle (edit, "Edit", style);
			if (state != edit) {
				Tools.hidden = state;
				SceneView.RepaintAll ();
				if (state) {
					tool = Tools.current;
					Tools.current = Tool.None;
				} else
					Tools.current = tool;
			}

			edit = state;
		}

		void OnSceneGUI() {
			DrawLines ();
			if(edit)
				DrawButtons ();

			Selection.activeGameObject = path.transform.gameObject;
		}

		void DrawLines() {
			Handles.color = Color.green;

			for (int i = 0; i < col.pointCount-1; i++) {
				Vector2 current = path.transform.TransformPoint (col.points [i]);
				Vector2 next = path.transform.TransformPoint (col.points [i+1]);
				Handles.DrawLine (current, next);
			}
		}

		void DrawButtons() {
			Handles.color = Color.green;
			points = col.points; 

			int deleteIndex = -1;
			int addIndex = -1;
			Vector2 newPoint = Vector2.zero;

			for (int i = 0; i < points.Length; i++) {
				Vector3 localPoint = path.transform.TransformPoint (points [i]);
				float size = HandleUtility.GetHandleSize (localPoint);

				if(Event.current.control && points.Length > 2) {
					Handles.color = Color.red;
					bool delete = Handles.Button(localPoint, Quaternion.identity, handleSize * size, handleSize * size, Handles.DotHandleCap);
					if (delete)
						deleteIndex = i;

				} else {
					EditorGUI.BeginChangeCheck ();

					Handles.color = Color.green;
					localPoint = Handles.FreeMoveHandle(localPoint, Quaternion.identity, handleSize * size, Vector3.zero, Handles.DotHandleCap);

					if(i+1 < points.Length) {
						Vector3 next = path.transform.TransformPoint (points [i+1]);
						Vector2 position = Vector2.Lerp (localPoint, next, 0.5f);

						if (Event.current.shift) {
							bool add = Handles.Button (position, Quaternion.identity, handleSize * size * 2, handleSize * size * 2, Handles.SphereHandleCap);
							if (add) {
								addIndex = i + 1;
								newPoint = path.transform.InverseTransformPoint (position);
							}
						}
					}

					if (EditorGUI.EndChangeCheck ()) {
						points [i] = path.transform.InverseTransformPoint (localPoint);

						//If cyclic, move start and end points together
						if (path.IsCyclic ()) {
							int last = points.Length - 1;
							if (i == 0) {
								points [last] = points [0];
							}
							if (i == last) {
								points [0] = points [last];
							}
						}

						UpdatePoints ("Move a point");
					}
				}

			}

			if (deleteIndex >= 0 && ((!path.IsCyclic() && points.Length > 2) || (path.IsCyclic() && points.Length > 3))) {
				List<Vector2> newPoints = new List<Vector2> ();

				if (path.IsCyclic() && (deleteIndex == 0 || deleteIndex == points.Length - 1)) {
					for (int i = 1; i < points.Length-1; i++)
						newPoints.Add (points [i]);
				} else {
					for (int i = 0; i < points.Length; i++)
						if (i != deleteIndex)
							newPoints.Add (points [i]);
				}
				points = newPoints.ToArray();

				UpdatePoints ("Delete a point");
			}

			if (addIndex >= 0) {
				Vector2[] newPoints = new Vector2[points.Length+1];
				int j = 0;
				for (int i = 0; i < points.Length; i++) {
					if (i == addIndex)
						newPoints [j++] = newPoint;
					newPoints [j++] = points [i];
				}
				points = newPoints;

				UpdatePoints ("Add a point");
			}

			if (Event.current.shift && 
				Event.current.type == EventType.MouseDown && 
				deleteIndex == -1 && 
				!path.IsCyclic()) {

				Vector2[] newPoints = new Vector2[points.Length+1];
				for (int i = 0; i < points.Length; i++) {
					newPoints [i] = points [i];
				}
				newPoints [newPoints.Length - 1] = path.transform.InverseTransformPoint (GetMouseClickPosition ());

				points = newPoints;

				UpdatePoints ("Add a point at the end");
			}
		}

		void UpdatePoints(String cause) {
			Undo.RecordObject (col, cause);
			EditorUtility.SetDirty (col);
			path.SetPoints (points);
		}

		Vector2 GetMouseClickPosition() {
			Event e = Event.current;
			Ray r = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, -e.mousePosition.y + Camera.current.pixelHeight));
			return r.origin;
		}

	}

}