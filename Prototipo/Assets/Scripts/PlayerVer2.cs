using UnityEngine;
using System.Collections;

public class PlayerVer2 : MonoBehaviour {

	public float playerSpeed=400, jumpForce=1500;

	private bool inAir= false;
	private Animator animator;

	void Start () {
		animator = GetComponent<Animator>() as Animator;
	}
	// Update is called once per frame
	void FixedUpdate () {
		float desp= Input.GetAxisRaw("Horizontal");
		float jump= Input.GetAxis("Jump");
		
		if(jump>0 && !inAir){
			rigidbody2D.AddForce(Vector2.up * Time.deltaTime * jump * jumpForce);
		}
		
		if(desp<0) {
			//sie (false);
			transform.eulerAngles = new Vector3(0, 180, 0);
			transform.Translate(Vector2.right  * playerSpeed * Time.deltaTime* 0.02f);

			animator.SetTrigger("makewalk");
			//rigidbody2D.AddForce(-Vector2.right * speed);
		}
		
		if(desp>0) {
			//sie (true);
			transform.eulerAngles = new Vector3(0, 0, 0);
			transform.Translate(Vector2.right * playerSpeed * Time.deltaTime* 0.02f);
			animator.SetBool("makewalk", true);
			//rigidbody2D.AddForce(Vector2.right * speed);
		}

		if (desp == 0) {
			animator.SetBool("makewalk" , false);	

		}
				
		
	}
	
	void putInAir(){
		inAir= true;
	}
	
	void OnCollisionEnter2D(Collision2D coll){
		inAir= false;
	}
	
	void OnCollisionExit2D(Collision2D coll){
		Invoke("putInAir",0.2f);
	}
}
