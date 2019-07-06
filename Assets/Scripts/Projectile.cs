using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
  public CharacterController Character;

  public float Speed;
  public int Direction;

  public int Damage;

  // Update is called once per frame
  void Update()
  {
    transform.Translate(new Vector3(Direction * Speed * Time.deltaTime, 0, 0));
  }

  private void OnBecameInvisible()
  {
    Character.CurrentProjectilesAmount--;
    Destroy(gameObject);
  }
}
