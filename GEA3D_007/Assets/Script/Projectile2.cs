using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2 : MonoBehaviour
{
    public float speed = 20f;
    public float lifeTime = 3f;
    public int damage = 5;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);

            Destroy(gameObject);
        }
    }
}
