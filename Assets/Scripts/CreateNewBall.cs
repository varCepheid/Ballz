using UnityEngine;

public class CreateNewBall : MonoBehaviour
{
  public GameObject greenBallPrefab;

  void OnTriggerEnter2D(Collider2D collision)
  {
    Instantiate(greenBallPrefab, transform.position, greenBallPrefab.transform.rotation);
    gameObject.SetActive(false);
  }
}
