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
	private float maxPoleDistance = -5f;

	private bool fadeout = false;
	private float opacity = 1f;
	public float fadeSpeed = 5f;

	public GameObject explosion;

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
			GameObject tempPole = Instantiate(poleObject);
			//Instantiate(poleObject);
			// position the pole prefab
			if(posX < maxPoleDistance+minPoleGap)
				posX = maxPoleDistance+minPoleGap;
			if(posX > maxPoleDistance+maxPoleGap)
				posX = maxPoleDistance+maxPoleGap;
			tempPole.transform.position = new Vector2(posX,-1.5f-Random.value*2.5f);
			maxPoleDistance = posX;
			// determining next pole position
			posX += minPoleGap+Random.value*(maxPoleGap-minPoleGap);
			// try to place another pole\
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

			GameObject.FindWithTag("Player").SendMessage("powering");
			GameObject.FindWithTag("Player").SendMessage("poweringUpSound");

			findMaxPoleDist();
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

		if (fadeout) {
			GameObject[] poles = GameObject.FindGameObjectsWithTag("Pole");
			Color polecolor = new Color(255,255,255,opacity);
			foreach (GameObject pole in poles){
				// looking for the rightmost pole
				pole.GetComponent<SpriteRenderer>().color = polecolor;
			}
			opacity-=fadeSpeed/255;
			if(opacity <= 0){
				fadeout = false;
				Application.LoadLevel(2);
			}
		}
	}

	void findMaxPoleDist(){
		// once the player is charging, check if there's enough poles
		GameObject[] poles = GameObject.FindGameObjectsWithTag("Pole");
		maxPoleDistance = 0;
		foreach (GameObject pole in poles){
			// looking for the rightmost pole
			maxPoleDistance = Mathf.Max(maxPoleDistance,pole.transform.position.x);
		}
	}

	public void pointEarned(){
		score++;
		scoreText.text = "Score: " + score;
	}

	public void playerDied(){
		GameObject tempexp = Instantiate (explosion);
		PlayerPrefs.SetInt ("score", score);
		Invoke("startFadeOut", 2.0f);
	}

	void startFadeOut(){
		fadeout = true;
	}
}
