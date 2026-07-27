using UnityEngine;
using UnityEngine.InputSystem;

public class ManageInputs : MonoBehaviour
{
  private Inputs inputs;
  public InputAction clickAction;
  private InputAction pointer;

  private Vector2 mousePosition, pointerValue, mouseStart;
  public Vector2 mouseChange;

  private GameManager gameManager;

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start()
  {
    inputs = new();
    inputs.Player.Disable();
    inputs.UI.Enable();
    clickAction = inputs.UI.Click;
    pointer = inputs.UI.Point;
    mousePosition = new(0, 0);

    gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
  }

  // Update is called once per frame
  void Update()
  {
    // publish mouse position in game coordinates
    pointerValue = pointer.ReadValue<Vector2>();
    mousePosition.x = pointerValue.x * (7.0f / 635.0f) - 5.565f;
    mousePosition.y = pointerValue.y * (9.0f / 808.0f) - 5.0f;

    if (gameManager.GamePhaseMatches("ready")) // phase where player can click and drag
    {
      if (clickAction.WasPressedThisFrame()) // when player clicks, move to holding phase
      {
        gameManager.SetPhase("holding");
        mouseStart = mousePosition;
      }
    }
    else if (gameManager.GamePhaseMatches("holding")) // phase where player is aiming
    {
      mouseChange = mousePosition - mouseStart;

      if (clickAction.WasReleasedThisFrame()) // when player releases, move to running phase
      {
        gameManager.SetPhase("running");
      }
    }
  }
}
