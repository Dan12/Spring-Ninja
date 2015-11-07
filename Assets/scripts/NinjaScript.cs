using UnityEngine;
using System.Collections;

public class NinjaScript : MonoBehaviour {

	public Sprite stand_sprite;
	public Sprite power_sprite;
	public Sprite jump_sprite;
	private SpriteRenderer spriteRenderer;

	public AudioClip jumpAudio;
	public AudioClip landAudio;
	public AudioClip powerAudio;
	public AudioClip explosionAudio;
	private AudioSource audioSource;

	private bool dying = false;

	void Start () {
		// placing the ninja
		transform.position = new Vector2(-2.55f, 2f);
		// tagging it as "Player"
		//tag = "Player";

		spriteRenderer = GetComponent<SpriteRenderer>();
		if (spriteRenderer.sprite == null){
			spriteRenderer.sprite = stand_sprite;
		}

		audioSource = GetComponent<AudioSource>();
	}
	
	void Update(){
		// checking if the ninja falls down the stage, in this case restart the game
		Vector2 stagePos = Camera.main.WorldToScreenPoint(transform.position);
		if (stagePos.y < 0 && !dying){
			GameObject.FindGameObjectsWithTag ("GameEngine") [0].SendMessage ("playerDied");
			audioSource.PlayOneShot(explosionAudio);
			dying = true;
			GetComponent<BoxCollider2D>().enabled = false;
			GetComponent<Rigidbody2D> ().freezeRotation = false;
			GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
			GetComponent<Rigidbody2D> ().AddForce(new Vector2(50f, 500f));
			GetComponent<Rigidbody2D> ().angularVelocity = 800f;
		}
		if(!dying)
			transform.position = new Vector2(-2.55f,transform.position.y);
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
		audioSource.Stop ();
		audioSource.PlayOneShot (jumpAudio);
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
		audioSource.PlayOneShot (landAudio);
	}

	public void powering(){
		spriteRenderer.sprite = power_sprite;
	}

	public void poweringUpSound(){
		audioSource.PlayOneShot (powerAudio);
	}
}
