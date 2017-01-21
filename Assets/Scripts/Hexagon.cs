using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hexagon : MonoBehaviour
{

	public Coordinate coordinates;
	public TextMesh textmesh;

	float initialYPos;
	//float jumpForce = 5;

	void Start()
	{
		initialYPos = this.transform.position.y;
	}

	public void SetCoordinates(int x, int y)
	{
		coordinates.x = x;
		coordinates.y = y;
		textmesh.text = x+", "+y;
	}

	public void Jump()
	{
		Hashtable hash =  new Hashtable();
		hash.Add("y", 0.2f);
		hash.Add("time", 0.05f);
		hash.Add("easetype", "easeOutSine");

		iTween.MoveAdd(gameObject, hash);

		hash =  new Hashtable();
		hash.Add("y", initialYPos);
		hash.Add("time", 0.2f);
		hash.Add("delay", 0.06f);
		hash.Add("easetype", "easeInSine");

		iTween.MoveTo(gameObject, hash);
	}

	IEnumerator Waveee()
	{
		int i = 0;
		List<Hexagon> radiusList;
		do
		{
			radiusList = Map.GetListOfHexagon(this, i);
			foreach(Hexagon hexagon in radiusList)
			{
				hexagon.Jump();
			}

			yield return new WaitForSeconds(0.05f);
			i++;
		}while(radiusList.Count > 0);
	}

	void OnMouseUpAsButton()
	{
		StartCoroutine(Waveee());
	}
}
