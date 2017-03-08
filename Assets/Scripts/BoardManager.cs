using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class BoardManager : MonoBehaviour {

	public Button[] buttons;

	public static BoardManager instance;

	private Dictionary<string, List<int>> gameProgress; // TODO: Remove this?
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
	private static List<int> win8 = new List<int>(new List<int> { 3, 2, 7 });

	void Awake()
	{
		// I don't think I need a singletone here

		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}

		DontDestroyOnLoad(gameObject); //somehow this isn't necessary?

		//gameProgress = new Dictionary<string, List<int>>();
		//gameProgress.Add("x", xPositions);
		//gameProgress.Add("o", oPositions);
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
			updateList(xPositions, position);
		}
		else if (markerType == "O")
		{
			updateList(oPositions, position);
		}
		else
		{
			Debug.Log("IncorrectMarkerTypeSent");
		}

	}


	private void updateList(List<int> positionList, int position)
	{
		positionList.Add(position);
		positionList.Sort();    // ensure we're putting them in the right order.  Matters for pattern matching of winning tile arrangements	
	}

	public void CheckIfGameComplete()
	{

		bool xWins = WinningSequenceFound(xPositions);
		bool oWins = WinningSequenceFound(oPositions);

		if (xWins)
		{
			GameManager.instance.GameOver("X");
		}
		else if (oWins)
		{
			GameManager.instance.GameOver("O");
		}
		else if (GameEndsInDraw())
		{
			GameManager.instance.GameOver("No one");
		}

	}

	private bool GameEndsInDraw()
	{
		// FIXME: refactor so that it can return correct value, no matter when it's called

		bool draw = false;

		if (xPositions.Count > 4)
		{
			draw = true;

		}
		return draw;
	}

	private bool WinningSequenceFound(List<int> positions)
	{
		bool winFound = false;
		if (positions.Count > 2)
		// Need at least 3 elements in positions list in order to get a win.
		{
			for (int i = 0; i < winningPatterns.Length; i++)
			{
				List<int> winPattern = winningPatterns[i] as List<int>;

				if (positions.SequenceEqual(winPattern))
				{
					winFound = true;
				}

			}
		}

		return winFound;
	}


}
