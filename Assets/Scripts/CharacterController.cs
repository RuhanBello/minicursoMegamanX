using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
  public float Speed;
  public float JumpForce;

  public bool IsGrounded;
  public bool OnWall;
  public bool IsClimbing;

  public GameObject SmallProjectile;
  public GameObject MediumProjectile;
  public GameObject BigProjectile;

  public Transform ProjectileOrigin;
  public bool CanShoot = true;
  public float ShootDelay;
  public int ProjectilesLimit = 3;
  public int CurrentProjectilesAmount = 0;

  public float ChargingTimer;

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
          if (transform.localScale.x > 0)
            Rigidbody.AddForce(new Vector2(-220, 180));

          if (transform.localScale.x < 0)
            Rigidbody.AddForce(new Vector2(220, 180));
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
    if (OnWall && _horizontal != 0 && Rigidbody.velocity.y <= 0)
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
    if (Input.GetMouseButton(0))
    {
      ChargingTimer += Time.deltaTime;
    }

    if (Input.GetMouseButtonUp(0) && CanShoot && CurrentProjectilesAmount < ProjectilesLimit)
    {
      GameObject selectedProjectile = null;
      if(ChargingTimer > 0 && ChargingTimer <= 1)
      {
        selectedProjectile = SmallProjectile;
      }

      if(ChargingTimer > 1 && ChargingTimer <= 3)
      {
        selectedProjectile = MediumProjectile;
      }

      if(ChargingTimer > 3)
      {
        selectedProjectile = BigProjectile;
      }

      ChargingTimer = 0;

      GameObject newProjectile = Instantiate(selectedProjectile, ProjectileOrigin.position, Quaternion.identity);
      Projectile p = newProjectile.GetComponent<Projectile>();
      p.Direction = (int)transform.localScale.x;
      p.Character = this;

      newProjectile.transform.localScale = new Vector3(p.Direction, 1, 1);
      CurrentProjectilesAmount++;

      StartCoroutine(ShootDelayCoroutine());
    }
  }

  private IEnumerator ShootDelayCoroutine()
  {
    CanShoot = false;
    yield return new WaitForSeconds(ShootDelay);
    CanShoot = true;
  }

  private void Animate()
  {
    Animator.SetFloat("hSpeed", Mathf.Abs(_horizontal));
    Animator.SetFloat("vSpeed", Rigidbody.velocity.y);
  }
}
