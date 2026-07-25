using System;
using UnityEngine;

[RequireComponent(typeof(UpdateNumberOnBlock))]
public class ChangeBlockColor : MonoBehaviour
{
  public SpriteRenderer[] sprites;
  public UpdateNumberOnBlock numberGetter;
  private int number;
  private float red, green, blue;
  private Color color;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    sprites = GetComponentsInChildren<SpriteRenderer>();
    numberGetter = gameObject.GetComponent<UpdateNumberOnBlock>();
  }

  // Update is called once per frame
  void Update()
  {
    // create color based on number on block
    number = numberGetter.number % 256;
    red = Math.Max(0, 3f * Math.Abs(number - 128f) - 128f);
    green = Math.Max(0, 256f - 3f * Math.Abs(number - (512f / 3f)));
    blue = Math.Max(0, 256f - 3f * Math.Abs(number - (256f / 3f)));
    color = new(red, green, blue);

    foreach (SpriteRenderer sprite in sprites) // color each component of block
    {
      sprite.color = color;
    }
  }
}
