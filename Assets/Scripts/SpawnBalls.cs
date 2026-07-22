using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnBalls : MonoBehaviour
{
  public GameObject ballPrefab;
  private GameObject thisBall;
  public GameManager gameManager;

  public Vector3 stoppedBallPosition;
  public Vector3 directionToLaunch;
  public Vector2 mousePosition;

  private int ballsLaunched;
  private Inputs inputs;
  private InputAction clickAction;
  private InputAction pointer;

  private readonly float TIME_BETWEEN_BALLS = 0.1f; // seconds

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    stoppedBallPosition = ballPrefab.transform.position;
    directionToLaunch = Vector3.up;

    inputs = new();
    inputs.Player.Disable();
    inputs.UI.Enable();
    clickAction = inputs.UI.Click;
    pointer = inputs.UI.Point;
  }

  // Update is called once per frame
  void Update()
  {
    mousePosition = pointer.ReadValue<Vector2>();

    Debug.Log(gameManager.gamePhase + " "
        + gameManager.gamePhase.Equals("ready").ToString() + " "
        + clickAction.WasPressedThisFrame().ToString() + " "
        + clickAction.WasReleasedThisFrame().ToString());
    if (gameManager.gamePhase.Equals("ready") && clickAction.WasPressedThisFrame())
    {
      gameManager.gamePhase = "holding";
    }
    else if (gameManager.gamePhase.Equals("holding") && clickAction.WasReleasedThisFrame())
    {
      gameManager.gamePhase = "running";
      ballsLaunched = 0;
      InvokeRepeating(nameof(LaunchBall), 0, TIME_BETWEEN_BALLS);
    }
  }

  private void LaunchBall()
  {
    if (ballsLaunched < gameManager.numberOfBalls)
    {
      thisBall = Instantiate(ballPrefab, stoppedBallPosition, ballPrefab.transform.rotation);
      thisBall.GetComponent<Rigidbody>().AddForce(10 * directionToLaunch, ForceMode.Force);
    }
  }
}
