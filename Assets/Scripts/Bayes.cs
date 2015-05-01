using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public struct EscapeRoute {
    public GameObject bank;
    public GameObject escapePoint;
}

public class Bayes {

    static Dictionary<EscapeRoute, int> successfulRobberies;

	// Use this for initialization
	public static void Initialize () {
        successfulRobberies = new Dictionary<EscapeRoute, int>();
	    for (int i = 0; i < City.banks.Length; i++) {
            for (int j = 0; j < City.escapes.Length; j++) {
                EscapeRoute tempEscape;
                tempEscape.bank = City.banks [i];
                tempEscape.escapePoint = City.escapes [j];
                successfulRobberies.Add (tempEscape, 1);
            }
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public static int getSuccessfulRobberies(GameObject bank, GameObject escape) {
        EscapeRoute tempEscape;
        tempEscape.bank = bank;
        tempEscape.escapePoint = escape;
        //Debug.Log("Getting successful robberies for: Bank: " + bank + ", Escape: " + escape);
        return successfulRobberies [tempEscape];
	}

    public static void reportRobbery(GameObject bank, GameObject escape, bool success) {
        EscapeRoute tempEscape;
        tempEscape.bank = bank;
        tempEscape.escapePoint = escape;
        //Debug.Log ("Reporting the robbery as " + bank + " " + escape + " " + success);
        if (success)
            successfulRobberies [tempEscape] += 1;
        else {
            if (successfulRobberies [tempEscape] > 1) successfulRobberies[tempEscape] -= 1;
        }
    }
}