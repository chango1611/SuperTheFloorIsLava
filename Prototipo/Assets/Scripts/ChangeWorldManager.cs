using UnityEngine;
using System.Collections;
using System.IO;

public class ChangeWorldManager : MonoBehaviour {

	public Animator changeWorldAnim;
	public bool pong= false; //Indica si se desea un efecto pong o no
	
	// Inicialisacion
	void Awake () {
		changeWorldAnim.speed= 0;
	}
	
	//Metodo para la animacion del morph
	public void AnimateMorph(float speed){
		changeWorldAnim.speed= speed;
	}
	
	public void StopAnimateMorph(){
		changeWorldAnim.speed= 0;
	}
	
	//Metodo para crear un nuevo frame
	public void NewMorphFrame (float currentFrame) {
		changeWorldAnim.Play("changeWorld",-1, currentFrame);
	}
}
