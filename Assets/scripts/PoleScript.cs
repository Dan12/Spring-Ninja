using UnityEngine;
using System.Collections;

public class PoleScript : MonoBehaviour {

	void Start () {
		// tagging the object as Pole
		//tag = "Pole";
	}

	void Update(){
		// getting the real position, in pixels, of the pole on the stage
		Vector2 stagePos = Camera.main.WorldToScreenPoint(transform.position);
		// if the pole leaves the stage...
		if (stagePos.x < -20){
			Destroy(gameObject);
		}
	}
	
	void scroll(){
		// assigning pole an horizontal speed
		GetComponent<Rigidbody2D>().velocity = new Vector2(-2f, 0f);
	}

	void stop(){
		// assigning pole an horizontal speed
		GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 0f);
	}
}
