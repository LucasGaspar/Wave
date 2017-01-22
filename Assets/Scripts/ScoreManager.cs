using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour {

	public static ScoreManager SM;

	public GameObject[] totems;

	public int p1Score = 0;
	public int p2Score = 0;
	public int p3Score = 0;
	public int p4Score = 0;
	public int p5Score = 0;
	public int p6Score = 0;

	void Awake(){
		if (SM == null) {
			SM = this;
			DontDestroyOnLoad (this);
		} else {
			Destroy (this);
		}

	}

	public void UpdateScores(){
		print ("get totems");
		Invoke ("GetRemainingTotems", 2);
	}

	void GetRemainingTotems(){
		totems = GameObject.FindGameObjectsWithTag ("Totem");
		if (totems.Length == 1) {
			string winner = totems [0].GetComponent<TotemControl> ().buttonName;
			print (winner);
			switch (winner) {
			case "1":
				p1Score++;
				break;
			case "2":
				p2Score++;
				break;
			case "3":
				p3Score++;
				break;
			case "4":
				p4Score++;
				break;
			case "5":
				p6Score++;
				break;
			case "6":
				p6Score++;
				break;
			}
		}
	}
}
