using System.Collections;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  private static WaitForSeconds _waitForHalfASecond = new(0.5f);

  public int numberOfBalls;
  public int levelNumber;
  private string gamePhase; // ready -> holding -> running -> preparing; inactive

  private GameObject rtsBall;
  public GameObject gameElements;
  public GameObject titleText;
  public GameObject startButton;
  public GameObject gameOverText;
  public GameObject scoreText;

  private SpawnBalls spawnBalls;
  private SpawnBlocksAndTokens spawnBTs;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    spawnBalls = GameObject.Find("Spawn Manager").GetComponent<SpawnBalls>();
    spawnBTs = GameObject.Find("Spawn Manager").GetComponent<SpawnBlocksAndTokens>();
    gamePhase = "inactive";
  }

  public void SetPhase(string newPhase)
  {
    if (newPhase.Equals("ready"))
    {
      gamePhase = "ready";
    }
    else if (newPhase.Equals("holding"))
    {
      gamePhase = "holding";
    }
    else if (newPhase.Equals("running"))
    {
      gamePhase = "running";

      // disable RTS ball and all its parts
      rtsBall.SetActive(false);
      foreach (Transform child in rtsBall.GetComponentsInChildren<Transform>())
      {
        child.gameObject.SetActive(false);
      }

      spawnBalls.StartSpawningBalls();
    }
    else if (newPhase.Equals("preparing"))
    {
      gamePhase = "preparing";

      // enable all parts of RTS ball
      rtsBall.GetComponent<EnableChildren>().EnableThem();

      // spawn blocks and tokens for next level
      spawnBTs.PrepareNextLevel();
    }
    if (newPhase.Equals("inactive"))
    {
      gamePhase = "inactive";
      GameOver();
    }
  }

  public bool GamePhaseMatches(string other)
  {
    return gamePhase == other;
  }

  public void GameOver() // called half a second after block reaches last row
  {
    // disable game elements and enable others
    gameElements.SetActive(false);
    scoreText.SetActive(false);
    gameOverText.SetActive(true);
    startButton.SetActive(true);
    startButton.GetComponentInChildren<TextMeshProUGUI>().text = "Play Again";
  }

  public IEnumerator SetInactive() // called by blocks when they reach last row, ends game after half a second
  {
    yield return _waitForHalfASecond;
    SetPhase("inactive");
  }

  // called when "play" button pressed to activate game
  public void StartGame()
  {
    // enable and disable appropriate objects
    gameElements.SetActive(true);
    scoreText.SetActive(true);
    titleText.SetActive(false);
    gameOverText.SetActive(false);
    startButton.SetActive(false);

    // set up ready-to-shoot ball
    rtsBall = GameObject.Find("Ready-To-Shoot Ball");
    rtsBall.transform.position = new(-0.5f, -4.9f);

    // other starting actions
    numberOfBalls = 1;
    levelNumber = 0;
    SetPhase("preparing");
  }
}
