using UnityEngine;

public class GameManager : MonoBehaviour
{
  public int numberOfBalls;
  public int levelNumber;
  private string gamePhase; // ready -> holding -> running -> preparing; inactive

  private GameObject rtsBall;
  private SpawnBalls spawnBalls;
  private SpawnBlocksAndTokens spawnBTs;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    rtsBall = GameObject.Find("Ready-To-Shoot Ball");
    spawnBalls = GameObject.Find("Spawn Manager").GetComponent<SpawnBalls>();
    spawnBTs = GameObject.Find("Spawn Manager").GetComponent<SpawnBlocksAndTokens>();

    rtsBall.transform.position = new(-0.5f, -4.8f);

    numberOfBalls = 1;
    levelNumber = 0;
    SetPhase("preparing");
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
      rtsBall.SetActive(false);
      spawnBalls.StartSpawningBalls();
    }
    else if (newPhase.Equals("preparing"))
    {
      gamePhase = "preparing";
      rtsBall.SetActive(true);
      spawnBTs.PrepareNextLevel();
    }
  }

  public bool GamePhaseMatches(string other)
  {
    return gamePhase == other;
  }

  public void GameOver()
  {
    Debug.Log("Game Over");
  }
}
