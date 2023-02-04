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
        Plant plant = collision.gameObject.GetComponent<Plant>();

        if (plant == null) return;

        baseEntity.AddEntityToAttack(plant.baseEntity);

        if (state != State.Attack)
        {
            state = State.Attack;
            baseEntity.StartAttacking();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Plant plant = collision.gameObject.GetComponent<Plant>();

        if (plant == null) return;

        baseEntity.RemoveEntityToAttack(plant.baseEntity);

        if (baseEntity.EntitiesToAttackCount == 0)
        {
            baseEntity.StopAttacking();
            state = State.Run; 
        }
    }
}
