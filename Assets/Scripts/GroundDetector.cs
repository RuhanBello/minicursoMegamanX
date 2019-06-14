using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
  public CharacterController CharacterController;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    CharacterController.IsGrounded = true;
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    CharacterController.IsGrounded = false;
  }
}
