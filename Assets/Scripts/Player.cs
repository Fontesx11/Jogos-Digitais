using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public PauseMenu pauseMenu;

    public float Speed;
    public float JumpForce;
    public float DashForce; // Força do dash
    public float DashCooldown = 3f; // Cooldown do dash
    public float SpeedReductionPercentage = 0.75f; // Redução da velocidade após o dash

    private bool canDash = true;
    private float lastDashTime;

    public bool isJumping;
    public bool dubleJumping;

    private Rigidbody2D rig;
    private Animator anim;

    public GameObject Saw;
    public GameObject Saw2;
    public GameObject Fall;

    Vector2 spawnPosition;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spawnPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();
        if (Input.GetKeyDown(KeyCode.F)) // Pressionar Shift Esquerdo para dash
        {
            Debug.Log("teste");
            Dash();
        }
    } 

    void Move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        if((Input.GetAxis("Horizontal") > 0f))
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 0f, 0f);
        }

        if ((Input.GetAxis("Horizontal") < 0f))
        {
            anim.SetBool("walk", true);
            transform.eulerAngles = new Vector3(0f, 180f, 0f);
        }

        if ((Input.GetAxis("Horizontal") == 0f))
        {
            anim.SetBool("walk", false);
        }
    } 

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            if (!isJumping)
            {
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                dubleJumping = true;
                anim.SetBool("jump", true);
            }
            else
            {
                if (dubleJumping)
                {
                    rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
                    dubleJumping = false;
                }
            }
        }
    }

    void Dash()
    {
        if (canDash)
        {
            float dashDirection = transform.eulerAngles.y == 0f ? 1f : -1f; // Direção do dash
            rig.AddForce(new Vector2(dashDirection * DashForce, 0f), ForceMode2D.Impulse);
            canDash = false;
            lastDashTime = Time.time;

            // Reduz a velocidade do personagem após o dash
            StartCoroutine(ReduceSpeedTemporarily());
        }
    }

    IEnumerator ReduceSpeedTemporarily()
    {
        float originalSpeed = Speed;
        Speed *= SpeedReductionPercentage; // Reduz a velocidade
        yield return new WaitForSeconds(DashCooldown); // Espera pelo cooldown
        Speed = originalSpeed; // Restaura a velocidade original
        canDash = true; // Permite dash novamente após o cooldown
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = false;
            anim.SetBool("jump", false);
        }

        if (collision.gameObject.layer == 9)
        {   
            Destroy(Saw);
            Destroy(Saw2);
        }

        if (collision.gameObject.tag == "Saw")
        {
             pauseMenu.GameOver();
        }

         if (collision.gameObject.tag == "Fall")
        {
             Destroy(Fall);
        }

        if (collision.gameObject.tag == "Spikes")
        {
            pauseMenu.GameOver();
           
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            isJumping = true;
        }
    }
}
