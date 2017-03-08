using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

	public void HandleClickEvent() 
	{
		Debug.Log("hi");
		GameManager.instance.PlayGame();	
	}
}
