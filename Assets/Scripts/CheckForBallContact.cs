using UnityEngine;

public class CheckForBallContact : MonoBehaviour
{
  public UpdateNumberOnBlock updater;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    updater = GetComponentInParent<UpdateNumberOnBlock>();
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    updater.BallCollision();
  }
}
