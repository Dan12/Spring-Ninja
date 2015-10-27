using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour {

	// all these variables will be declared in the inspector
	
	public GameObject ninjaObject; // the ninja prefab	
	public GameObject poleObject; // the pole prefab
	public GameObject powerBarObject; // the pole bar prefab
	public float jumpForce; // jump force - vertical only
	public float maxJumpForce; // maximum vertical jump force
	public bool ninjaJumping = true; // is the ninja jumping? we start with "true" because the ninja will fall from the top of the stage
	
	// function executed when the script is launched
	
	void Start () {
		// placing the ninja prefab on the stage
		Instantiate(ninjaObject);
		// placing the pole prefab on the stage
		Instantiate(poleObject);
		// setting pole position
		poleObject.transform.position = new Vector2(-2.5f, -2f);
	}
	
	// function executed at each frame
	
	void Update () {
		print ("hey");
		print (Input.GetButtonDown ("Fire1"));
		// mouse button down and the ninja is not jumping?
		if (Input.GetButtonDown("Fire1") && !ninjaJumping) {
			// let's place the power bar on the stage
			GameObject powerBar = Instantiate(powerBarObject) as GameObject;
		}
		// mouse button released and the ninja is not jumping?
		if (Input.GetButtonUp("Fire1") && !ninjaJumping) {
			// the ninja now is jumping 
			ninjaJumping = true;
			// get the game object tagged as "Power"
			GameObject main = GameObject.FindWithTag("Power");
			print (main);
			if(main != null){
				// inside the object tagged as "Power", get PowerScript script
				PowerScript script = main.GetComponent("PowerScript") as PowerScript;
				// destoy the power bar
				Destroy(GameObject.FindWithTag("Power"));
				// find the object tagged as "Player" and send "Jump" message, with the proper force
				GameObject.FindWithTag("Player").SendMessage("Jump",maxJumpForce*script.chargePower);
				// get all objects tagged with "Pole"
				GameObject[] poles = GameObject.FindGameObjectsWithTag("Pole");
				foreach (GameObject pole in poles){
					// send them all "scroll" message
					pole.SendMessage("scroll");
				}
			}
			else{
				ninjaJumping = false;
			}
		}
	}
}
