using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public List<LaneSlot> slots;
    public float timeToUnlockSlot = 60;
    public Transform enemySpawnPoint;
    public TreeRoot treeRoot;

    private bool hasLockedSlots = true;
    private float timeToNextSlot = 0;

    private EnemySettings enemySettings;
    private Coroutine enemySpawnRoutine;

    private Camera sceneCamera;
    private Transform healthBarsHolder;

    private bool isDead = false;
    private List<Bug> bugs = new();
    private List<Plant> plants = new();

    private void Start()
    {
        treeRoot.baseEntity.Died += GameOver;
    }

    private void GameOver()
    {
        isDead = true;
        StopCoroutine(enemySpawnRoutine);
        
        foreach (Bug bug in bugs)
        {
            Destroy(bug.gameObject);
        }

        foreach (Plant plant in plants)
        {
            Destroy(plant.gameObject);
        }
    }

    private void Update()
    {
        if (isDead) return;

        UpdateLockedSlots();
    }

    private void UpdateLockedSlots()
    {
        if (!hasLockedSlots) return;

        timeToNextSlot -= Time.deltaTime;

        if (timeToNextSlot <= 0)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (slots[i].State != LaneSlot.LaneSlotState.Locked) continue;

                slots[i].State = LaneSlot.LaneSlotState.Free;
                hasLockedSlots = i < slots.Count - 1;
                break;
            }
            timeToNextSlot = timeToUnlockSlot;
        }
    }

    public void SetUiOptions(Camera camera, Transform healthBarsHolder)
    {
        sceneCamera = camera;
        this.healthBarsHolder = healthBarsHolder;

        treeRoot.baseEntity.SetUiOptions(camera, healthBarsHolder);
    }

    public void AddPlantToSlot(Player player, Plant plantPrefab, int slot)
    {
        LaneSlot laneSlot = slots[slot];
        if (laneSlot.State != LaneSlot.LaneSlotState.Free) return;

        if (player.Resources >= plantPrefab.cost)
        {
            laneSlot.State = LaneSlot.LaneSlotState.Occupied;
            Plant newPlant = Instantiate(plantPrefab, laneSlot.plantRoot);
            newPlant.baseEntity.SetUiOptions(sceneCamera, healthBarsHolder);
            player.Resources -= plantPrefab.cost;

            plants.Add(newPlant);
            newPlant.baseEntity.Died += () => plants.Remove(newPlant);
        }
    }

    public void SetEnemySettings(EnemySettings enemySettings)
    {
        this.enemySettings = enemySettings;

        if (enemySpawnRoutine != null) StopCoroutine(enemySpawnRoutine);

        enemySpawnRoutine = StartCoroutine(UpdateEnemySpawn());
    }

    private IEnumerator UpdateEnemySpawn()
    {
        while (true)
        {
            Bug bug = enemySettings.GetNextBug();
            float waitTimeToNextBug = enemySettings.GetNextBugWaitTime();

            yield return new WaitForSeconds(waitTimeToNextBug);
            Bug newBug = Instantiate(bug, enemySpawnPoint);
            newBug.baseEntity.SetUiOptions(sceneCamera, healthBarsHolder);
            
            bugs.Add(newBug);
            newBug.baseEntity.Died += () => bugs.Remove(newBug);
        }
    }
}
