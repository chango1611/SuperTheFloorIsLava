using UnityEngine;
using System.Collections;

public class ImaginaryPortal : MonoBehaviour {

	public MorphObjectManager[] morphs;
	public float imaginationBar;
	public float upSpeed, downSpeed;
	public SpriteRenderer GUI_Bar; 
	
	private bool onCollision;
	private Vector3 bar_scale;
	
	void Awake(){
		imaginationBar= 0;
	}
	
	void Start(){
		bar_scale= GUI_Bar.transform.localScale;
	}
	
	void Update(){
		imaginationBar+= Time.deltaTime*(!onCollision?-downSpeed:upSpeed);
		if(imaginationBar>1) imaginationBar= 1;
		if(imaginationBar<0) imaginationBar= 0;
		
		GUI_Bar.transform.localScale= new Vector3(imaginationBar * bar_scale.x,bar_scale.y,bar_scale.z);
		
		foreach(MorphObjectManager morph in morphs){
			morph.NewMorphFrame(imaginationBar);
		}
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		if(coll.tag=="Player"){
			onCollision= true;
		}
	}
	
	void OnTriggerExit2D(Collider2D coll){
		if(coll.tag=="Player"){
			onCollision= false;
		}
	}
}
