using UnityEngine;
using System.Collections;

public class NinjaScript : MonoBehaviour {

	void Start () {
		// placing the ninja
		transform.position = new Vector2(-2.5f, 2f);
		// tagging it as "Player"
		//tag = "Player";
	}
	
	void Update(){
		// checking if the ninja falls down the stage, in this case restart the game
		Vector2 stagePos = Camera.main.WorldToScreenPoint(transform.position);
		if (stagePos.y < 0){
			Application.LoadLevel(Application.loadedLevel);
		}
	}
	
	void Jump(float jumpForce){
		// adding a vertical force to make the ninja jump
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
	}
	
	void OnCollisionEnter2D(){
		// when the ninja collides, find "Game Engine" object, then find "MainScript" script and set its ninjaJumping variable to false
		GameObject main = GameObject.Find("Game Engine");
		MainScript script = main.GetComponent("MainScript") as MainScript;
		script.ninjaJumping = false;
	}
}
