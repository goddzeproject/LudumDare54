using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [Range(0f, 100f)]
    public float health;

    private Rigidbody rb;
    private GameManager gameManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        ChekDamage();

        if (collision.collider.CompareTag("Finish"))
            gameManager.ChekWin();
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

    private void ShakeSindrome()
    {
        // Camerca Shake if HP less 40;
    }
}
