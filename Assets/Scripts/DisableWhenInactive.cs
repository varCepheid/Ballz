using UnityEngine;

public class DisableWhenInactive : MonoBehaviour
{
  public GameManager gameManager;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
  }

  // Update is called once per frame
  void Update()
  {
    if (gameManager.GamePhaseMatches("inactive"))
    {
      gameObject.SetActive(false);
    }
  }
}
