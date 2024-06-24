using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    private CircleCollider2D r2B;
    private void Start()
    {
        r2B = GetComponent<CircleCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag(Tag.blockline))
        {
            r2B.isTrigger = true;
        }
        else
        {
            r2B.isTrigger = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        r2B.isTrigger = false;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        r2B.isTrigger = false;
    }
}
