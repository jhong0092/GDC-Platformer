using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	//sets boundaries for camera 
	[SerializeField]
	private float xMax;
	[SerializeField]
	private float yMax;
	[SerializeField]
	private float xMin;
	[SerializeField]
	private float yMin;

	private Transform target;

	// Use this for initialization
	void Start () {
		target = GameObject.Find ("Player").transform;
	}
	
	// LateUpdate is called after update;
	void LateUpdate () {
		//moves camera based on player player
		transform.position = new Vector3(Mathf.Clamp(target.position.x,xMin,xMax),Mathf.Clamp(target.position.y,yMin,yMax),transform.position.z);
	
	}
}
