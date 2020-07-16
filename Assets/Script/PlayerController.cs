using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject weapons;
    public ParticleSystem hpUp;

    float horizontal;
    float vertical;
    float cdAttack = 0f;

    Rigidbody rb;
    Animator animator;
    PlayerState playerState;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();
    }

    void FixedUpdate()
    {
        Attack();
        Move();
        Die();
    }

    void Move()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(horizontal, 0.0f, vertical).normalized;

        if (dir.magnitude >= 0.1f)
        {
            animator.SetBool("Move", true);
            animator.SetBool("Idle", false);
            animator.SetBool("Damage", false);

            rb.MovePosition(transform.position + dir * playerState.speed * Time.deltaTime);
            rb.MoveRotation(Quaternion.LookRotation(dir));
        }
        else
        {
            animator.SetBool("Move", false);
            animator.SetBool("Idle", true);
            animator.SetBool("Damage", false);
        }
    }

    void Attack()
    {
        if (cdAttack <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                gameObject.GetComponent<AudioController>().AttackClip();
                Invoke("ActiveAttack", 0.5f);
                animator.SetTrigger("Attack");
                animator.SetBool("Damage", false);
                Invoke("DeActiveAttack", 1f);
                cdAttack = 2f;
            }
        }
        else if (cdAttack > 0)
        {
            cdAttack -= Time.deltaTime;
        }
    }

    void ActiveAttack()
    {
        weapons.SetActive(true);
    }

    void DeActiveAttack()
    {
        weapons.SetActive(false);
    }

    void Die()
    {
        if (playerState.hp <= 0)
        {
            gameObject.GetComponent<PlayerController>().enabled = false;
            gameObject.GetComponent<AudioController>().enabled = false;
            gameObject.GetComponent<AudioSource>().enabled = false;
            animator.SetTrigger("Die");
            animator.SetBool("Damage", false);
            DeActiveAttack();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyAttack")
        {
            gameObject.GetComponent<AudioController>().DamageClip();
            playerState.hp -= 1f;
            cdAttack = 1f;
            animator.SetBool("Damage", true);
            animator.SetBool("Move", false);
            animator.SetBool("Idle", false);
        }

        if (other.tag == "HpSphere")
        {
            hpUp.Play();
            playerState.hp += 1f;
            Destroy(other.gameObject);
        }
    }
}
