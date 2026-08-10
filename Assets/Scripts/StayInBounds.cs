using UnityEngine;

// Keeps RTS ball between left and right walls.
public class StayInBounds : MonoBehaviour
{
  // Update is called once per frame
  void Update()
  {
    if (transform.position.x > 3)
    {
      transform.position = new(3f, transform.position.y);
    }
    else if (transform.position.x < -4)
    {
      transform.position = new(-4f, transform.position.y);
    }
  }
}
