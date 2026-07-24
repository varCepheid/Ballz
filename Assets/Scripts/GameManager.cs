using UnityEngine;

public class GameManager : MonoBehaviour
{
  public int numberOfBalls;
  public int levelNumber;
  private string gamePhase; // ready -> holding -> running -> preparing; inactive

  private GameObject rtsBall;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    numberOfBalls = 3;
    gamePhase = "ready";
    levelNumber = 1;

    rtsBall = GameObject.Find("Ready-To-Shoot Ball");
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetPhase(string newPhase)
  {
    if (newPhase == "running")
    {
      gamePhase = "running";
      rtsBall.SetActive(false);
    }
    else if ((newPhase == "ready") || (newPhase == "preparing"))
    {
      rtsBall.SetActive(true);
      gamePhase = newPhase;
    }
    else if (newPhase == "holding")
    {
      gamePhase = "holding";
    }
  }

  public bool GamePhaseMatches(string other)
  {
    return gamePhase == other;
  }
}
