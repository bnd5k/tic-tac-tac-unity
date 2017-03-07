using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileButton : MonoBehaviour {

	public Button button; // FIXME: Odd that I need to attach this component to the button in Unity, then associate the button with this property
	public int position;
	public Text tileValue;

	public void HandleClickEvent() {
		string markerType = "X";
		tileValue.text = markerType;

		GameManager.instance.SaveProgress(markerType, position);
		DisableButton();

		GameManager.instance.CheckIfGameComplete();

		// TODO: add some sort of protection against user trying to mvoe while computer is moving.


		// Delay this call by 1 second so it seems like the machine is thinking		 
		GameManager.instance.Invoke("MoveOpponent", .5f);
	}

	public void DisableButton() {
		button.interactable = false;		
	}
}
