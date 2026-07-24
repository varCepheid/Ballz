using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody2D))]
public class MoveBall : MonoBehaviour
{
  public Vector2 launchDirection;
  private Rigidbody2D body;
  private Vector2 curVelocity;

  public float speed = 1.0f;
  public float accelFactor = 1.0f;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    body = GetComponent<Rigidbody2D>();
    body.AddForce(speed * launchDirection, ForceMode2D.Impulse);
  }

  // Update is called once per frame
  void Update()
  {
    curVelocity = body.GetPointVelocity(body.worldCenterOfMass);
    body.AddForce(accelFactor * Time.deltaTime * curVelocity.normalized, ForceMode2D.Impulse);
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    gameObject.SetActive(false);
  }
}
