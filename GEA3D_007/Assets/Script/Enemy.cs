using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class Enemy : MonoBehaviour
{
    public enum EnemyState { Idle, Trace, Attack, RunAway }

    public EnemyState state = EnemyState.Idle;

    public float moveSpeed = 2f;

    public float traceRange = 15f;

    public float attackRange = 6f;

    public float attckCooldown = 1.5f;

    public GameObject projectilerPrefab;

    public Transform fire;

    private Transform player;

    private float lastAttackTime;

    public int maxHP = 30;

    private int currentHP;

    public float safeDistance = 10f;

    public Slider HpSlider;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        lastAttackTime = attckCooldown;
        currentHP = maxHP;
        HpSlider.value = 1f;

    }

    void Update()
    {
        if (state != EnemyState.RunAway)   // Dead 체크 필요 없음
        {
            if (currentHP <= maxHP * 0.2f)
            {
                state = (EnemyState.RunAway);
            }
        }

        if (player == null) return;


        float dist = Vector3.Distance(player.position, transform.position);

        switch (state)
        {
            case EnemyState.Idle:
                if (dist < traceRange)
                    state = EnemyState.Trace;
                break;

            case EnemyState.Trace:
                if (dist < attackRange)
                    state = EnemyState.Attack;
                else if (dist > traceRange)
                    state = EnemyState.Idle;
                else
                    TracePlayer();
                break;

            case EnemyState.Attack:
                if (dist > attackRange)
                    state = EnemyState.Trace;
                else
                    AttackPlayer();
                break;

            case EnemyState.RunAway:
                RunAway();
                break;
                



        }

        void TracePlayer()
        {
            Vector3 dir = (player.position - transform.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;
            transform.LookAt(player.position);
        }

        void AttackPlayer()
        {
            if (Time.time >= lastAttackTime + attckCooldown)
            {
                lastAttackTime = Time.time;
                ShootProjectile();
            }
        }

        void ShootProjectile()
        {
            if (projectilerPrefab != null && fire != null)
            {
                transform.LookAt(player.position);
                GameObject proj = Instantiate(projectilerPrefab, fire.position, fire.rotation);
                EnemyProjectile ep = proj.GetComponent<EnemyProjectile>();
                if (ep != null)
                {
                    Vector3 dir = (player.position - fire.position).normalized;
                    ep.SetDirection(dir);
                }
            }

        }

        void RunAway()
        {
            // 플레이어 반대 방향으로 이동
            Vector3 dir = (transform.position - player.position).normalized;
            transform.position += dir * moveSpeed * Time.deltaTime;

            // 일정 거리 이상 벌어지면 Idle로
            float distance = Vector3.Distance(transform.position, player.position);
            if (distance > safeDistance)
            {
                state = (EnemyState.Idle);
            }
        }

    }
    public void TakeDamage(int damage)
    {
        currentHP -= damage;

        HpSlider.value = (float)currentHP / maxHP;
        if (currentHP <= 0)
        {
            Die();
        }

        void Die()
        {
            Destroy(gameObject);
        }

    }
}


       


    


