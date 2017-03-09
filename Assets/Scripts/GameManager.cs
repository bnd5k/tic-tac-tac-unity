using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public static GameManager instance;
	[HideInInspector] public string winner;
	[HideInInspector] public bool gameOver = false;

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

		SceneManager.LoadScene("MainMenu");
	}

	public void PlayGame()
	{
		SceneManager.LoadScene("GameScreen");
	}

	public void RestartGame()
	{
		gameOver = false;
		BoardManager.instance.clearBoard();
		SceneManager.LoadScene("GameScreen");
	}

	public void GameOver(string winningMarker)
	{
		gameOver = true;
		winner = winningMarker;

		// TODO: slowly transition over to GameOverScreen
		SceneManager.LoadScene("GameOverScreen");
	}
}
