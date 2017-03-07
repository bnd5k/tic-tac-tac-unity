using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverBoard : MonoBehaviour
{

	public Text winnerText;

	void Start()
	{
		winnerText.text = "Winner: " + GameManager.instance.winner;
	}
}
