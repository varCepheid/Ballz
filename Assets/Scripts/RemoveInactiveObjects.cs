using System.Collections.Generic;
using UnityEngine;

public class RemoveInactiveObjects : MonoBehaviour
{
  public GameManager gameManager;
  public GameObject spawnManager, rtsBall;
  public List<GameObject> balls;
  public List<GameObject> blocks;
  public List<GameObject> tokens;

  private bool firstBall;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    spawnManager = GameObject.Find("Spawn Manager");
  }

  void Update()
  {
    balls = spawnManager.GetComponent<SpawnBalls>().ballsCreated;
    blocks = spawnManager.GetComponent<SpawnBlocksAndTokens>().spawnedBlocks;
    tokens = spawnManager.GetComponent<SpawnBlocksAndTokens>().spawnedTokens;

    if (!gameManager.GamePhaseMatches("running") && !gameManager.GamePhaseMatches("inactive")) // only check during phase where balls are moving
    {
      firstBall = true;
      return;
    }

    for (int i = 0; i < blocks.Count; i++) // check for inactive blocks and remove them
    {
      GameObject block = blocks[i];
      if (!block.activeSelf) // blocks become inactive when they get to 0
      {
        blocks.Remove(block);
        Destroy(block);
      }
    }

    for (int i = 0; i < tokens.Count; i++) // check for inactive tokens and remove them
    {
      GameObject token = tokens[i];
      if (!token.activeSelf) // tokens become inactive when they get hit by a ball
      {
        tokens.Remove(token);
        Destroy(token);
      }
    }

    for (int i = 0; i < balls.Count; i++) // destroy balls that have hit the bottom
    {
      GameObject ball = balls[i];
      if (!ball.activeInHierarchy) // balls become inactive when they hit the bottom
      {
        if (firstBall) // record position of the first ball
        {
          firstBall = false;

          // put RTS ball in first ball's position
          rtsBall.transform.position = new Vector2(ball.transform.position.x, -4.9f);
          rtsBall.SetActive(true);
        }

        balls.Remove(ball);
        Destroy(ball);

        if (balls.Count == 0) // when last ball is destroyed, move to next phase
        {
          gameManager.SetPhase("preparing");
        }
      }
    }
  }
}

