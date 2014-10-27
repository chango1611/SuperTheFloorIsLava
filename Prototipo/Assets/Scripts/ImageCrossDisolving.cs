using UnityEngine;
using System.Collections;
using System.IO;

public class ImageCrossDisolving : MonoBehaviour {

	public SpriteRenderer spriteRenderer; //Sprite Renderer del objeto que se vaya a utilizar en el morph
	public Sprite first; //Sprite de la primera imagen
	public Sprite second; //Sprite de la segunda imagen
	public int numberOfFrames= 50; //Cantidad de frames que existiran entre ambos sprites
	public float animationTime= 1; //Tiempo total que durara la animacion
	public float animationSpeed= 1; //Velocidad a la que se ejecuta la animacion
	public int direction= 1; //Direccion de la animacion en caso de usar AnimateMorph
	public bool pong= false; //Indica si se desea un efecto pong o no

	private float frameTime;
	private Color[] firstPixels, secondPixels, currentPixels;
	private Texture2D[] currentTexture;
	private int currentFrame= 0; //Frame Actual de la animacion
	private int padding_x, padding_y;
	
	// Inicialisacion
	void Awake () {
		if(direction>=0) direction= 1;
		else direction= -1;
		
		//Inicializacion de variables privadas
		frameTime= animationTime/(animationSpeed*numberOfFrames);
		firstPixels= first.texture.GetPixels((int)first.rect.x,(int)first.rect.y,(int)first.rect.width,(int)first.rect.height);
		secondPixels= second.texture.GetPixels((int)second.rect.x,(int)second.rect.y,(int)second.rect.width,(int)second.rect.height);
		
		int mod, div;
		mod= (int)spriteRenderer.sprite.rect.x % (int)spriteRenderer.sprite.rect.width;
		div= (int)spriteRenderer.sprite.rect.x / (int)spriteRenderer.sprite.rect.width + 1;
		padding_x= mod/div;
		
		mod= (int)spriteRenderer.sprite.rect.y % (int)spriteRenderer.sprite.rect.height;
		div= (int)spriteRenderer.sprite.rect.y / (int)spriteRenderer.sprite.rect.height + 1;
		padding_y= mod/div;
		
		currentPixels= new Color[firstPixels.Length];
		currentTexture= new Texture2D[numberOfFrames];
	}
	
	//Metodo para la animacion del morph
	public void AnimateMorph(){
		currentFrame+= direction;
		
		NewMorphFrame(currentFrame);
		
		if(pong && (currentFrame==numberOfFrames-1 || currentFrame==0))
			direction= -direction;
		
		if(pong || (!pong && currentFrame<numberOfFrames-1 && currentFrame>0))
			Invoke("AnimateMorph",frameTime);
	}
	
	//Metodo para crear un nuevo frame
	public void NewMorphFrame (int currentFrame) {
		this.currentFrame= currentFrame;
		if(currentTexture[currentFrame]==null){
			float t= (float)currentFrame/(float)numberOfFrames;
			
			for(int i= 0; i<currentPixels.Length; i++)
				currentPixels[i]= (1-t)*firstPixels[i] + t*secondPixels[i];
			
			currentTexture[currentFrame] = new Texture2D(/*first.texture.width,first.texture.height);//*/(int)first.rect.width+padding_x,(int)first.rect.height+padding_y);
			currentTexture[currentFrame].SetPixels(/*(int)first.rect.x+*/padding_x,/*(int)first.rect.y+*/padding_y,(int)first.rect.width,(int)first.rect.height,currentPixels);
			currentTexture[currentFrame].Apply();
			
			/*Esto estoy seguro que funciona en caso de que lo de arriba no funcione para casos especificos
			currentTexture[currentFrame] = new Texture2D(first.texture.width,first.texture.height);
			currentTexture[currentFrame].SetPixels((int)first.rect.x,(int)first.rect.y,(int)first.rect.width,(int)first.rect.height,currentPixels);
			currentTexture[currentFrame].Apply();*/
		}
		
		//spriteRenderer= new SpriteRenderer();
		spriteRenderer.sprite= Sprite.Create(currentTexture[currentFrame],spriteRenderer.sprite.rect, new Vector2 (0.5f, 0.5f),100);
		spriteRenderer.sprite.name= "Morph_"+currentFrame;
		spriteRenderer.material.mainTexture = currentTexture[currentFrame] as Texture;
		//spriteRenderer.material.shader= Shader.Find("Sprites/Default");
		
	}
}
