using UnityEngine;
using System.Collections;

public class SpringUp : MonoBehaviour {

	private Animator myAnimator;

	[SerializeField]
	private int bounceForce;

	// Use this for initialization
	void Start () {
		myAnimator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// movement must be changed for horizontal springs to work could fix by using time delay rather than aircontrol
	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.name == "Player") {
				Player.Instance.MyRigidbody.velocity = (new Vector2 (Player.Instance.MyRigidbody.velocity.x, 0));
				myAnimator.SetTrigger ("spring");
				Player.Instance.MyRigidbody.AddForce (new Vector2 (0, bounceForce));
				//myAnimator.ResetTrigger ("spring");
		}
	}
}
