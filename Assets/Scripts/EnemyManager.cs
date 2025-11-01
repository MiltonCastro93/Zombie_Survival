using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyManager : MonoBehaviour{
    public GameObject player;
    public Animator enemyAnimator;

    public float damage = 20f;
    public float health = 100f;

    public GameManager gameManager;

    public Slider healthBar;

    public bool playerInReach;
    public float attackDelayTimer;
    public float howMuchEarlierStartAttackAnim;
    public float delayBetweenAttacks;

    public AudioSource enemyAudioSource;
    public AudioClip[] growlAudioClips;

    public int points = 20;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemyAudioSource = GetComponent<AudioSource>();
        healthBar.maxValue = health;
        healthBar.value = health;
    }

    void Update()
    {
        if (!enemyAudioSource.isPlaying)
        {
            enemyAudioSource.clip = growlAudioClips[Random.Range(0, growlAudioClips.Length)];
            enemyAudioSource.Play();
        }

        GetComponent<NavMeshAgent>().destination = player.transform.position;

        healthBar.transform.LookAt(player.transform);

        if(GetComponent<NavMeshAgent>().velocity.magnitude == 0)
        {
            enemyAnimator.SetBool("isRunning", false);
        } else {
            enemyAnimator.SetBool("isRunning", true);
        }


    }



    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject == player)
        {
            playerInReach = true;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (playerInReach)
        {
            attackDelayTimer += Time.deltaTime;

            if (attackDelayTimer >= delayBetweenAttacks - howMuchEarlierStartAttackAnim && attackDelayTimer <= delayBetweenAttacks)
            {
                enemyAnimator.SetTrigger("isAttacking");
            }

            if (attackDelayTimer >= delayBetweenAttacks)
            {
                player.GetComponent<PlayerManager>().Hit(damage);
                attackDelayTimer = 0;
            }
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject == player)
        {
            playerInReach = false;
            attackDelayTimer = 0;
        }
    }

    public void Hit(float damage)
    {
        health -= damage;
        healthBar.value = health;

        if (health <= 0)
        {
            enemyAnimator.SetTrigger("isDead");
            gameManager.enemiesAlive--;

            player.GetComponent<PlayerManager>().UpdatePoints(points);

            Destroy(gameObject, 10f);
            Destroy(GetComponent<NavMeshAgent>());
            Destroy(GetComponent<EnemyManager>());
            Destroy(GetComponent<CapsuleCollider>());
        }
    }
}
