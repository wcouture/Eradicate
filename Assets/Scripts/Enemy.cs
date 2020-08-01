using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float moveSpeed = 1;
    private Transform playerPosition;
    private Transform enemyPosition;
    private Vector2 playerPosVect;
    private Vector2 enemyPosVect;
    private Vector2 aimVect;
    private Rigidbody2D rb;
    private Random rand;
    private float horizontal;
    private float vertical;
    private float angle;
    private bool shot = false;

    private void Awake()
    {
        playerPosition = GameObject.Find("Player").GetComponent<Transform>();
        enemyPosition = GetComponent<Transform>();
        GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("FXvolume");

        rb = GetComponent<Rigidbody2D>();
        float xPos, yPos, distance;

        do
        {
            xPos = Random.Range(-7, 8);
            yPos = Random.Range(-3, 5);

            float xDif = playerPosition.position.x - xPos;
            float yDif = playerPosition.position.y - yPos;
            distance = Mathf.Sqrt((xDif * xDif) + (yDif * yDif));
        } while (distance<2);

        enemyPosition.position = new Vector2(xPos,yPos);
        StartCoroutine("sounds");
    }

    private void FixedUpdate()
    {
        CalcPositions();
        CalcAim();
        move();
    }

    void CalcPositions()
    {
        enemyPosVect = enemyPosition.position;
        playerPosVect = playerPosition.position;
    }

    void CalcAim()
    {
        aimVect = playerPosVect - enemyPosVect;
        angle = Mathf.Atan2(aimVect.y, aimVect.x);
    }

    private void move()
    {
        horizontal = Mathf.Cos(angle);
        vertical = Mathf.Sin(angle);
        rb.velocity = new Vector2(horizontal,vertical);
        float angleDeg = angle * Mathf.Rad2Deg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if(other.tag == "projectile" && !shot)
        {
            shot = true;
            gameObject.tag = "dead";
            GetComponentInChildren<SpriteRenderer>().enabled = false;
            GetComponent<Enemy>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StartCoroutine("wait");
        }
    }

    IEnumerator wait()
    {
        GetComponentInChildren<ParticleSystem>().Play();
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }

    IEnumerator sounds()
    {
        while (!shot)
        {
            float rand = Random.Range(1, 10);
            if(rand > 5)
            {
                GetComponent<AudioSource>().Play();
            }
            yield return new WaitForSeconds(1);
        }
    }

}
