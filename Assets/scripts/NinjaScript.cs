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
		transform.position = new Vector2(-2.5f,transform.position.y);
	}
	
	void Jump(float jumpForce){
		// adding a vertical force to make the ninja jump
		GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
		GameObject[] poles = GameObject.FindGameObjectsWithTag("Pole");
		foreach (GameObject pole in poles){
			// send them all "scroll" message
			pole.SendMessage("scroll");
		}
	}
	
	void OnCollisionEnter2D(Collision2D collision){
		if (collision.contacts [0].point.y >= collision.gameObject.transform.position.y)
			GameObject.FindGameObjectsWithTag ("GameEngine") [0].SendMessage ("pointEarned");
		// get all objects tagged with "Pole"
		GameObject[] poles = GameObject.FindGameObjectsWithTag("Pole");
		foreach (GameObject pole in poles){
			// send them all "scroll" message
			pole.SendMessage("stop");
		}
	}
}
