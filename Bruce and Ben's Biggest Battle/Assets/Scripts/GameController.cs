using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	/*
	 * NOTES:
	 * Edge of the screen on the x-axis is 6.676 (26.704)
	 * Edge of the screen on the y-axis is 5 (20)
	 * */

	public static int maxAsteroids = 4;
	public static int curAsteroids = 0;
	public static int level = 1;
	public static int goal = 100;
	private static float x;
	private static float y;

	public GameObject asteroids;
	public GameObject LevelUI;
	public GameObject Time;
	private GameObject player;

	// Use this for initialization
	void Awake () {
		if (this.name == "Level") {
			x = this.transform.position.x;
			y = this.transform.position.y;
			StartCoroutine("showLevel");
		}
		player = GameObject.FindWithTag ("Player");
	}

	IEnumerator showLevel(){
		curAsteroids = maxAsteroids;
		PlayerController.canMove = false;
		while (GameObject.Find ("SmallAsteroids(Clone)") != null) {
			DestroyImmediate(GameObject.Find("SmallAsteroids(Clone)"));
		}
		Text lev = LevelUI.GetComponent<Text> ();
		lev.transform.position = new Vector2 (x, y);
		lev.text = "Level: " + level + "\nGoal: " + goal;
		Text tim = Time.GetComponent<Text> ();
		tim.transform.position = new Vector2 (x, y - 75);
		tim.text = "3";
		yield return new WaitForSeconds (1);
		tim.text = "2";
		yield return new WaitForSeconds (1);
		tim.text = "1";
		yield return new WaitForSeconds (1);
		tim.transform.position += Vector3.up * 1000;
		lev.transform.position += Vector3.up * 1000;
		PlayerController.canMove = true;
		curAsteroids = 0;
	}

	void FixedUpdate(){
		player = GameObject.FindWithTag ("Player");
		if (curAsteroids < maxAsteroids) {
			createNewSmall();
		}
		if (PointController.points >= goal) {
			level++;
			maxAsteroids = (int)(maxAsteroids * 1.25);
			goal = goal + (int)(goal * 1.25);
			while(goal % 10 != 0){
				goal--;
			}
			if (this.name == "Level") {
			}
			StartCoroutine("showLevel");
		}
	}
	
	public void createNewSmall(){
		float pX = player.gameObject.transform.position.x;
		float pY = player.gameObject.transform.position.y;
		float randX;
		float randY;
		do {
			randX = Random.Range(-6.0f, 6.0f);
			randY = Random.Range(-4.5f, 4.5f);
		} while((randX <= pX + 3 && randX >= pX - 3) && (randY <= pY + 3 && randY >= pY - 3));
		Instantiate(asteroids, new Vector2(randX - 6.676f, randY - 5), Quaternion.identity);
		curAsteroids++;
	}

	public void createNewBig(){
		float pX = player.gameObject.transform.position.x;
		float pY = player.gameObject.transform.position.y;
		float randX;
		float randY;
		do {
			randX = Random.Range(-6.0f, 6.0f);
			randY = Random.Range(-4.5f, 4.5f);
		} while((randX <= pX + 3 && randX >= pX - 3) && (randY <= pY + 3 && randY >= pY - 3));
		Instantiate(asteroids, new Vector2(randX - 6.676f, randY - 5), Quaternion.identity);
		curAsteroids += 3;
	}
}
