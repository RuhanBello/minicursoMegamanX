using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour
{
  public CharacterController CharacterController;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.tag == "Ground")
    {
      CharacterController.IsGrounded = true;
      CharacterController.Animator.SetBool("isGrounded", true);
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.tag == "Ground")
    {
      CharacterController.IsGrounded = false;
      CharacterController.Animator.SetBool("isGrounded", false);
    }
  }
}
