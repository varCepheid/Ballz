using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnBalls : MonoBehaviour
{
  public GameObject ballPrefab;
  private GameObject thisBall;
  public List<GameObject> ballsCreated;
  public GameManager gameManager;
  private GameObject rtsBall;

  public Vector2 directionToLaunch, mousePosition;
  private InputAction clickAction;
  private ManageInputs inputs;

  public int ballsLaunched;
  private bool firstBall = true;

  private readonly float TIME_BETWEEN_BALLS = 0.2f; // seconds

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    rtsBall = GameObject.Find("Ready-To-Shoot Ball");
    inputs = GameObject.Find("Player Input").GetComponent<ManageInputs>();
    clickAction = inputs.clickAction;

    directionToLaunch = new();
    ballsCreated = new();
  }

  // Update is called once per frame
  void Update()
  {
    mousePosition = inputs.mousePosition;

    if (gameManager.GamePhaseMatches("ready")) // phase where player can click and drag
    {
      if (clickAction.WasPressedThisFrame()) // when player clicks, move to next phase
      {
        gameManager.SetPhase("holding");
      }
    }

    else if (gameManager.GamePhaseMatches("holding")) // phase where player is aiming
    {
      if (clickAction.WasReleasedThisFrame()) // when player releases, move to next phase and start spawning balls
      {
        gameManager.SetPhase("running");
        ballsLaunched = 0;
        ballsCreated.Clear();
        InvokeRepeating(nameof(LaunchBall), 0, TIME_BETWEEN_BALLS);
        firstBall = true;
      }
    }

    else if (gameManager.GamePhaseMatches("running"))
    {
      if (ballsLaunched >= gameManager.numberOfBalls) // stop spawning when there are enough balls
      {
        CancelInvoke(nameof(LaunchBall));
      }

      if (ballsCreated.Count == 0) // when all balls are destroyed, move to next phase
      {
        gameManager.SetPhase("ready"); // NEED TO switch to "preparing"
      }

      for (int i = 0; i < ballsCreated.Count; i++) // destroy balls that have hit the bottom
      {
        GameObject ball = ballsCreated[i];
        if (!ball.activeInHierarchy) // balls become inactive when they hit the bottom
        {
          if (firstBall) // record position of the first ball
          {
            rtsBall.transform.position = new Vector2(ball.transform.position.x, -0.8f);
            firstBall = false;
          }

          ballsCreated.Remove(ball);
          Destroy(ball);
        }
      }

    }
  }

  private void LaunchBall()
  {
    ballsLaunched++;
    thisBall = Instantiate(ballPrefab, rtsBall.transform.position, ballPrefab.transform.rotation);
    thisBall.GetComponent<MoveBall>().launchDirection = directionToLaunch.normalized;
    ballsCreated.Add(thisBall);
  }
}
