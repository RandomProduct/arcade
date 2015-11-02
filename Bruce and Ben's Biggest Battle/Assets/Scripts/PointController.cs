using UnityEngine;
using System.Collections;

public class PointController : MonoBehaviour {
	
	public static string pName;
	public static int points;
	public static ArrayList HSNames = new ArrayList{"SGW", "WHG", "SRC", "RH", "MRW", "JST", "DES", "HAA", "MJR", "CRJ"};
	public static ArrayList HSPoints = new ArrayList{60,110,180,200,420,500,510,680,760,1000};

	// Use this for initialization
	void Start () {
		if (GameObject.FindGameObjectsWithTag ("GameMaster").Length > 1) {
			Destroy(GameObject.FindWithTag("GameMaster"));
		}
		DontDestroyOnLoad (this.gameObject);
		newHS ();
		points = 0;
	}

	public static void newHS(){
		object[] arr = HSPoints.ToArray ();
		int i = 0;
		while (points >= (int)arr[i]) {
			i++;
			if(i + 1 > arr.Length){ //Let's make sure we don't go out of bounds.
				break;
			}
		}
		if (i > 0) {
			HSPoints.Insert (i, points);
			HSPoints.RemoveAt (0);
			HSNames.Insert (i, pName);
			HSNames.RemoveAt (0);
		}
	}
}