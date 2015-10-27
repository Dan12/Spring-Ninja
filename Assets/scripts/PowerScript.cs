using UnityEngine;
using System.Collections;

public class PowerScript : MonoBehaviour {

	public float chargePower = 0;
	
	void Start () {
		// tagging the object as "Power"
		//tag = "Power";
		// horizontally scaling the object to zero
		transform.localScale = new Vector2(0f,1f);
	}
	
	void Update () {
		// adding chargePower the elapsed time until it reaches 1
		chargePower = Mathf.Min(chargePower+Time.deltaTime,1f);
		// setting local scale accordingly
		transform.localScale = new Vector2(chargePower,1f);
		// finally updating its position to give the feeling it's growing from left to right
		transform.position = new Vector2(-3.0f+chargePower/2, 2.15f);
	}
}
