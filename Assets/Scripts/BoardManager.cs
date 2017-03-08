using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BoardManager : MonoBehaviour {

	public Button[] buttons;

	public static BoardManager instance;

	public List<int> xPositions = new List<int>();
	public List<int> oPositions = new List<int>();

	private List<int> allTilePositions = new List<int>(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

	private List<int>[] winningPatterns = new List<int>[] { win1, win2, win3, win4, win5, win6, win7, win8 };
	private static List<int> win1 = new List<int>(new List<int> { 1, 2, 3 });
	private static List<int> win2 = new List<int>(new List<int> { 4, 5, 6 });
	private static List<int> win3 = new List<int>(new List<int> { 7, 8, 9 });
	private static List<int> win4 = new List<int>(new List<int> { 1, 4, 7 });
	private static List<int> win5 = new List<int>(new List<int> { 2, 5, 8 });
	private static List<int> win6 = new List<int>(new List<int> { 3, 6, 9 });
	private static List<int> win7 = new List<int>(new List<int> { 1, 5, 9 });
	private static List<int> win8 = new List<int>(new List<int> { 3, 5, 7 });

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject); //somehow this isn't necessary?
	}


	public void MoveOpponent()
	{

		// find the free tiles and randomly select 1
		List<int> occupiedTiles = (oPositions.Count > 0) ? xPositions.Concat(oPositions).ToList() : xPositions;
		List<int> availableTiles = allTilePositions.Except(occupiedTiles).ToList();
		//List<int> availableTiles = new List<int>(new List<int> { 7, 8, 9 }); // for speedier gameplay ;)

		int randomItemIndex = Random.Range(0, availableTiles.Count - 1);

		int selectedTilePosition = availableTiles[randomItemIndex];

		//find tile and change text value to 0
		string buttonName = $"Button{selectedTilePosition}"; // FIXME.  This is fragile.
		TileButton selectedButton = GameObject.Find(buttonName).GetComponent<TileButton>();
		selectedButton.tileValue.text = "O";

		selectedButton.DisableButton();

		SaveProgress("O", selectedTilePosition);
		CheckIfGameComplete();
	}


	public void SaveProgress(string markerType, int position)
	{
		if (markerType == "X")
		{
			xPositions.Add(position);
		}
		else if (markerType == "O")
		{
			oPositions.Add(position);
		}
		else
		{
			Debug.Log("IncorrectMarkerTypeSent");
		}

	}

	public void CheckIfGameComplete()
	{

		bool xWins = didFindWinningPattern(xPositions);
		bool oWins = didFindWinningPattern(oPositions);

		if (xWins)
		{
			GameManager.instance.GameOver("X");
		}
		else if (oWins)
		{
			GameManager.instance.GameOver("O");
		}
		else if (!xWins && !oWins && xPositions.Count > 4)
		{
			GameManager.instance.GameOver("No one");
		}

	}

	private bool didFindWinningPattern(List<int> positions)
	{
		bool winFound = false;
		bool sufficientPositionsOccupiedForWin = positions.Count > 2;
		// ^^ Need at least 3 elements in positions list in order to get a win.
		if (sufficientPositionsOccupiedForWin) 
		{
			

			for (int i = 0; i < winningPatterns.Length; i++)
			{
				if (positions.Contains(winningPatterns[i][0]) && positions.Contains(winningPatterns[i][1]) && positions.Contains(winningPatterns[i][2]) ) 
				{
					winFound = true;
				}
			}
		}

		return winFound;
	}
}
