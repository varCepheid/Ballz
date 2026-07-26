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
    transform.Translate(0.0f, -1.0f, 0.0f);
    if ((transform.position.y <= -3.0f) && gameObject.CompareTag("block"))
    {
      StartCoroutine(gameManager.SetInactive());
    }
  }
}
