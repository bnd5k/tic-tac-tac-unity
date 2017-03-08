using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; //still needed?
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public static GameManager instance;
	public string winner;

	//private Dictionary<string, List<int>> gameProgress;
	//private List<int> xPositions = new List<int>();
	//private List<int> oPositions = new List<int>();
	//private List<int> allTilePositions = new List<int>(new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 });

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


	public void PlayGame()
	{
		SceneManager.LoadScene("GameScreen");
	}

	public void RestartGame()
	{
		SceneManager.LoadScene("GameScreen");
	}


	public void GameOver(string winningMarker)
	{
		winner = winningMarker;

		// TODO: slowly transition over to GameOverScreen
		SceneManager.LoadScene("GameOverScreen");
	}


}
