using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public int attack;
    public int speed;
    public int maxEnemiesHit;

    private void Update()
    {
        Vector3 position = transform.position;
        position.x += speed * Time.deltaTime;
        transform.position = position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Bug bug = collision.gameObject.GetComponent<Bug>();

        if (bug == null) return;

        bug.baseEntity.TakeDamage(attack);
        maxEnemiesHit--;

        if (maxEnemiesHit <= 0) Destroy(gameObject);
    }
}
