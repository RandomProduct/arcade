using UnityEngine;
using System.Collections;

public class AsteroidController : MonoBehaviour {

	public GameObject p;
	Rigidbody2D rb;
	float x;
	float y;

	void Awake(){
		float theta = Random.Range (0.0f, 360.0f);
		float speed = Random.Range (10.0f, 100.0f);
		this.transform.parent.GetComponent<Rigidbody2D>().AddForce (new Vector2(speed*Mathf.Cos(theta), speed*Mathf.Sin(theta)));
		rb = this.transform.parent.GetComponent<Rigidbody2D>();
	}

	void Start(){
		p = GameObject.FindWithTag ("GameController");
	}

	void Update(){
		x = this.gameObject.transform.position.x;
		y = this.gameObject.transform.position.y;
		if (x > 7.03 && rb.velocity.x > 0) { //Move to the left.
			this.gameObject.transform.position = new Vector2(this.transform.position.x - 26.704f, this.transform.position.y);
		}
		else if (x < -7.03 && rb.velocity.x < 0) { //Move to the right.
			this.gameObject.transform.position = new Vector2(this.transform.position.x + 26.704f, this.transform.position.y);
		}
		if (y > 5.35 && rb.velocity.y > 0) { //Move to the bottom.
			this.gameObject.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 20);
		} else if (y < -5.35 && rb.velocity.y < 0) { //Move to the top.
			this.gameObject.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 20);
		}
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.GetComponent<Collider2D>().gameObject.tag == "Shot") {
			Destroy (collision.GetComponent<Collider2D>().gameObject);
			ShotContoller.currentShots--;
			if(this.name.Substring(0,3).Equals("Ast")){
				GameController.curAsteroids--;
			}
			PointController.points += 10;
			Destroy (this.gameObject.transform.parent.gameObject);
		} else if (collision.GetComponent<Rigidbody2D>().gameObject.tag == "Player" || collision.GetComponent<Rigidbody2D>().gameObject.tag == "PlayerOff") {
			Destroy(p);
			GameObject nam = GameObject.Find("Name");
			nam.GetComponent<MenuController>().moveEnterName();
			nam.GetComponent<MenuController>().StartCoroutine("takeInitials");
		}
	}
}
