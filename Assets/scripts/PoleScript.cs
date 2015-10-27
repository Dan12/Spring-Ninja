using UnityEngine;
using System.Collections;

public class PoleScript : MonoBehaviour {

	void Start () {
		// tagging the object as Pole
		//gameObject.tag = "Pole";
	}
	
	void scroll(){
		// assigning pole an horizontal speed
		GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 0f);
	}
}
