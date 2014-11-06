using UnityEngine;
using System.Collections;

public class PlayerManager : MonoBehaviour {

	public float playerSpeed=400, jumpForce=1500;

	private bool inAir= false;
	private Animator animator;
	private float y_offset, x_offset;
	
	//Esto deberia ir en un Game Manager
	public AudioListener listener;
	private Rect PauseMenu = new Rect(Screen.width*0.5f-100.0f, Screen.height*0.5f-60.0f, 200, 120);
	private bool inPause= false;
	private bool mute= false;

	void Start () {
		animator = GetComponent<Animator>() as Animator;
		SpriteRenderer aux= GetComponent<SpriteRenderer>() as SpriteRenderer;
		y_offset= transform.localScale.y * aux.sprite.rect.height/200 + 0.1f;
		x_offset= transform.localScale.x * aux.sprite.rect.width/200 * 0.2f;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float desp= Input.GetAxisRaw("Horizontal");
		float jump= Input.GetAxis("Jump");
		
		if(jump>0 && !inAir){
			rigidbody2D.AddForce(Vector2.up * jump * jumpForce);
		}
		
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + x_offset, transform.position.y - y_offset), -Vector2.up, 0.01f);
		RaycastHit2D hit2 = Physics2D.Raycast(new Vector2(transform.position.x - x_offset, transform.position.y - y_offset), -Vector2.up, 0.01f);
		if((hit.collider != null && !hit.collider.isTrigger) || (hit2.collider !=null && !hit2.collider.isTrigger)){
			inAir= false;
		}else{
			inAir= true;
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
	
	//Esto debe ir en el GameManager
	//Desde aqui-------------------------------------------------------------->
	void Update(){
		if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)){
			inPause= !inPause;
			Time.timeScale= 1 - Time.timeScale;
		}
	}
	
	void OnGUI(){
		if(inPause)
			GUI.Window(0, PauseMenu, ThePauseMenu, "Pause Menu");
	}
	
	void ThePauseMenu(int e){
		if(GUILayout.Button("Continue")){
			inPause= false;
			Time.timeScale = 1;
		}
		if(GUILayout.Button("Restart")){
			Time.timeScale = 1;
			Application.LoadLevel(Application.loadedLevel);
		}
		if(GUILayout.Button(mute?"Unmute":"Mute")){
			mute= !mute;
			listener.enabled= !mute;
		}
		
		if(GUILayout.Button("Quit")){
			Time.timeScale = 1;
			Application.Quit();
		}
	}
	//<-------------------------------------------------------------Hasta aqui
}
