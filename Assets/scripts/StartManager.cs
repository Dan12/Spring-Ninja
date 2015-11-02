using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartManager : MonoBehaviour {

	public Text[] buttontext;
	public Button button;
	public float fadeSpeed = 5;

	private float opacity = 1f;
	private bool fading = false;
	
	// Update is called once per frame
	void Update () {
		if (fading) {
			Color c;
			c.r = button.image.color.r;
			c.g = button.image.color.g;
			c.b = button.image.color.b;
			c.a = opacity;
			button.image.color = c;
			for(var i = 0; i < buttontext.Length; i++){
				c.r = buttontext[i].color.r;
				c.g = buttontext[i].color.g;
				c.b = buttontext[i].color.b;
				buttontext[i].color = c;
			}
			opacity-=fadeSpeed/255;
			if(opacity <= 0)
				playScene();
		}
	}

	public void clickStartButton(){
		fading = true;
	}

	void playScene(){
		Application.LoadLevel (1);
	}
}
