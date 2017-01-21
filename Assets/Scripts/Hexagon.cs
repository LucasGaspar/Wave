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
		hash.Add("y", this.transform.position.y+0.2f);
		hash.Add("time", 0.05f);
		hash.Add("easetype", "easeOutSine");
		hash.Add("oncomplete", "Fall");

		iTween.MoveTo(gameObject, hash);
	}

	void Fall()
	{
		Hashtable hash =  new Hashtable();
		hash.Add("y", initialYPos);
		hash.Add("time", 0.2f);
		hash.Add("easetype", "easeInSine");

		iTween.MoveTo(gameObject, hash);
	}

	IEnumerator WaveCoroutine()
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

	public void Wave()
	{
		StartCoroutine(WaveCoroutine());
	}

	void OnMouseUpAsButton()
	{
		Wave();
	}
}
