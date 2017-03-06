using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq; //still needed?

public class GameManager : MonoBehaviour
{

	public static GameManager instance;

	public Button[] buttons;

	private List<int> foo;

	private Dictionary<string, List<int>> gameProgress;
	public List<int> xPositions = new List<int>();
	private List<int> oPositions = new List<int>();
	private List<int> allTilePositions = new List<int>(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
	private List<int> winningPatterns = new List<int>(new List<int>{ 1, 2, 3 });

	void Awake() {

		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject); //somehow this isn't necessary?


		gameProgress = new Dictionary<string, List<int>> ();
		gameProgress.Add("x", xPositions);
		gameProgress.Add("o", oPositions);
	}

	public void SaveProgress(List<int> positionList, int position)
	{
		positionList.Add(position);
	}

	public void MoveOpponent() {

		// find the free tiles and randomly select 1
		List<int> occupiedTiles = (oPositions.Count > 0) ? xPositions.Concat(oPositions).ToList() : xPositions;
		List<int> availableTiles = allTilePositions.Except(occupiedTiles).ToList();
		var randomItemIndex = Random.Range(0, availableTiles.Count - 1);
		var selectedTilePosition = availableTiles[randomItemIndex];

		Debug.Log($"selectedTilePosition: {selectedTilePosition}");


		//find tile and change text value to 0
		var buttonName = $"Button{selectedTilePosition}"; // FIXME.  This is fragile.
		var selectedButton = GameObject.Find(buttonName).GetComponent<TileButton>();
		selectedButton.tileValue.text = "O";


		selectedButton.DisableButton();
		              
		// then save answer
		GameManager.instance.SaveProgress(oPositions, selectedTilePosition);
	}

	private string PrettyPrint(List<int> myList)
	{
		if (myList.Count > 0)
		{
			string temp = "";
			foreach (int item in myList)
			{
				temp += item.ToString() + ",";
			}
			return temp;
		}
		else {
			return "EmptyList";
		}
	}

}
