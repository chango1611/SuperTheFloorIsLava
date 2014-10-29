using UnityEngine;
using System.Collections;

public class ImaginaryPortal2 : MonoBehaviour {

	public ImageCrossDisolving[] morphs;
	public float imaginationBar;
	public float upSpeed, downSpeed;
	
	private bool onCollision;
	
	void Awake(){
		imaginationBar= 0;
	}
	
	void Update(){
		imaginationBar+= Time.deltaTime*(!onCollision?-downSpeed:upSpeed);
		if(imaginationBar>1) imaginationBar= 1;
		if(imaginationBar<0) imaginationBar= 0;
		
		foreach(ImageCrossDisolving morph in morphs){
			morph.NewMorphFrame(Mathf.RoundToInt(imaginationBar*(morph.numberOfFrames-1)));
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
