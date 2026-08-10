using UnityEngine;

public class MoveDownEachLevel : MonoBehaviour
{
  public GameManager gameManager;

  void Start()
  {
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
  }

  public void MoveDown()
  {
    if (gameManager.GamePhaseMatches("inactive")) return;
    transform.Translate(0.0f, -1.0f, 0.0f);
    if (transform.position.y <= -4.0f)
    {
      if (gameObject.CompareTag("block"))
      {
        StartCoroutine(gameManager.SetInactive());
      }
      else if (gameObject.CompareTag("token"))
      {
        gameObject.SetActive(false);
      }
    }
  }
}
