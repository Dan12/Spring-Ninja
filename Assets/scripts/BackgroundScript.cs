using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {

	private static BackgroundScript instance = null;
	public static BackgroundScript Instance {
		get { return instance; }
	}
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
}
