using UnityEngine;

public class GameManager : MonoBehaviour
{
  public int numberOfBalls;
  public bool gameActive;
  public string gamePhase; // ready -> holding -> running

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    numberOfBalls = 1;
    gameActive = true;
    gamePhase = "ready";
  }

  // Update is called once per frame
  void Update()
  {

  }
}
