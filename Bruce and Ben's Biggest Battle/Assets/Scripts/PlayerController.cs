using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	/*
	 * NOTES:
	 * Edge of the screen on the x-axis is 6.676 (26.704)
	 * Edge of the screen on the y-axis is 5 (20)
	 * Player off screen (with thrusters) on x-axis is 7.03
	 * Player off screen (with thrusters) on y-axis is 5.35
	 * 
	 * */

		
	public float thrust;
	public float torque;
	public float shotThrust;
	public Rigidbody2D rb;
	public GameObject turnRight;
	public GameObject turnLeft;
	public GameObject backThrusters;
	public Rigidbody2D shot;
	public static bool canMove = false;
//	private static int ths;
	
	void Start() {
		PlayerMovement par = this.transform.parent.GetComponent<PlayerMovement> ();
		rb = GetComponent<Rigidbody2D>();
		rb.angularDrag = par.angDrag;
		rb.drag = par.linDrag;
		this.thrust = par.thrust;
		this.torque = par.torque;
	}

	void Update() {
		float x = this.transform.position.x;
		float y = this.transform.position.y;

		if (canMove) {
			if (Input.GetKeyUp (KeyCode.Space) && this.tag == "Player" && ShotContoller.currentShots < ShotContoller.maxShots) {
				Shoot ();
			}
			if (Input.GetAxis ("Vertical") > 0) {
				rb.AddForce (transform.up * thrust);
				backThrusters.transform.position = new Vector3 (backThrusters.transform.position.x, backThrusters.transform.position.y, 0.15f);
			} 
			if (Input.GetAxis ("Horizontal") > 0) { //Press D. This is reversed with the torque.
				rb.AddTorque (Input.GetAxis ("Horizontal") * -torque, ForceMode2D.Force);
				turnRight.transform.position = new Vector3 (turnRight.transform.position.x, turnRight.transform.position.y, 0.15f);
			} else if (Input.GetAxis ("Horizontal") < 0) { //Press A.
				rb.AddTorque (Input.GetAxis ("Horizontal") * -torque, ForceMode2D.Force);
				turnLeft.transform.position = new Vector3 (turnLeft.transform.position.x, turnLeft.transform.position.y, 0.15f);
			}
		}
		if (Input.GetAxis ("Vertical") <= 0) {
			backThrusters.transform.position = new Vector3 (backThrusters.transform.position.x, backThrusters.transform.position.y, -11);
		}
		if (Input.GetAxis ("Horizontal") == 0) {
			turnRight.transform.position = new Vector3 (turnRight.transform.position.x, turnRight.transform.position.y, -11);
			turnLeft.transform.position = new Vector3 (turnLeft.transform.position.x, turnLeft.transform.position.y, -11);
		}
		if (x > 7.03 && rb.velocity.x > 0) { //Move to the left.
			this.transform.position = new Vector2(this.transform.position.x - 26.704f, this.transform.position.y);
		}
		else if (x < -7.03 && rb.velocity.x < 0) { //Move to the right.
			this.transform.position = new Vector2(this.transform.position.x + 26.704f, this.transform.position.y);
		}
		if (y > 5.35 && rb.velocity.y > 0) { //Move to the bottom.
			this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y - 20);
		} else if (y < -5.35 && rb.velocity.y < 0) { //Move to the top.
			this.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 20);
		}

		if (x <= 6.676 && x >= -6.676 && y > -5 && y < 5) { //Make sure the game knows which ship(s) can be hit.
			this.tag = "Player";
		} else {
			this.tag = "PlayerOff";
		}
	}

	void Shoot(){
		Rigidbody2D boom = (Rigidbody2D) Instantiate(shot, new Vector3(this.transform.position.x, this.transform.position.y, 0.1f), new Quaternion(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z, this.transform.rotation.w));
		boom.velocity = transform.up * shotThrust;
	}
}