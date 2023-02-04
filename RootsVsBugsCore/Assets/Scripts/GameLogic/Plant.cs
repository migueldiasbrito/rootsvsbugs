using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    public Resources cost;
    public BaseEntity baseEntity;
    public Projectile projectile;

    private void Start()
    {
        if (projectile == null) return;

        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        while (true)
        {
            yield return new WaitForSeconds(baseEntity.recoil);
            Instantiate(projectile, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (projectile != null) return;

        Bug bug = collision.gameObject.GetComponent<Bug>();

        if (bug == null) return;

        baseEntity.AddEntityToAttack(bug.baseEntity);
        baseEntity.StartAttacking();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (projectile != null) return;

        Bug bug = collision.gameObject.GetComponent<Bug>();

        if (bug == null) return;

        baseEntity.RemoveEntityToAttack(bug.baseEntity);
        if (baseEntity.EntitiesToAttackCount == 0) baseEntity.StopAttacking();
    }
}
