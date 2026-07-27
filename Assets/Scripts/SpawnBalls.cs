using System.Collections.Generic;
using UnityEngine;

public class SpawnBalls : MonoBehaviour
{
  public GameObject ballPrefab;
  private GameObject thisBall;
  public List<GameObject> ballsCreated;
  public GameManager gameManager;
  public GameObject rtsBall;

  public Vector2 directionToLaunch;
  private ManageInputs inputs;

  public int ballsLaunched;

  private readonly float TIME_BETWEEN_BALLS = 0.2f; // seconds

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    inputs = GameObject.Find("Player Input").GetComponent<ManageInputs>();

    directionToLaunch = new();
    ballsCreated = new();
  }

  // Update is called once per frame
  void Update()
  {
    // stop spawning when there are enough balls
    if (gameManager.GamePhaseMatches("running") && (ballsLaunched >= gameManager.numberOfBalls))
    {
      CancelInvoke(nameof(LaunchBall));
    }
  }

  private void LaunchBall() // create one ball and add it to the list
  {
    ballsLaunched++;
    thisBall = Instantiate(ballPrefab, rtsBall.transform.position, ballPrefab.transform.rotation);
    thisBall.GetComponent<MoveBall>().SetDirection(directionToLaunch.normalized);
    ballsCreated.Add(thisBall);
  }

  public void StartSpawningBalls() // called by Game Manager when move to running phase
  {
    // send balls in direction of mouse
    directionToLaunch = inputs.mouseChange;

    // reset counter and list to prepare for new balls
    ballsLaunched = 0;
    ballsCreated.Clear();

    // start launching balls
    InvokeRepeating(nameof(LaunchBall), 0, TIME_BETWEEN_BALLS);
  }
}
