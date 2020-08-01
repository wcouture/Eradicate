using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject light;
    public GameObject particles;
    public AudioClip impact;
    private FollowMouse pointerScript;
    private Rigidbody2D rb;
    private float horizontal;
    private float vertical;
    private float speed = 300f;
    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        pointerScript = GameObject.Find("Player").GetComponent<FollowMouse>();

        float angle = pointerScript.angle;
        rb.rotation = angle;

        horizontal = Mathf.Cos(angle * Mathf.Deg2Rad) * speed;
        vertical = Mathf.Sin(angle * Mathf.Deg2Rad) * speed;

        GameObject spawn = GameObject.Find("barrel");
        transform.position = new Vector2(spawn.transform.position.x, spawn.transform.position.y);

        rb.AddForce(new Vector2(horizontal, vertical));

        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("FXvolume");
        GetComponent<AudioSource>().Play();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "scene")
        {
            Debug.Log("Hit Wall");
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(light);
            rb.velocity = Vector2.zero;
            StartCoroutine("waitForParticles");
        }
        else if(collision.gameObject.tag != "dead")
        {
            GetComponent<SpriteRenderer>().enabled = false;
            Destroy(light);
            rb.velocity = Vector2.zero;
            StartCoroutine("waitForSound");
        }
    }

    IEnumerator waitForParticles()
    {
        particles.GetComponent<ParticleSystem>().Play();
        GetComponent<AudioSource>().clip = impact;
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    IEnumerator waitForSound()
    {
        AudioSource ausrc = GetComponent<AudioSource>();
        ausrc.clip = impact;
        ausrc.Play();
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }


}
