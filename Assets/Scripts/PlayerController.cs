using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform tm;
    private float horizontal;
    private float vertical;
    public float Speed;
    public GameObject player;
    private Vector2 vect1;
    public float health = 5;
    public bool alive = true;
    public Image[] hearts;
    private Animator animator;
    public GameObject Sprite;
    private ParticleSystem pSystem;
    bool dead = false;

    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("FXvolume");
        pSystem = GameObject.Find("PlayerParticleSystem").GetComponent<ParticleSystem>();
        animator = Sprite.GetComponent<Animator>();
        rb = player.GetComponent<Rigidbody2D>();
        tm = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (alive)
        {
            calcDirection();
            updatePosition();
            Animate();
        }
        else if(!dead)
        {
            rb.velocity = new Vector2(0,0);
            GameObject.Find("Sprite").GetComponent<SpriteRenderer>().enabled = false;
            pSystem.Play();
            dead = true;
        }
        
    }

    void calcDirection()
    {
        horizontal = Input.GetAxisRaw("Horizontal") * Speed * Time.fixedDeltaTime;
        vertical = Input.GetAxisRaw("Vertical") * Speed * Time.fixedDeltaTime;

        vect1.x = horizontal;
        vect1.y = vertical;
    }

    void updatePosition()
    {
        rb.velocity = vect1;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject damager = collision.gameObject;
        if(damager != null && health != 0 && damager.tag == "enemy")
        {
            Destroy(hearts[(int)health - 1]);
            health--;
            GetComponent<AudioSource>().Play();
            checkHealth();
        }
    }

    private void checkHealth()
    {
        if(health <= 0)
        {
            alive = false;
        }
    }

    private void Animate()
    {
        animator.SetFloat("Speed", rb.velocity.magnitude);
    }

    IEnumerator deathCoroutine()
    {

        return null;
    }
}
