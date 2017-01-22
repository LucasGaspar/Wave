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

	public string currentKing = "0";

	void Awake(){
		if (SM == null) {
			SM = this;
			DontDestroyOnLoad (this);
		} else {
			Destroy (this);
		}

	}

	public void UpdateKing(){
		print ("1 " + p1Score);
		print ("2 " + p2Score);
		print ("3 " + p3Score);
		print ("4 " + p4Score);
		print ("5 " + p5Score);
		print ("6 " + p6Score);

		if (p1Score > p2Score && p1Score > p3Score && p1Score > p4Score && p1Score > p5Score && p1Score > p6Score) {
			currentKing = "1";
		} else if (p2Score > p1Score && p2Score > p3Score && p2Score > p4Score && p2Score > p5Score && p2Score > p6Score) {
			currentKing = "2";
		} else if (p3Score > p1Score && p3Score > p2Score && p3Score > p4Score && p3Score > p5Score && p3Score > p6Score) {
			currentKing = "3";
		} else if (p4Score > p1Score && p4Score > p2Score && p4Score > p3Score && p4Score > p5Score && p4Score > p6Score) {
			currentKing = "4";
		} else if (p5Score > p1Score && p5Score > p2Score && p5Score > p3Score && p5Score > p4Score && p5Score > p6Score) {
			currentKing = "5";
		} else if (p6Score > p1Score && p6Score > p2Score && p6Score > p3Score && p6Score > p4Score && p6Score > p5Score) {
			currentKing = "5";
		} else {
			currentKing = "0";
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
				p5Score++;
				break;
			case "6":
				p6Score++;
				break;
			}
		}
	}
}
