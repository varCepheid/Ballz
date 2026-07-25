using System.Collections.Generic;
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
    if (gameManager.GamePhaseMatches("running")) // phase where balls are moving
    {
      if (ballsLaunched >= gameManager.numberOfBalls) // stop spawning when there are enough balls
      {
        CancelInvoke(nameof(LaunchBall));
      }

      for (int i = 0; i < ballsCreated.Count; i++) // destroy balls that have hit the bottom
      {
        GameObject ball = ballsCreated[i];
        if (!ball.activeInHierarchy) // balls become inactive when they hit the bottom
        {
          if (firstBall) // record position of the first ball
          {
            rtsBall.transform.position = new Vector2(ball.transform.position.x, -4.8f);
            firstBall = false;
          }

          ballsCreated.Remove(ball);
          Destroy(ball);

          if (ballsCreated.Count == 0) // when last ball is destroyed, move to next phase
          {
            gameManager.SetPhase("preparing");
          }
        }
      }
    }
  }

  private void LaunchBall() // create one ball and add it to the list
  {
    ballsLaunched++;
    thisBall = Instantiate(ballPrefab, rtsBall.transform.position, ballPrefab.transform.rotation);
    thisBall.GetComponent<MoveBall>().moveDirection = directionToLaunch.normalized;
    ballsCreated.Add(thisBall);
  }

  public void StartSpawningBalls() // called by Game Manager when move to running phase
  {
    // send balls in direction of mouse
    mousePosition = inputs.mousePosition;
    directionToLaunch = mousePosition - (Vector2)rtsBall.transform.position;

    // reset counter and list to prepare for new balls
    ballsLaunched = 0;
    ballsCreated.Clear();
    firstBall = true;

    // start launching balls
    InvokeRepeating(nameof(LaunchBall), 0, TIME_BETWEEN_BALLS);
  }
}
