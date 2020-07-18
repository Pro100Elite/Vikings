using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject weapons;
    public ParticleSystem hpUp;

    float horizontal;
    float vertical;

    Rigidbody rb;
    Animator animator;
    PlayerState playerState;
    ModileController mobileController;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();
        mobileController = GameObject.FindGameObjectWithTag("Joystick").GetComponent<ModileController>();
    }

    void FixedUpdate()
    {
        if(playerState.cdAttack <= playerState.enableAttack)
        {
            playerState.cdAttack += Time.deltaTime;
        }
        Move();
        Die();
    }

    void Move()
    {
        horizontal = mobileController.Horizontal();
        vertical = mobileController.Vertical();

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

    public void Attack()
    {
        if (playerState.cdAttack >= playerState.enableAttack)
        {
            gameObject.GetComponent<AudioController>().AttackClip();
            Invoke("ActiveAttack", 0.5f);
            animator.SetTrigger("Attack");
            animator.SetBool("Damage", false);
            Invoke("DeActiveAttack", 1f);
            playerState.cdAttack = 0f;
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
            playerState.cdAttack = 1f;
            animator.SetBool("Damage", true);
            animator.SetBool("Move", false);
            animator.SetBool("Idle", false);
        }

        if (other.tag == "HpSphere")
        {
            if (playerState.hp < playerState.maxHp)
            {
                hpUp.Play();
                playerState.hp += 1f;
                Destroy(other.gameObject);
            }
        }
    }
}
