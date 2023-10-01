using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HealthSystem : MonoBehaviour
{
    [Range(0f, 100f)]
    public float health;

    private Rigidbody rb;
    private GameManager gameManager;

    public Transform Brain;
    private Tween brainTweenIdle;
    private Tween brainTweenShake;

    public MeshFilter GlassBankOrigin;
    public Mesh[] GlassBanks;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.instance;
        //AnimationLoop();
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
        GlassBankAnim();
        ShakeSindrome();
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
        // MB added Camera Shake?
        brainTweenShake.Kill();
        brainTweenShake = Brain.transform.DOPunchPosition(Vector3.down * 0.1f, 2f, 10);
    }

    private void AnimationLoop()
    {
        if (health > 0)
        {
            brainTweenIdle = Brain.DOScale(Vector3.one * 1.1f, 2f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            brainTweenIdle.Kill();
        }
    }

    private void GlassBankAnim()
    {
        if (health < 60 && health > 30)
        {
            GlassBankOrigin.mesh = GlassBanks[0];
        }
        else if (health < 30 && health > 00)
        {
            GlassBankOrigin.mesh = GlassBanks[1];
        }
        else if (health < 0)
        {
            GlassBankOrigin.mesh = GlassBanks[2];
        }
    }

}
