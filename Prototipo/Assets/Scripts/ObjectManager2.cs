using UnityEngine;
using System.Collections;

public class ObjectManager2 : MonoBehaviour {

	public ImageCrossDisolving Morph;
	public int currentFrame= 0;
	
	private int oldFrame= 0;
	
	void Start () {
		//Morph.AnimateMorph(); //Realiza la animacion del morph
	}
	
	//Permite manejar el morph de manera manual cambiando en el editor el currentFrame
	void Update(){
		if(currentFrame>Morph.numberOfFrames-1) currentFrame= Morph.numberOfFrames-1;
		if(currentFrame<0) currentFrame= 0;
		//Este condicional es para evitar consumo adicional del procesador
		if(currentFrame!=oldFrame){
			oldFrame= currentFrame;
			Morph.NewMorphFrame(currentFrame);
		}
	}
}
