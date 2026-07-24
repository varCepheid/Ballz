using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveBall : MonoBehaviour
{
  public Vector2 launchDirection;
  private Rigidbody2D body;
  private Vector2 curVelocity;

  public float speed = 2.0f;
  private readonly float MAX_VELOCITY = 20.0f;
  private readonly float ACCEL_FACTOR = 1.5f;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    body = GetComponent<Rigidbody2D>();
    body.AddForce(speed * launchDirection, ForceMode2D.Impulse); // start moving in direction of mouse at starting speed
  }

  void FixedUpdate()
  {
    curVelocity = body.GetPointVelocity(body.worldCenterOfMass);
    if (curVelocity.magnitude < MAX_VELOCITY) // accelerate in current direction until velocity is 20
    {
      body.AddForce(ACCEL_FACTOR * Time.fixedDeltaTime * curVelocity.normalized, ForceMode2D.Impulse);
    }
    else // once velocity reaches 20, accelerate downwards to end round
    {
      body.AddForce(ACCEL_FACTOR * Time.fixedDeltaTime * Vector2.down, ForceMode2D.Impulse);
    }
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.gameObject.CompareTag("bottom")) // contacted bottom wall, stop moving
    {
      gameObject.SetActive(false);
    }
    else if (collision.gameObject.CompareTag("token")) // contacted new ball token
    {

    }
  }
}
