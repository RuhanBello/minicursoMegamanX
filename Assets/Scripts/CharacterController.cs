using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
  public float Speed;
  public float JumpForce;

  public bool IsGrounded;

  public Rigidbody2D Rigidbody;

  public Animator Animator;

  private SpriteRenderer _spriteRenderer;

  private float _horizontal;

  // Start is called before the first frame update
  void Start()
  {
    _spriteRenderer = GetComponent<SpriteRenderer>();
    Animator = GetComponent<Animator>();
  }

  // Update is called once per frame
  void Update()
  {
    Movement();
    Jump();
    Flip();
    Animate();
  }

  private void Movement()
  {
    _horizontal = Input.GetAxisRaw("Horizontal");

    Rigidbody.velocity = new Vector2(_horizontal * Speed, Rigidbody.velocity.y);
  }

  private void Flip()
  {
    if (_horizontal > 0)
      _spriteRenderer.flipX = false;

    if (_horizontal < 0)
      _spriteRenderer.flipX = true;
  }

  private void Jump()
  {
    if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
    {
      Rigidbody.AddForce(new Vector2(0, JumpForce));
    }

    if (Input.GetKeyUp(KeyCode.Space) && Rigidbody.velocity.y > 0)
    {
      Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0);
    }
  }

  private void Animate()
  {
    Animator.SetFloat("hSpeed", Mathf.Abs(_horizontal));
  }
}
