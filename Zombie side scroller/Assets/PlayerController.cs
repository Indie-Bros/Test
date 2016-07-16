using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	//movement variable
	public float runSpeed;
	public float walkSpeed;

	Rigidbody myRB;
	Animator myAnim;

	bool facingRight;

	//For Jumping
	bool Grounded = false;
	Collider[] groundCollisions;
	float groundCheckRadius = 0.2f;
	public LayerMask groundLayer;
	public Transform groundCheck;
	public float jumpHeight;

	// Use this for initialization
	void Start () {
		myRB = GetComponent<Rigidbody>();
		myAnim = GetComponent<Animator>();
		facingRight = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate () {

		if(Grounded && Input.GetAxis("Jump")>0){
			Grounded = false;
			myAnim.SetBool ("Grounded", Grounded);
			myRB.AddForce(new Vector3(0,jumpHeight,0));
				}

		groundCollisions = Physics.OverlapSphere (groundCheck.position, groundCheckRadius, groundLayer);
		if (groundCollisions.Length>0) Grounded = true;
		else Grounded = false;

		myAnim.SetBool ("Grounded",Grounded);

		float move = Input.GetAxis ("Horizontal");
		myAnim.SetFloat ("Speed", Mathf.Abs (move));

		float sneaking = Input.GetAxisRaw ("Fire3");
		myAnim.SetFloat ("Sneaking", sneaking);

		float firing = Input.GetAxis ("Fire1");
		myAnim.SetFloat ("shooting", firing);

		if ((sneaking>0 ||  firing>0) && Grounded) {
			myRB.velocity = new Vector3 (move * walkSpeed, myRB.velocity.y, 0);
		}
		else{
			myRB.velocity = new Vector3 (move * runSpeed, myRB.velocity.y, 0);
		}

		if (move>0 && !facingRight) Flip ();
		else if(move<0 && facingRight) Flip();
	}

	void Flip (){
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.z *= -1;
		transform.localScale = theScale;
	}

	public float GetFacing () { 
	if(facingRight){ 
		return 1;
	}
	else {
		return -1;
		}
	}
}