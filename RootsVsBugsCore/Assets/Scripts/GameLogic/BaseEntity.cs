using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEntity : MonoBehaviour
{
    public int totalHealth;
    public int attack;
    public float recoil;
    public Action Died;

    public HealthDisplayView healthDisplayViewPrefab;
    public Transform healthDisplayHolder;
    private HealthDisplayView healthDisplayView;
    private Camera sceneCamera;

    private int currentHealth;

    private List<BaseEntity> entitiesToAttack = new();
    public int EntitiesToAttackCount => entitiesToAttack.Count;
    private Coroutine attackCoroutine;

    private void Start()
    {
        currentHealth = totalHealth;
    }

    private void OnDestroy()
    {
        if (healthDisplayView != null)
        {
            Destroy(healthDisplayView.gameObject);
        }
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
            Died?.Invoke();
            Destroy(gameObject);
        }
        else
        {
            healthDisplayView.UpdateHealthBar(currentHealth, totalHealth);
        }
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            float recoilTime = 0;
            foreach (BaseEntity entity in entitiesToAttack)
            {
                if (entity == null) continue;

                entity.TakeDamage(attack);
                recoilTime = recoil;
                break;
            }
            yield return new WaitForSeconds(recoilTime);
        }
    }

    public void SetUiOptions(Camera sceneCamera, Transform healthBarsHolder)
    {
        this.sceneCamera = sceneCamera;
        healthDisplayView = Instantiate(healthDisplayViewPrefab, healthBarsHolder);
        UpdateHealthBarPosition();
    }

    public void UpdateHealthBarPosition()
    {
        healthDisplayView.transform.position = sceneCamera.WorldToScreenPoint(healthDisplayHolder.position);
    }

    public void DestoryHealthBar()
    {
        Destroy(healthDisplayView.gameObject);
    }
}
