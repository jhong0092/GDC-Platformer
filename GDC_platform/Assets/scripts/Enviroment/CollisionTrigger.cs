using UnityEngine;
using System.Collections;

public class CollisionTrigger : MonoBehaviour {

	[SerializeField]
	private BoxCollider2D platformCollider;
	//height is an object in unity
	[SerializeField]
	private GameObject height;

	void Start () {

	}

	void Update(){
		PassThrough();
	}

	private void PassThrough(){
		//0.1 to account for pivot point
		if ((Player.Instance.transform.position.y-0.1) < height.transform.position.y) {
			Physics2D.IgnoreCollision (Player.Instance.Collider, platformCollider,true);
		} else {
			Physics2D.IgnoreCollision (Player.Instance.Collider, platformCollider,false);

		}
	}
}
