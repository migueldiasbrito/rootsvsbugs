using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public BaseEntity baseEntity;
    public float speed;

    private enum State { Run, Attack}
    private State state = State.Run;

    private void Update()
    {
        if (state == State.Run)
        {
            Vector3 position = transform.position;
            position.x += speed * Time.deltaTime;
            transform.position = position;

            baseEntity.UpdateHealthBarPosition();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaseEntity otherBaseEntity;
        Plant plant = collision.gameObject.GetComponent<Plant>();
        TreeRoot treeRoot = collision.gameObject.GetComponent<TreeRoot>();

        if (plant != null)
        {
            otherBaseEntity = plant.baseEntity;
        }
        else if (treeRoot != null)
        {
            otherBaseEntity = treeRoot.baseEntity;
        }
        else
        {
            return;
        }

        baseEntity.AddEntityToAttack(otherBaseEntity);

        if (state != State.Attack)
        {
            state = State.Attack;
            baseEntity.StartAttacking();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        BaseEntity otherBaseEntity;
        Plant plant = collision.gameObject.GetComponent<Plant>();
        TreeRoot treeRoot = collision.gameObject.GetComponent<TreeRoot>();

        if (plant != null)
        {
            otherBaseEntity = plant.baseEntity;
        }
        else if (treeRoot != null)
        {
            otherBaseEntity = treeRoot.baseEntity;
        }
        else
        {
            return;
        }

        baseEntity.RemoveEntityToAttack(otherBaseEntity);

        if (baseEntity.EntitiesToAttackCount == 0)
        {
            baseEntity.StopAttacking();
            state = State.Run; 
        }
    }
}
