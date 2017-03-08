using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	public static GameManager instance;
	public string winner;

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
