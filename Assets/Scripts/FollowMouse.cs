using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class FollowMouse : MonoBehaviour
{
    private Transform aimtransform;
    private GameObject Sprite;
    private Animator anim;
    private Animator anim2;
    private Animator sceneAnim;
    private PlayerController controller;
    public GameObject bullet;
    public float angle;
    public Vector3 aimVect;
    private ParticleSystem particles;
    private void Awake()
    {
        controller = GameObject.Find("Logic").GetComponent<PlayerController>();
        particles = GameObject.Find("GunParticleSystem").GetComponent<ParticleSystem>();
        Sprite = GameObject.Find("Sprite");
        aimtransform = transform.Find("Gun");
        anim = Sprite.GetComponent<Animator>();
        anim2 = GameObject.Find("Gun").GetComponentInChildren<Animator>();
        sceneAnim = GameObject.Find("Main Camera").GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.alive)
        {
            Vector3 MousePosition = UtilsClass.GetMouseWorldPosition();

            aimVect = (MousePosition - transform.position).normalized;

            angle = Mathf.Atan2(aimVect.y, aimVect.x) * Mathf.Rad2Deg;

            aimtransform.eulerAngles = new Vector3(0, 0, angle);

            if (angle >= 0)
            {
                anim.SetFloat("Angle", angle);
                anim2.SetFloat("angle", angle);
            }
            else
            {
                anim.SetFloat("Angle", 360 + angle);
                anim2.SetFloat("angle", 360 + angle);
            }

            if (angle < -135 || angle > 135)
            {
                aimtransform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                aimtransform.localScale = new Vector3(1, 1, 1);
            }
            if (Input.GetButtonDown("Fire1"))
            {
                anim2.SetTrigger("Fire");
                sceneAnim.SetTrigger("GunRumble");
                Instantiate(bullet);
                particles.Play();

            }
        }
        else
        {
            GameObject.Find("Gun").GetComponentInChildren<SpriteRenderer>().enabled = false;
        }
        
    }
}
