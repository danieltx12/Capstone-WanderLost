using UnityEngine;

public class CircularSawBlade : MonoBehaviour {

	public float speed = 1f;

	void Update () {
		transform.Rotate (new Vector3 (0, 0, 1) * speed * Time.deltaTime);
	}
}
