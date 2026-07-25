using UnityEngine;

public class MoveBall : MonoBehaviour
{
  private Vector2 moveDirection; // always normalized
  private float speed;
  public float STARTING_VELOCITY = 2.0f;
  private readonly float MAX_VELOCITY = 20.0f;
  public float ACCEL_FACTOR = 1.5f;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    speed = STARTING_VELOCITY;
  }

  void Update()
  {
    // move in current direction
    transform.Translate(speed * Time.deltaTime * moveDirection);

    if (speed <= MAX_VELOCITY) // increase speed if not yet at max velocity
    {
      speed += ACCEL_FACTOR * Time.deltaTime;
    }
    else // make direction more down
    {
      moveDirection.y -= 0.1f;
      moveDirection.Normalize();
    }
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("bottom")) // contacted bottom wall, stop moving
    {
      gameObject.SetActive(false);
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    if (collision.gameObject.CompareTag("vertical barrier")) // hit something vertical, flip x-direction
    {
      moveDirection.x *= -1;
    }
    else if (collision.gameObject.CompareTag("horizontal barrier")) // hit something horizontal, flip y-direction
    {
      moveDirection.y *= -1;
    }
  }

  public void SetDirection(Vector2 newDirection)
  {
    moveDirection = newDirection;
  }
}
