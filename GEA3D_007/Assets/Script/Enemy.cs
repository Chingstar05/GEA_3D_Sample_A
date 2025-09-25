using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float movespeed = 2f;
    public int health = 5;

    private Transform player;

    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * movespeed * Time.deltaTime;
        transform.LookAt(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        Projectile proj = other.GetComponent<Projectile>();
        if (proj != null)
        {
            TakeDamage(proj.damage);  // Projectile의 damage 값 사용
            Destroy(other.gameObject); // 발사체 삭제
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log($"{gameObject.name} 체력: {health}");

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}

