using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hexagon : MonoBehaviour
{
	public Coordinate coordinates;
	public TextMesh textmesh;

	float initialYPos;
	float elevation = 0.2f;
	float maxElevation = 0.5f;

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
		StartCoroutine(JumpCoroutine());
		/*Hashtable hash =  new Hashtable();
		float y = this.transform.position.y + elevation;
		hash.Add("y", ((y > (initialYPos+maxElevation)) ? (initialYPos + maxElevation) : y));
		hash.Add("time", 0.075f);
		hash.Add("easetype", "easeOutSine");
		hash.Add("oncomplete", "Fall");

		iTween.MoveTo(gameObject, hash);*/
	}
	IEnumerator JumpCoroutine()
	{
		this.transform.position = this.transform.position + new Vector3(0,elevation,0);
		yield return new WaitForSeconds(0.05f);
		this.transform.position = this.transform.position - new Vector3(0,elevation*0.25f,0);
		yield return new WaitForSeconds(0.03f);
		this.transform.position = this.transform.position - new Vector3(0,elevation*0.25f,0);
		yield return new WaitForSeconds(0.03f);
		this.transform.position = this.transform.position - new Vector3(0,elevation*0.25f,0);
		yield return new WaitForSeconds(0.03f);
		this.transform.position = this.transform.position - new Vector3(0,elevation*0.25f,0);
	}

	void Fall()
	{
		/*Hashtable hash =  new Hashtable();
		hash.Add("y", initialYPos);
		hash.Add("time", 0.1f);
		hash.Add("easetype", "easeInSine");

		iTween.MoveTo(gameObject, hash);*/
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
