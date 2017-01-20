using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {
	public float time;
	private float startTime;

	private void Start () {
		startTime = Time.time;
	}

	private void Update () {
		if ((startTime + time) < Time.time) {
			Destroy(gameObject);
		}
	}
}
