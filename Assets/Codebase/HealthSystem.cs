using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;

public class HealthSystem : MonoBehaviour
{
    [Range(0f, 100f)]
    public float health;

    private Rigidbody rb;
    private GameManager gameManager;
    private AudioManager audioManager;

    public Transform Brain;
    private Tween brainTweenIdle;
    private Tween brainTweenShake;

    public MeshFilter GlassBankOrigin;
    public Mesh[] GlassBanks;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameManager = GameManager.instance;
        audioManager = AudioManager.instance;
        AnimationLoop();

        gameManager.SetReferencesUI();
    }


    private void OnCollisionEnter(Collision collision)
    {
        ChekDamage();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
            gameManager.ChekWin();
    }


    private void ChekDamage()
    {
        if (Math.Abs(rb.velocity.magnitude) > 10f)
        {
            SetDamage(10f);
            audioManager.Play("Fall1");
        }
        else if (Math.Abs(rb.velocity.magnitude) > 5f)
        {
            SetDamage(5f);
            audioManager.Play("Fall3");
        }
        else
            return;
    }
    private void SetDamage(float damage)
    {
        health -= damage;
        audioManager.Play("GlassMoving");
        GlassBankAnim();
        ShakeSindrome();
        ChekDeath();
    }

    private void ChekDeath()
    {
        if (health <= 0)
        {
            gameManager.ChekLose();
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

    public void GlassBankAnim()
    {
        if (health > 60)
        {
            GlassBankOrigin.mesh = GlassBanks[0];
            audioManager.Play("BrainIdle");
        }
        if (health < 60 && health > 30)
        {
            GlassBankOrigin.mesh = GlassBanks[1];
            audioManager.Play("BrainIdle");
        }
        else if (health < 30 && health > 00)
        {
            GlassBankOrigin.mesh = GlassBanks[2];
            audioManager.Play("BrainIdle");
        }
        else if (health < 0)
        {
            GlassBankOrigin.mesh = GlassBanks[3];
            audioManager.Play("BrainDeath");
        }
    }

    private void OnDestroy()
    {
        brainTweenIdle.Kill();
    }

}
