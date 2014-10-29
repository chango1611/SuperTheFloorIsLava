using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public float playerSpeed=400, jumpForce=1500;

	private bool inAir= false;
	private Animator animator;
	private float max_distance;

	void Start () {
		animator = GetComponent<Animator>() as Animator;
		SpriteRenderer aux= GetComponent<SpriteRenderer>() as SpriteRenderer;
		max_distance= transform.localScale.y * aux.sprite.rect.height/200 + 0.1f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float desp= Input.GetAxisRaw("Horizontal");
		float jump= Input.GetAxis("Jump");
		
		if(jump>0 && !inAir){
			rigidbody2D.AddForce(Vector2.up * jump * jumpForce);
			inAir= true;
		}
		
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - max_distance), -Vector2.up, 0.05f);
		if(hit.collider != null && !hit.collider.isTrigger){
			inAir= false;
		}
		
		if(desp<0) {
			transform.eulerAngles = new Vector3(0, 180, 0);
			transform.Translate(Vector2.right  * playerSpeed * Time.deltaTime* 0.02f);

			animator.SetBool("makewalk", true);
		}else if(desp>0) {
			transform.eulerAngles = new Vector3(0, 0, 0);
			transform.Translate(Vector2.right * playerSpeed * Time.deltaTime* 0.02f);
			animator.SetBool("makewalk", true);
		}else {
			animator.SetBool("makewalk" , false);
		}			
		
	}
}
