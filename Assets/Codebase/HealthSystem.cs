using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Range(0f, 100f)]
    public float health;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        ChekDamage();
    }


    private void ChekDamage()
    {
        if (Math.Abs(rb.velocity.magnitude) > 10f)
        {
            SetDamage(10f);
        }
        else if (Math.Abs(rb.velocity.magnitude) > 5f)
        {
            SetDamage(5f);
        }
        else
            return;
    }
    private void SetDamage(float damage)
    {
        health -= damage;
        ChekDeath();
    }

    private void ChekDeath()
    {
        if (health <= 0)
        {
            Debug.Log("THE END");
        }
    }

    private void Update()
    {
        Debug.Log(rb.velocity.magnitude);
    }
}
