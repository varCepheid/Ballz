using UnityEngine;

class EnableChildren : MonoBehaviour
{
  public GameObject text, circle1, circle2, circle3;

  public void EnableThem()
  {
    text.SetActive(true);
    circle1.SetActive(true);
    circle2.SetActive(true);
    circle3.SetActive(true);
  }
}