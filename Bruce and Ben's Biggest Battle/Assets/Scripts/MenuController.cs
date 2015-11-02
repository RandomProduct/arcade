using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

public class MenuController : MonoBehaviour {

	private bool done = false;
	Text instruction;
	Button play;
	static string[] initials = new string[]{null,null,null};
	public bool debugMode;
	string[] alphabet = new string[]{"A","B","C","D","E","F","G","H","I","J","K","L","M","N","O","P","Q","R","S","T","U","V","W","X","Y","Z",
		"a","b","c","d","e","f","g","h","i","j","k","l","m","n","o","p","q","r","s","t","u","v","w","x","y","z"};

	// Update is called once per frame
	void Update () {
		switch(this.name){
		case "Title":
			instruction = GetComponent<Text> ();
			instruction.text = string.Format ("{0,-12}", "BRUCE AND BEN'S\nBIGGEST BATTLE");
			if(!done){
				instruction.transform.position += Vector3.up * 270;
				done = true;
			}
		break;
		case "ScoreboardNames":
			instruction = GetComponent<Text>();
			instruction.text = "HIGH SCORES\n\t   1. " + PointController.HSNames[9] + "\n" +
				"\t   2. " + PointController.HSNames[8] + "\n" +
					"\t   3. " + PointController.HSNames[7] + "\n" +
					"\t   4. " + PointController.HSNames[6] + "\n" +
					"\t   5. " + PointController.HSNames[5] + "\n" +
					"\t   6. " + PointController.HSNames[4] + "\n" +
					"\t   7. " + PointController.HSNames[3] + "\n" +
					"\t   8. " + PointController.HSNames[2] + "\n" +
					"\t   9. " + PointController.HSNames[1] + "\n" +
					"\t 10. " + PointController.HSNames[0];
			if(!done){
				instruction.transform.position -= Vector3.right * 100;
				instruction.transform.position += Vector3.up * 100;
				done = true;
			}
		break;
		case "ScoreboardPoints":
			instruction = GetComponent<Text>();
			instruction.text = "POINTS\n" + PointController.HSPoints[9] + "\n" +
				 	PointController.HSPoints[8] + "\n"
					+ PointController.HSPoints[7] + "\n"
					+ PointController.HSPoints[6] + "\n"
					+ PointController.HSPoints[5] + "\n"
					+ PointController.HSPoints[4] + "\n"
					+ PointController.HSPoints[3] + "\n"
					+ PointController.HSPoints[2] + "\n"
					+ PointController.HSPoints[1] + "\n"
					+ PointController.HSPoints[0] + "\n";
			if(!done){
				instruction.transform.position += Vector3.right * 90;
				instruction.transform.position += Vector3.up * 100;
				done = true;
			}
			break;
		case "Button":
			play = GetComponent<Button>();
			if(!done){
				play.transform.position -= Vector3.up * 250;
				play.transform.position -= Vector3.right * 20;
				done = true;
			}
		break;
		case "Score":
			instruction = GetComponent<Text>();
			instruction.text = "Score: " + PointController.points;
			if(!done){
				instruction.transform.position += Vector3.up * 425;
				instruction.transform.position += Vector3.right * 300;
				done = true;
			}
		break;
		case "Name":
			instruction = GetComponent<Text>();
			instruction.text = initials[0] + initials[1] + initials[2];
			if(!done){
				instruction.transform.position += Vector3.right * 35;
				instruction.transform.position += Vector3.up * 10;
				done = true;
			}
			break;
		}
	}

	public void startGame(){
		initials = new string[]{null,null,null};
		GameController.level = 1;
		GameController.goal = 100;
		GameController.maxAsteroids = 4;
		GameController.curAsteroids = 0;
		Application.LoadLevel ("MainGame");
	}

	public void moveEnterName(){
		instruction = GameObject.Find("EnterName").GetComponent<Text>();
		instruction.text = "Type your initials\n\n\nPress Enter when ready";
		instruction.transform.position = new Vector2(435,365);
	}

	IEnumerator takeInitials(){
		while (debugMode) {
			string temp = Input.inputString; //Just to make sure nothing goes wrong.
			if (ArrayUtility.Contains (alphabet, temp)) {
				if(initials[0] == null){
					initials[0] = temp.ToUpper();
				}
				else if(initials[1] == null){
					initials[1] = temp.ToUpper();
				}
				else if(initials[2] == null){
					initials[2] = temp.ToUpper();
				}
			}
			else if(Input.GetKeyDown(KeyCode.Backspace)){
				if(initials[2] != null){
					initials[2] = null;
				}
				else if(initials[1] != null){
					initials[1] = null;
				}
				else if(initials[0] != null){
					initials[0] = null;
				}
			}
			else if(Input.GetKeyDown(KeyCode.Return)){
				if(initials[1] != null){
					PointController.pName = initials[0] + initials[1] + initials[2];
					Application.LoadLevel("MainMenu");
				}
			}
			yield return new WaitForSeconds (0.01f);
		}
	}
}
