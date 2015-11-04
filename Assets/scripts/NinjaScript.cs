using UnityEngine;
using System.Collections;

public class NinjaScript : MonoBehaviour {

	public Sprite stand_sprite;
	public Sprite power_sprite;
	public Sprite jump_sprite;
	private SpriteRenderer spriteRenderer;

	void Start () {
		// placing the ninja
		transform.position = new Vector2(-2.5f, 2f);
		// tagging it as "Player"
		//tag = "Player";

		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer.sprite == null){
			spriteRenderer.sprite = stand_sprite;
		}
	}
	
	void Update(){
		// checking if the ninja falls down the stage, in this case restart the game
		Vector2 stagePos = Camera.main.WorldToScreenPoint(transform.position);
		if (stagePos.y < 0){
			GameObject.FindGameObjectsWithTag ("GameEngine") [0].SendMessage ("playerDied");
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
		spriteRenderer.sprite = jump_sprite;
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (gameObject.GetComponent<Rigidbody2D> ().velocity.y == 0) {
			GameObject.FindGameObjectsWithTag ("GameEngine") [0].SendMessage ("pointEarned");
			gameObject.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			powering();
			Invoke ("standing", 0.05f);
		}
		// get all objects tagged with "Pole"
		GameObject[] poles = GameObject.FindGameObjectsWithTag("Pole");
		foreach (GameObject pole in poles){
			// send them all "scroll" message
			pole.SendMessage("stop");
		}
	}

	public void standing(){
		spriteRenderer.sprite = stand_sprite;
	}

	public void powering(){
		spriteRenderer.sprite = power_sprite;
	}
}
