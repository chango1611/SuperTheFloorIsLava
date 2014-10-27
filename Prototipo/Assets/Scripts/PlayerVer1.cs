using UnityEngine;
using System.Collections;

public class PlayerVer1 : MonoBehaviour {

	public ImageCrossDisolving[] morphs;
	public float imaginationBar;
	public float upSpeed, downSpeed, playerSpeed=400, jumpForce=1500;

	private bool inAir= false;
	private Animator animator;

	void Awake(){
		imaginationBar= 0;
		animator = GetComponent<Animator>() as Animator;
		Debug.Log (animator);
	}
	
	void Start(){
	}

	// Update is called once per frame
	void FixedUpdate () {
		float desp= Input.GetAxis("Horizontal");
		float jump= Input.GetAxis("Jump");
		
		if(jump>0 && !inAir){
			rigidbody2D.AddForce(Vector2.up * Time.deltaTime*jump*jumpForce);
		}
		if(desp<0){
			transform.eulerAngles = new Vector3(0, 180, 0);
			animator.SetTrigger("makewalk");
		}
		
		if(desp>0){
			transform.eulerAngles = new Vector3(0, 0, 0);
			animator.SetBool("makewalk", true);
		}
		
		rigidbody2D.velocity= new Vector2 (Time.deltaTime*desp*playerSpeed , rigidbody2D.velocity.y);
		
		//Calcula el imaginationBar que esta en el rango 0 y 1
		imaginationBar+= Time.deltaTime*(desp+jump==0?-downSpeed:upSpeed);
		if(imaginationBar>1) imaginationBar= 1;
		if(imaginationBar<0) imaginationBar= 0;
		
		
			
		if(desp == 0){
			animator.SetBool("makewalk", false);
		}
		
		foreach(ImageCrossDisolving morph in morphs){
			morph.NewMorphFrame(Mathf.RoundToInt(imaginationBar*(morph.numberOfFrames-1)));
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
