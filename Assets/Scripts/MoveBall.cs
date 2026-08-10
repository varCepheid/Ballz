using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveBall : MonoBehaviour
{
  private Rigidbody2D rb;
  public Vector2 moveDirection; // always normalized
  public float speed;
  public float STARTING_VELOCITY = 3.0f;
  public float MAX_VELOCITY = 20.0f;
  public float ACCEL_FACTOR = 2f;

  // private bool hitVerticalBarrierThisFrame, hitHorizontalBarrierThisFrame;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    speed = STARTING_VELOCITY;
    rb = GetComponent<Rigidbody2D>();
    rb.linearVelocity = moveDirection * speed;
    // hitHorizontalBarrierThisFrame = false;
    // hitVerticalBarrierThisFrame = false;
  }

  void FixedUpdate()
  {
    // hitHorizontalBarrierThisFrame = false;
    // hitVerticalBarrierThisFrame = false;

    // stop moving when below lower bound
    if (rb.position.y < -5)
    {
      gameObject.SetActive(false);
    }

    // get current velocity
    moveDirection = rb.linearVelocity.normalized;

    if (speed <= MAX_VELOCITY) // increase speed if not yet at max velocity
    {
      speed += ACCEL_FACTOR * Time.deltaTime;
    }
    else // make direction more down
    {
      moveDirection.y -= 0.1f * Time.deltaTime;
      moveDirection.Normalize();
    }

    // move in current direction
    rb.linearVelocity = moveDirection * speed;
  }

  // void OnTriggerEnter2D(Collider2D collision)
  // {
  //   if (collision.gameObject.CompareTag("bottom")) // contacted bottom wall, stop moving
  //   {
  //     gameObject.SetActive(false);
  //   }
  // }

  // void OnCollisionEnter2D(Collision2D collision)
  // {
  //   if (collision.gameObject.CompareTag("vertical barrier") && !hitVerticalBarrierThisFrame) // hit something vertical, flip x-direction
  //   {
  //     moveDirection.x *= -1;
  //     hitVerticalBarrierThisFrame = true; // do not register more than one barrier per frame
  //   }
  //   else if (collision.gameObject.CompareTag("horizontal barrier") && !hitHorizontalBarrierThisFrame) // hit something horizontal, flip y-direction
  //   {
  //     moveDirection.y *= -1;
  //     hitHorizontalBarrierThisFrame = true; // do not register more than one barrier per frame
  //   }
  // }
}
