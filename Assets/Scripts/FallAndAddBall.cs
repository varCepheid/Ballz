using UnityEngine;

public class FallAndAddBall : MonoBehaviour
{
  public GameManager gameManager;
  public float speed = 3.0f;

  void OnEnable()
  {
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    gameManager.numberOfBalls++;
  }

  // Update is called once per frame
  void Update()
  {
    if (!gameManager.GamePhaseMatches("running")) // once all balls are done moving
    {
      Destroy(gameObject);
    }
    else
    {
      if (transform.position.y <= -4.9f)
      {
        transform.position = new(transform.position.x, -4.9f);
      }
      else
      {
        transform.Translate(speed * Time.deltaTime * Vector2.down);
        speed += 3.0f * Time.deltaTime;
      }
    }
  }
}
