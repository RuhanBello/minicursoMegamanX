using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
  public CharacterController CharacterController;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.tag == "Wall")
    {
      CharacterController.OnWall = true;
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.tag == "Wall")
    {
      CharacterController.OnWall = false;
    }
  }
}
