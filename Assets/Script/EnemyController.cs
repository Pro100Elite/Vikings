using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public GameObject weapons;
    public GameObject dieEffect;
    public GameObject hpSphere;


    private float dist;

    private GameObject player;
    private NavMeshAgent nav;
    private Animator animator;
    private EnemyState enemyState;
    private ScoreController score;

    void Start()
    {
        score = FindObjectOfType<ScoreController>();
        player = GameObject.FindGameObjectWithTag("Player");
        nav = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        enemyState = GetComponent<EnemyState>();
    }
    private void FixedUpdate()
    {
        Move();
        Attack();
        Die();
    }

    void Move()
    {
        dist = Vector3.Distance(player.transform.position, transform.position);

        if (dist > 3.5f)
        {
            weapons.SetActive(false);
            nav.isStopped = false;
            if (player != null)
            {
                try
                {
                    nav.SetDestination(player.transform.position);
                }
                catch
                {
                    Destroy(gameObject);
                }
                animator.SetTrigger("Move");
            }
        }
    }

    void Attack()
    {
        if (dist < 2.5f)
        {
            weapons.SetActive(true);
            animator.Play("Attack");
            nav.isStopped = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerAttack")
        {
            gameObject.GetComponent<AudioController>().DamageClip();
            animator.SetTrigger("Damage");
            enemyState.hp -= 1f;
        }
    }
    void Die()
    {
        if (enemyState.hp <= 0)
        {
            gameObject.GetComponent<EnemyController>().enabled = false;
            animator.SetTrigger("Die");
            Invoke("Dead", 3f);
        }
    }

    void Dead()
    {
        Instantiate(hpSphere, transform.position, transform.rotation);
        score.value += 1;
        dieEffect.SetActive(true);
        Destroy(gameObject, 3.1f);
    }
}
