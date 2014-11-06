using UnityEngine;
using System.Collections;

public class GUIKeyDemo : MonoBehaviour {

	//Este script es temporal para mostrar como agregar y quitar llaves del GUI
	public SpriteRenderer[] keys;
	private int num_keys;
	private bool key_change;

	// Use this for initialization
	void Start () {
		num_keys= 0;
	}
	
	// Update is called once per frame
	void Update () {
		key_change= false;
		if(Input.GetKeyDown(KeyCode.Z)){
			num_keys+= num_keys==keys.Length?0:1;
			key_change= true;
		}
		if(Input.GetKeyDown(KeyCode.X)){
			num_keys-= num_keys==0?0:1;
			key_change= true;
		}
		if(key_change){
			int i;
			for(i=0; i<num_keys;i++)
				keys[i].enabled= true;
			for(;i<keys.Length;i++)
				keys[i].enabled= false;
		}
	}
}
