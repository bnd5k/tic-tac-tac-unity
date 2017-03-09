using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BoardManager : MonoBehaviour {

	public const string xMarker = "X";
	public const string oMarker = "O";

	public static BoardManager instance;
	
	public Button[] buttons;

	private List<int> xPositions = new List<int>();
	private List<int> oPositions = new List<int>();
	private List<int> allTilePositions = new List<int>(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });
	private int[,] winningPatterns = new int[8, 3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 1, 4, 7 }, { 2, 5, 8 }, { 3, 6, 9 }, { 1, 5, 9 }, { 3, 5, 7 } } ;

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

		DontDestroyOnLoad(gameObject);
	}

	public void MoveOpponent()
	{
		int selectedTilePosition = SelectFreeTile();
		MarkTileAsOccupied(selectedTilePosition);
		SaveProgress(oMarker, selectedTilePosition);
	}

	public void SaveProgress(string markerType, int position)
	{
		if (markerType == xMarker)
		{
			xPositions.Add(position);
		}
		else if (markerType == oMarker)
		{
			oPositions.Add(position);
		}
		else
		{
			Debug.Log("IncorrectMarkerTypeSent");
		}

		CheckIfGameComplete();
	}

	public void clearBoard() {
		xPositions.Clear();
		oPositions.Clear();
	}

	private void CheckIfGameComplete()
	{

		bool xWins = didFindWinningPattern(xPositions);
		bool oWins = didFindWinningPattern(oPositions);

		if (xWins)
		{
			GameManager.instance.GameOver(xMarker);
		}
		else if (oWins)
		{
			GameManager.instance.GameOver(oMarker);
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
			for (int i = 0; i < winningPatterns.GetLength(0); i++)
			{
				if (isWin(positions, i))
				{
					winFound = true;
				}
			}
		}

		return winFound;
	}

	private bool isWin(List<int> positions, int winPatternIndex) 
	{
		return positions.Contains(winningPatterns[winPatternIndex, 0]) && positions.Contains(winningPatterns[winPatternIndex, 1]) && positions.Contains(winningPatterns[winPatternIndex, 2]);
	}


	private int SelectFreeTile()
	{
		// find the free tiles and randomly select 1

		List<int> occupiedTiles = (oPositions.Count > 0) ? xPositions.Concat(oPositions).ToList() : xPositions;
		List<int> availableTiles = allTilePositions.Except(occupiedTiles).ToList();
		// List<int> availableTiles = new List<int>(new List<int> { 7, 8, 9 }); // for speedier gameplay ;)

		int randomItemIndex = Random.Range(0, availableTiles.Count);

		return availableTiles[randomItemIndex];
	}

	private void MarkTileAsOccupied(int selectedTilePosition)
	{
		string buttonName = $"Button{selectedTilePosition}"; // FIXME.  This is fragile.

		TileButton selectedButton = GameObject.Find(buttonName).GetComponent<TileButton>();

		selectedButton.SetMarkerValue(oMarker);
		selectedButton.DisableButton();
	}

}
