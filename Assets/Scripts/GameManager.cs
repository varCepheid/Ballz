using UnityEngine;

public class GameManager : MonoBehaviour
{
  public int numberOfBalls;
  public int levelNumber;
  private string gamePhase; // ready -> holding -> running -> preparing; inactive

  private GameObject rtsBall;
  private SpawnBalls spawnBalls;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    numberOfBalls = 3;
    gamePhase = "ready";
    levelNumber = 1;

    rtsBall = GameObject.Find("Ready-To-Shoot Ball");
    spawnBalls = GameObject.Find("Spawn Manager").GetComponent<SpawnBalls>();
  }

  // Update is called once per frame
  void Update()
  {
  }

  public void SetPhase(string newPhase)
  {
    Debug.Log(newPhase);

    if (newPhase.Equals("ready"))
    {
      rtsBall.SetActive(true);
      gamePhase = "ready";
    }
    else if (newPhase.Equals("holding"))
    {
      gamePhase = "holding";
    }
    else if (newPhase.Equals("running"))
    {
      gamePhase = "running";
      rtsBall.SetActive(false);
      spawnBalls.StartSpawningBalls();
    }
    else if (newPhase.Equals("preparing"))
    {
      SetPhase("ready");
    }
  }

  public bool GamePhaseMatches(string other)
  {
    return gamePhase.Equals(other);
  }
}
