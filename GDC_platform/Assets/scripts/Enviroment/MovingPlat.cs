using UnityEngine;
using System.Collections;

public class MovingPlat : MonoBehaviour {

	private Transform myTransform;
	private Vector3 ogPos;
	private int count;
	private bool forward=true;


	[SerializeField]
	private float movementSpeed;

	[SerializeField]
	private int distance;

	[SerializeField]
	private int mode;

	// Use this for initialization
	void Start () {
		myTransform = gameObject.GetComponent<Transform> ();
		ogPos = myTransform.localPosition;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Move ();
	}

	private void Move(){
		
		//horizontal
		if (mode == 1) {
			//decides direction
			if (forward && myTransform.localPosition.x > ogPos.x + distance) {
				forward = !forward;
			} else if (!forward && myTransform.localPosition.x < ogPos.x) {
				forward = !forward;
			}

			//moves platform 
			if (forward) {
				myTransform.localPosition = new Vector3 (myTransform.localPosition.x + 1 * movementSpeed / 100, ogPos.y, ogPos.z);
			} else {
				myTransform.localPosition = new Vector3 (myTransform.localPosition.x - 1 * movementSpeed / 100, ogPos.y, ogPos.z);
			}
		} else if (mode == 2) {//vertical
			//decides direction
			if (forward && myTransform.localPosition.y > ogPos.y + distance) {
				forward = !forward;
			} else if (!forward && myTransform.localPosition.y < ogPos.y) {
				forward = !forward;
			}

			//moves platform 
			if (forward) {
				myTransform.localPosition = new Vector3 (ogPos.x, myTransform.localPosition.y + 1 * movementSpeed / 100, ogPos.z);
			} else {
				myTransform.localPosition = new Vector3 (ogPos.x, myTransform.localPosition.y - 1 * movementSpeed / 100, ogPos.z);
			}
		}
	}

	//sets player to child of platform makes parent null in reset values
	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.name == "Player") {
			Player.Instance.transform.parent = gameObject.transform;
		}

	}


}
