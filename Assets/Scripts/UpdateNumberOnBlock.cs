using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshPro))]
public class UpdateNumberOnBlock : MonoBehaviour
{
  public TextMeshPro text;
  public int number;
  // private byte ableToContact; // 0 is available, 1 and 2 are not

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    text = GetComponent<TextMeshPro>();
    // ableToContact = 0;

    // Debug.Log("Spawned block at " + (transform.position.x + 3.5f).ToString() + " with number " + number.ToString());
  }

  // Update is called once per frame
  void Update()
  {
    text.text = number.ToString();

    // move up number each frame
    // if (ableToContact == 1)
    // {
    //   ableToContact = 2;
    // }
    // else if (ableToContact == 2)
    // {
    //   ableToContact = 0;
    // }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    // if (ableToContact == 0) // only allow collision if this block has not had a collision in last two frames
    // {
    number--;
    if (number <= 0)
    {
      gameObject.SetActive(false);
    }
    // ableToContact = 1;
    // }
  }
}
