using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class UpdateNumberOnBlock : MonoBehaviour
{
  public TextMeshPro text;
  public int number;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    text = GetComponent<TextMeshPro>();
    // Debug.Log("Spawned block at " + (transform.position.x + 3.5f).ToString() + " with number " + number.ToString());
  }

  // Update is called once per frame
  void Update()
  {
    text.text = number.ToString();
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    Debug.Log("Collision2D between block at " + (transform.position.x + 3.5f).ToString() + " and " + collision.gameObject.name);

    number--;
    if (number <= 0)
    {
      gameObject.SetActive(false);
    }
  }

  void OnCollisionEnter(Collision collision)
  {
    Debug.Log("Collision between block at " + (transform.position.x + 3.5f).ToString() + " and " + collision.gameObject.name);
  }
}
