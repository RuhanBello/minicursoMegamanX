using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
  public float Speed;
  public float JumpForce;

  public bool IsGrounded;
  public bool OnLeftWall;
  public bool OnRightWall;
  public bool IsClimbing;

  public GameObject ProjectilePrefab;
  public Transform ProjectileOrigin;

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
    Climb();
    Shoot();
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
      transform.localScale = new Vector3(1, 1, 1);

    if (_horizontal < 0)
      transform.localScale = new Vector3(-1, 1, 1);
  }

  private void Jump()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      if (IsGrounded)
      {
        Rigidbody.AddForce(new Vector2(0, JumpForce));
      }
      else
      {
        if (IsClimbing)
        {
          if(OnLeftWall)
            Rigidbody.AddForce(new Vector2(220, 180));

          if(OnRightWall)
            Rigidbody.AddForce(new Vector2(-220, 180));
        }
      }
    }

    if (Input.GetKeyUp(KeyCode.Space) && Rigidbody.velocity.y > 0)
    {
      Rigidbody.velocity = new Vector2(Rigidbody.velocity.x, 0);
    }
  }

  private void Climb()
  {
    if (((_horizontal < 0 && OnLeftWall) || (_horizontal > 0 && OnRightWall)) && Rigidbody.velocity.y <= 0)
    {
      Rigidbody.velocity = new Vector2(0, -0.5f);
      IsClimbing = true;
      Animator.SetBool("isClimbing", true);
    }
    else
    {
      IsClimbing = false;
      Animator.SetBool("isClimbing", false);
    }
  }

  private void Shoot()
  {
    if (Input.GetMouseButtonUp(0))
    {
      GameObject newProjectile = Instantiate(ProjectilePrefab, ProjectileOrigin.position, Quaternion.identity);
      Projectile p = newProjectile.GetComponent<Projectile>();
      p.Direction = (int)transform.localScale.x;
    }
  }

  private void Animate()
  {
    Animator.SetFloat("hSpeed", Mathf.Abs(_horizontal));
    Animator.SetFloat("vSpeed", Rigidbody.velocity.y);
  }
}
