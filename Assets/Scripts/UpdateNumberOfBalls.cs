using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class UpdateNumberOfBalls : MonoBehaviour
{
  public GameManager gameManager;
  public TextMeshPro text;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    text = GetComponent<TextMeshPro>();
  }

  // Update is called once per frame
  void Update()
  {
    text.text = "x" + gameManager.numberOfBalls.ToString();
  }
}
