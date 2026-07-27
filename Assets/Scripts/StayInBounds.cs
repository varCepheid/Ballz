using UnityEngine;

public class StayInBounds : MonoBehaviour
{
  private readonly float WIDTH = 0.15f;

  // Update is called once per frame
  void Update()
  {
    if (transform.position.x > 3 - WIDTH)
    {
      transform.position = new(3f - WIDTH, -4.9f);
    }
    else if (transform.position.x < -4 + WIDTH)
    {
      transform.position = new(-4f + WIDTH, -4.9f);
    }
  }
}
