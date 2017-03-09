using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileButton : MonoBehaviour
{

	public Button button; // FIXME: Odd that I need to attach this component to the button in Unity, then associate the button with this property
	public int position;
	public Text tileValue;

	public void HandleClickEvent()
	{
		SetMarkerValue(BoardManager.xMarker);
		DisableButton();

		BoardManager.instance.SaveProgress(BoardManager.xMarker, position);

		if (!GameManager.instance.gameOver)
		{
			// Delay this call by 1 second so it seems like the machine is thinking		 
			BoardManager.instance.Invoke("MoveOpponent", .5f);
		}
	}

	public void SetMarkerValue(string markerValue) {
		tileValue.text = markerValue;
	}

	public void DisableButton()
	{
		button.interactable = false;
	}
}
