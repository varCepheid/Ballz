using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class UpdateLevelNumber : MonoBehaviour
{
  public GameManager gameManager;
  public TextMeshProUGUI text;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    text = GetComponent<TextMeshProUGUI>();
  }

  // Update is called once per frame
  void Update()
  {
    text.text = gameManager.levelNumber.ToString();
  }
}
