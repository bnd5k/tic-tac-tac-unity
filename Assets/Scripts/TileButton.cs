using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TileButton : MonoBehaviour {

	public int position;
	public Text tileValue;

	public void handleClickEvent() {
		tileValue.text = "X";

		GameManager.instance.SaveProgress(GameManager.instance.xPositions, position);

		// TODO: add some sort of protection against user trying to mvoe while computer is moving.

		// Delay this call by 1 second so it seems like the machine is thinking
		GameManager.instance.Invoke("moveOpponent", .5f);

		// change bkground color
		// update progress in game controller
		// call move opponent in game contorller
	}
}
