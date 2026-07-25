using System;
using UnityEngine;

[RequireComponent(typeof(UpdateNumberOnBlock))]
public class ChangeBlockColor : MonoBehaviour
{
  public SpriteRenderer[] sprites;
  public UpdateNumberOnBlock numberGetter;
  private int number;
  private float red, green, blue, convertedNumber;
  private readonly float SCALE_FACTOR = (float)Math.PI / 128.0f * (256f / 100f);
  private readonly float MIN_VALUE = 3f / 16f;
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
    number = numberGetter.number % 100;
    convertedNumber = number * SCALE_FACTOR;
    red = (float)Math.Max(MIN_VALUE, 2f / 3f * Math.Cos(convertedNumber) + 1f / 3f);
    green = (float)Math.Max(MIN_VALUE, 2f / 3f * Math.Cos(convertedNumber - Math.PI * 2f / 3f) + 1f / 3f);
    blue = (float)Math.Max(MIN_VALUE, 2f / 3f * Math.Cos(convertedNumber + Math.PI * 2f / 3f) + 1f / 3f);
    color = new(red, green, blue);

    foreach (SpriteRenderer sprite in sprites) // color each component of block
    {
      sprite.color = color;
    }
  }
}
