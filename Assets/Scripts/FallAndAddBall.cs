using UnityEngine;

public class FallAndAddBall : MonoBehaviour
{
  public GameManager gameManager;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    gameManager.numberOfBalls++;
  }

  // Update is called once per frame
  void Update()
  {
    if (gameManager.GamePhaseMatches("preparing")) // once all balls are done moving
    {
      Destroy(this);
    }
  }
}
