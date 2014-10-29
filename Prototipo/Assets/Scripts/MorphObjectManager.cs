using UnityEngine;
using System.Collections;
using System.IO;

public class MorphObjectManager : MonoBehaviour {

	public Animator morphObjectAnim;
	
	// Inicialisacion
	void Awake () {
		morphObjectAnim.speed= 0;
	}
	
	//Metodo para la animacion del morph
	public void AnimateMorph(float speed){
		morphObjectAnim.speed= speed;
	}
	
	public void StopAnimateMorph(){
		morphObjectAnim.speed= 0;
	}
	
	//Metodo para crear un nuevo frame
	public void NewMorphFrame (float currentFrame) {
		morphObjectAnim.Play("changeWorld",-1, currentFrame);
	}
}
