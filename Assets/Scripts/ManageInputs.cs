using UnityEngine;
using UnityEngine.InputSystem;

public class ManageInputs : MonoBehaviour
{
  private Inputs inputs;
  public InputAction clickAction;
  private InputAction pointer;
  public Vector2 mousePosition, pointerValue;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    inputs = new();
    inputs.Player.Disable();
    inputs.UI.Enable();
    clickAction = inputs.UI.Click;
    pointer = inputs.UI.Point;
    mousePosition = new(0, 0);
  }

  // Update is called once per frame
  void Update()
  {
    pointerValue = pointer.ReadValue<Vector2>();
    mousePosition.x = pointerValue.x * (7.0f / 635.0f) - 5.565f;
    mousePosition.y = pointerValue.y * (9.0f / 808.0f) - 5.0f;
  }
}
