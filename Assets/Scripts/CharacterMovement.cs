using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CharacterMovement : MonoBehaviour
{
    public string PlayerInput;
    public float MoveSpeed = 0f;
    private float horizontal;

    public LayerMask invisibleLayer;

    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;

    private bool pressed;
    private float timer;

    [SerializeField]
    private bool canDash = true;
    [SerializeField]
    private float coolTimer = 3;
    public float coolTime;

    private int oldDir;

    public GameObject effect;
    public List<ShakeTransformEventData> shakeEvents;

    public PlayerShooting _PlayerShooting;

    private void Start()
    {
        dashTime = startDashTime;
        _PlayerShooting = GetComponent<PlayerShooting>();
    }

    private void Update()
    {
        horizontal = Input.GetAxis(PlayerInput);
        NormalMovement();

        Dash();
    }

    private void NormalMovement()
    {
        if (horizontal > 0)
        {
            if (!Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y + 0.5f), invisibleLayer))
            {
                transform.Translate(Vector2.up * horizontal * MoveSpeed * Time.deltaTime, Space.World);
            }
        }
        if (horizontal < 0)
        {
            if (!Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y - 0.5f), invisibleLayer))
            {
                transform.Translate(Vector2.up * horizontal * MoveSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "InvisibleWall")
        {
            dashTime = 0;
            Debug.Log("111");
        }
    }

    private void Dash()
    {
        if (PlayerInput == "PlayerOneMovement")
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (pressed == false)
                {
                    pressed = true;
                    oldDir = 1;
                }
                else
                {
                    if (timer < 0.2f && canDash && oldDir == 1)
                    {
                        direction = 1;
                        Effect();
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                if (pressed == false)
                {
                    pressed = true;
                    oldDir = 2;
                }
                else
                {
                    if (timer < 0.2f && canDash && oldDir == 2)
                    {
                        direction = 2;
                        Effect();
                    }
                }
            }

            if (direction == 0)
            {
            }
            else
            {
                if (dashTime <= 0)
                {
                    direction = 0;
                    dashTime = startDashTime;
                }
                else
                {
                    dashTime -= Time.deltaTime;

                    if (direction == 1)
                    {
                        if (!Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y + 0.5f + dashSpeed), invisibleLayer))
                        {
                            transform.Translate(Vector2.up * dashSpeed, Space.World);
                        }
                        else
                        {
                            dashTime = 0;
                        }
                    }
                    else if (direction == 2)
                    {
                        if (!Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y - 0.5f - dashSpeed), invisibleLayer))
                        {
                            transform.Translate(-Vector2.up * dashSpeed, Space.World);
                        }
                        else
                        {
                            dashTime = 0;
                        }
                    }
                }
            }
        }
        else if (PlayerInput == "PlayerTwoMovement")
        {
            if (Input.GetKeyDown(KeyCode.Keypad6) || Input.GetKeyDown(KeyCode.RightBracket))
            {
                if (pressed == false)
                {
                    pressed = true;
                    oldDir = 1;
                }
                else
                {
                    if (timer < 0.2f && canDash && oldDir == 1)
                    {
                        direction = 1;
                        Effect();
                    }
                }
            }
            else if (Input.GetKeyDown(KeyCode.Keypad3) || Input.GetKeyDown(KeyCode.Quote))
            {
                if (pressed == false)
                {
                    pressed = true;
                    oldDir = 2;
                }
                else
                {
                    if (timer < 0.2f && canDash && oldDir == 2)
                    {
                        direction = 2;
                        Effect();
                    }
                }
            }

            if (direction == 0)
            {
            }
            else
            {
                if (dashTime <= 0)
                {
                    direction = 0;
                    dashTime = startDashTime;
                }
                else
                {
                    dashTime -= Time.deltaTime;

                    if (direction == 1)
                    {
                        if (!Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y + 0.5f + dashSpeed), invisibleLayer))
                        {
                            transform.Translate(Vector2.up * dashSpeed, Space.World);
                        }
                        else
                        {
                            dashTime = 0;
                        }
                    }
                    else if (direction == 2)
                    {
                        if (!Physics2D.Linecast(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x, transform.position.y - 0.5f - dashSpeed), invisibleLayer))
                        {
                            transform.Translate(-Vector2.up * dashSpeed, Space.World);
                        }
                        else
                        {
                            dashTime = 0;
                        }
                    }
                }
            }
        }
        if (pressed == true)
        {
            timer += Time.deltaTime;
            if (timer > 0.2f)
            {
                pressed = false;
                timer = 0;
            }
        }
        if (canDash == false)
        {
            if (coolTimer <= 0)
            {
                canDash = true;
                coolTimer = coolTime;
            }
            else
            {
                coolTimer -= Time.deltaTime;
            }
        }
    }

    private void Effect()
    {
        _PlayerShooting.canShoot = false;
        Vector3 oriScale = transform.localScale;
        canDash = false;
        Instantiate(effect, transform.position, Quaternion.identity);
        transform.localScale = new Vector3(0.01f, 0.01f, 1);
        transform.DOScale(oriScale, 0.2f).SetEase(Ease.OutQuad).OnComplete(() => _PlayerShooting.canShoot = true);
        Camera.main.GetComponentInParent<ShakeTranform>().AddShakeEvent(shakeEvents[0]);
    }
}
