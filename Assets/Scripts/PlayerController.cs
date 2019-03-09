using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    public float speed;
    public float jumpForce;
    private int count;
    private int score;
  
    Animator anim;

    public Text countText;
    public Text winText;
    public Text loseText;
    public Text scoreText;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;



    private bool facingRight = true;


    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        count = 0;
        score = 3;
        SetAllText();
        winText.text = "";
        loseText.text = "";
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {


        /*if (Input.GetKey(KeyCode.LeftShift))
            moveHorizontal = 10f;
        else
            moveHorizontal = 3f;

        dirX = Input.GetAxis("Horizontal") * moveHorizontal;

        */
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector2 movement = new Vector2(moveHorizontal, 0);

        rb2d.AddForce(movement * speed);


        if (facingRight == false && moveHorizontal > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveHorizontal < 0)
        {
            Flip();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State 1", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State 1", 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State 1", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State 1", 0);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetInteger("State 1", 3);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetInteger("State 1", 6);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetInteger("State 1", 5);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            anim.SetInteger("State 1", 4);
        }



        if (Input.GetKey("escape"))
            Application.Quit();

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
           /* if (collision.collider.tag == "PlatformBoundries")
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                }
            } */
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetAllText();
        }
        else if (other.gameObject.CompareTag("JellyEnemy"))
        {
            other.gameObject.SetActive(false);
            score = score - 1;
            SetAllText();
        }

        if (count == 4)
        {
            transform.position = new Vector3(30f, transform.position.y, 0f);
        }
    }
    void SetAllText()
    {
        countText.text = "Count: " + count.ToString();
        if (count >= 8)
        {
            winText.text = "You Win!";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
        }
        scoreText.text = "Lives: " + score.ToString();
        if (score <= 0)
        {
            loseText.text = "You Lose!";
            Destroy(gameObject);
        }

        if (Input.GetKey("escape"))
            Application.Quit();
    }
}