using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DetectorType { Left, Right }

public class WallDetector : MonoBehaviour
{
  public CharacterController CharacterController;
  public DetectorType Type;

  private void OnTriggerEnter2D(Collider2D collision)
  {
    if(collision.tag == "Wall")
    {
      if (Type == DetectorType.Left)
        CharacterController.OnLeftWall = true;

      if (Type == DetectorType.Right)
        CharacterController.OnRightWall = true;
    }
  }

  private void OnTriggerExit2D(Collider2D collision)
  {
    if (collision.tag == "Wall")
    {
      if (Type == DetectorType.Left)
        CharacterController.OnLeftWall = false;

      if (Type == DetectorType.Right)
        CharacterController.OnRightWall = false;
    }
  }
}
