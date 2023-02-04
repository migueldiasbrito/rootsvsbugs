using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySettings : MonoBehaviour
{
    [Serializable]
    public class SpawnEnemySettings
    {
        public Bug enemy;
        public int factor;
    }

    public List<SpawnEnemySettings> spawnEnemySettings;
    public float minSpawnTime;
    public float maxSpawnTime;

    private List<float> normalizedEnemyProbability;

    private void Awake()
    {
        float enemyFactorSum = 0;
        foreach (SpawnEnemySettings setting in spawnEnemySettings)
        {
            enemyFactorSum += setting.factor;
        }

        float cummulativeProbability = 0;
        normalizedEnemyProbability = new();
        foreach (SpawnEnemySettings setting in spawnEnemySettings)
        {
            cummulativeProbability += setting.factor / enemyFactorSum;
            normalizedEnemyProbability.Add(cummulativeProbability);
        }
    }

    public Bug GetNextBug()
    {
        float rng = Random.Range(0f, 1f);
        
        for (int i = 0; i < normalizedEnemyProbability.Count; i++)
        {
            if (normalizedEnemyProbability[i] < rng) continue;

            return spawnEnemySettings[i].enemy;
        }

        return spawnEnemySettings[^1].enemy;
    }

    public float GetNextBugWaitTime()
    {
        return Random.Range(minSpawnTime, maxSpawnTime);
    }
}
