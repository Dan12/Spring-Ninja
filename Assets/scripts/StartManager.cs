using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartManager : MonoBehaviour {

	public Text buttontext;
	public Button button;

	private float opacity = 1.0f;
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
			opacity-=(float)5/255;
			c.r = buttontext.color.r;
			c.g = buttontext.color.g;
			c.b = buttontext.color.b;
			buttontext.color = c;
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
