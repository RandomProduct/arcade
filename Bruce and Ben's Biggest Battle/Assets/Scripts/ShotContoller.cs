using UnityEngine;
using System.Collections;

public class ShotContoller : MonoBehaviour {

	public static int maxShots = 4;
	public static int currentShots = 0;

	// Use this for initialization
	void Start () {
		currentShots++;
		StartCoroutine (decay());
	}
	
	// Update is called once per frame
	void Update () {
		float x = this.gameObject.transform.position.x;
		float y = this.gameObject.transform.position.y;

		if (x < -6.676) {
			transform.position = new Vector2 (x + 6.676f * 2, y);
		} else if (x > 6.676) {
			transform.position = new Vector2 (x - 6.676f * 2, y);
		}
		if (y > 5) {
			transform.position = new Vector2 (x, y - 10);
		} else if (y < -5) {
			transform.position = new Vector2 (x, y + 10);
		}
	}

	IEnumerator decay(){
		yield return new WaitForSeconds (1);
		currentShots--;
		Destroy (this.gameObject);
	}
}
