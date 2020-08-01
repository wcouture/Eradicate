using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOrderLayer : MonoBehaviour
{
    private SpriteRenderer SR;
    private Transform tform;

    private void Awake()
    {
        SR = GetComponent<SpriteRenderer>();
        tform = GetComponent<Transform>();
    }

    private void Update()
    {
        SR.sortingOrder = (int)(1000000 - (tform.position.y * 100));
    }
}
