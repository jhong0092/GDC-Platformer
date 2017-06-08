using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	//create rigidbody to point to players rigidBody,allows for player to move
	 
	private Animator myAnimator;

	private bool facingRight;
	private bool running;
	private bool jump;
	private bool isGrounded;

	float horizontal;

	//serialize field allows for access to the variable from unity ui
	[SerializeField]
	private float movementSpeed;

	[SerializeField]
	private float groundRadius;

	[SerializeField]
	private float jumpForce;
	//Layer Mask is like tag but groups layers
	[SerializeField]
	private LayerMask whatIsGround;
	//checks if player is grounded used for jumping
	[SerializeField]
	private Transform[] groundPoints;
	// Use this for initialization

	//property

	public Rigidbody2D MyRigidbody{get;set;}
	public BoxCollider2D Collider{ get; set;}

	//singleton expression allows universal access
	private static Player instance;
	public static Player Instance{
		get
		{
			if (instance == null) {
				instance = GameObject.FindObjectOfType<Player> ();
			}
			return instance;
		}
	}





	void Start () 
	{
		facingRight = true;
		//assign rigidbody to players rigidbody using getcomponent(gets attached component) creates pointer
		MyRigidbody = GetComponent<Rigidbody2D>();
		//pointer to animator
		myAnimator = GetComponent<Animator> ();
		Collider = GetComponent<BoxCollider2D> ();
	}
	

	//FixedUpdate that runs a fixed amount of times use when moving something with physics
	void FixedUpdate () 
	{
		//checks to see if grounded perhaps move to handleMovement
		isGrounded = IsGrounded ();
		//Debug.Log(horizontal); shows values int console
		HandleMovement (horizontal);
		HandleLayers ();
		ResetValues();
	}

	// Update is called once per frame runs once a frame
	void Update(){
		HandleInput ();
	}

	private void HandleInput (){

		//gets horizontal for run
		horizontal = Input.GetAxis("Horizontal");//edit > project settings> axes

		//run change so speed can be changed dynamicly
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			running = true;
			movementSpeed = 2;
		} else if (Input.GetKeyUp(KeyCode.LeftShift)) {
			running = false;
			movementSpeed = 1;
		}
		if (Input.GetKeyDown (KeyCode.Space)) {
			jump = true;
		}

	}

	//handles movement
	private void HandleMovement(float horizontal)
	{
		//if allows for diagonal spring to work need to tweak how movement works if vertical springs are to work
		//if(airControl)
		MyRigidbody.velocity = new Vector2 (horizontal*movementSpeed, MyRigidbody.velocity.y);
		myAnimator.SetFloat ("speed", Mathf.Abs(horizontal));

		//handles Running 
		if (running) {
			myAnimator.SetBool ("run", true);
		} else if (!running) {
			myAnimator.SetBool ("run", false);
		}

		//jumping
		if (jump && isGrounded) {
			MyRigidbody.AddForce (new Vector2(0, jumpForce));
		}
		myAnimator.SetFloat ("ySpeed",MyRigidbody.velocity.y);

		//calls flip after movement is decided
		Flip(horizontal);
	}


	private void Flip(float horizontal)
	{
		if (horizontal > 0 && !facingRight || horizontal <0 && facingRight) 
		{
			facingRight = !facingRight;
			//this copies object rather thatn getComponent which creates a pointer
			Vector3 theScale = transform.localScale;
			theScale.x *= -1;
			transform.localScale = theScale;

		} 
	}

	private bool IsGrounded(){
		if (MyRigidbody.velocity.y <= 0)
		{
			foreach (Transform point in groundPoints) 
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position,groundRadius,whatIsGround);
				foreach (Collider2D col in colliders) 
				{
					if (col.gameObject != gameObject) 
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	private void ResetValues(){
		jump = false;
		//sets parent back to null from moving platform
		if (!isGrounded) {
			transform.parent = null;
		}
	}

	private void HandleLayers(){
		if (!isGrounded) {
			myAnimator.SetLayerWeight (1, 1);
			myAnimator.SetBool ("grounded", false);
		} else {
			myAnimator.SetLayerWeight (1, 0);
			myAnimator.SetBool ("grounded",true);
		}
	}

}

//myAnimator.GetCurrentStateInfo(Layer).IsTag("Attack")