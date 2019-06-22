using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  public float Speed;
  public int Direction;

  // Update is called once per frame
  void Update()
  {
    transform.Translate(new Vector3(Direction * Speed * Time.deltaTime, 0, 0));
  }
}
