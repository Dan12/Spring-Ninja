using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {

	// all these variables will be declared in the inspector
	
	public GameObject ninjaObject; // the ninja prefab	
	public GameObject poleObject; // the pole prefab
	public GameObject powerBarObject; // the pole bar prefab
	public float jumpForce; // jump force - vertical only
	public float maxJumpForce; // maximum vertical jump force
	public bool ninjaJumping = true; // is the ninja jumping? we start with "true" because the ninja will fall from the top of the stage

	public Text scoreText;
	private int score = -1;

	private bool isCharging = false; // is the ninja charging the jump?
	private float minPoleGap = 1.5f; // min distance between two poles
	private float maxPoleGap = 2.5f; // max distance between two poles 

	// function executed when the script is launched
	
	void Start () {
		// placing the ninja prefab on the stage
		Instantiate(ninjaObject);
		// placing the pole prefabs on the stage
		placePole (-2.5f);
	}

	void placePole(float posX){
		// if the pole is not too far, then place it
		if(posX<10f){
			// adding the prefab to the stage
			Instantiate(poleObject);
			// position the pole prefab
			poleObject.transform.position = new Vector2(posX,-1.5f-Random.value*2.5f);
			// determining next pole position
			posX += minPoleGap+Random.value*(maxPoleGap-minPoleGap);
			// try to place another pole
			placePole (posX);
		}
	}
	
	// function executed at each frame
	
	void Update () {
		// mouse button down and ninja is not charging and not jumping/falling - that is its y velocity is zero?
		if (Input.GetButtonDown("Fire1") && GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity.y==0 && !isCharging) {
			// let's place the power bar on the stage
			GameObject powerBar = Instantiate(powerBarObject) as GameObject;
			// now the player is charging
			isCharging = true;
			// once the player is charging, check if there's enough poles
			GameObject[] poles = GameObject.FindGameObjectsWithTag("Pole");
			float maxPoleDistance = 0;
			foreach (GameObject pole in poles){
				// looking for the rightmost pole
				maxPoleDistance = Mathf.Max(maxPoleDistance,pole.transform.position.x);
			}
			// if the rightmost pole is not too far, try to place another pole.
			if(maxPoleDistance<10f){
				placePole (maxPoleDistance+minPoleGap+Random.value*(maxPoleGap-minPoleGap));
			}
		}
		// mouse button released and the ninja is charging but not jumping/falling - that is its y velocity is zero?
		if (Input.GetButtonUp("Fire1") && GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity.y==0 && isCharging) {
			// player is no longer charging
			isCharging = false;
			// get the game object tagged as "Power"
			GameObject powerObject = GameObject.FindWithTag("Power");
			// inside the object tagged as "Power", get PowerScript script
			PowerScript script = powerObject.GetComponent("PowerScript") as PowerScript;
			// destoy the power bar
			Destroy(GameObject.FindWithTag("Power"));
			// find the object tagged as "Player" and send "Jump" message, with the proper force
			GameObject.FindWithTag("Player").SendMessage("Jump",maxJumpForce*script.chargePower+50);
		}
	}

	public void pointEarned(){
		score++;
		scoreText.text = "Score: " + score;
	}
}
