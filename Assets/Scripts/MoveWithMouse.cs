using UnityEngine;

public class MoveWithMouse : MonoBehaviour
{
  private ManageInputs inputs;
  private GameManager gameManager;
  private GameObject ball;
  private Vector2 towardsMouse;

  public float distanceFromBall = 1.0f;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    inputs = GameObject.Find("Player Input").GetComponent<ManageInputs>();
    ball = GameObject.Find("Ready-To-Shoot Ball");
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
  }

  // Update is called once per frame
  void Update()
  {
    // start at ball's position
    transform.position = (Vector2)ball.transform.position;

    // while mouse is down, move away from ball towards mouse
    if (gameManager.GamePhaseMatches("holding"))
    {
      towardsMouse = inputs.mousePosition - (Vector2)ball.transform.position;
      towardsMouse.Normalize();
      transform.Translate(towardsMouse * distanceFromBall);
    }
  }
}
