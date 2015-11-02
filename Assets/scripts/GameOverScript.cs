using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

	public Text scoreText;
	public Text highscoreText;

	// Use this for initialization
	void Start () {
		int score = PlayerPrefs.GetInt ("score", 0);
		int highscore = PlayerPrefs.GetInt ("highscore", 0);
		if (score > highscore) {
			highscore = score;
			PlayerPrefs.SetInt("highscore", highscore);
		}
		scoreText.text = "Score: " + score;
		highscoreText.text = "High Score: " + highscore;
	}
}
