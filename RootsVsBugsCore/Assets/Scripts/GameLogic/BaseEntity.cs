using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    public int totalHealth;
    public int attack;
    public float recoil;

    private int currentHealth;

    private List<BaseEntity> entitiesToAttack = new();
    public int EntitiesToAttackCount => entitiesToAttack.Count;
    private Coroutine attackCoroutine;

    private void Start()
    {
        currentHealth = totalHealth;
    }

    public void AddEntityToAttack(BaseEntity entity)
    {
        entitiesToAttack.Add(entity);
    }

    public void RemoveEntityToAttack(BaseEntity entity)
    {
        entitiesToAttack.Remove(entity);
    }

    public void StartAttacking()
    {
        if (attackCoroutine != null) return;
        attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    public void StopAttacking()
    {
        if (attackCoroutine == null) return;
        StopCoroutine(attackCoroutine);
        attackCoroutine = null;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            if (entitiesToAttack.Count < 0) continue;

            entitiesToAttack[0].TakeDamage(attack);

            yield return new WaitForSeconds(recoil);
        }
    }
}
