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

    [Serializable]
    public class WaveSettings
    {
        public List<SpawnEnemySettings> spawnEnemySettings;
        public float minSpawnTime;
        public float maxSpawnTime;
        public float waveDuration;
    }

    public List<WaveSettings> waveSettings;

    private int currentLevel;
    private List<float> normalizedEnemyProbability;
    private WaveSettings CurrentWaveSetting => waveSettings[currentLevel];


    private void Awake()
    {
        LoadWave();
    }

    private void LoadWave()
    {
        float enemyFactorSum = 0;
        foreach (SpawnEnemySettings setting in CurrentWaveSetting.spawnEnemySettings)
        {
            enemyFactorSum += setting.factor;
        }

        float cummulativeProbability = 0;
        normalizedEnemyProbability = new();
        foreach (SpawnEnemySettings setting in CurrentWaveSetting.spawnEnemySettings)
        {
            cummulativeProbability += setting.factor / enemyFactorSum;
            normalizedEnemyProbability.Add(cummulativeProbability);
        }

        if (currentLevel + 1 < waveSettings.Count)
        {
            StartCoroutine(LoadNewLevel());
        }
    }

    private IEnumerator LoadNewLevel()
    {
        yield return new WaitForSeconds(CurrentWaveSetting.waveDuration);
        currentLevel++;
        LoadWave();
    }

    public Bug GetNextBug()
    {
        float rng = Random.Range(0f, 1f);
        
        for (int i = 0; i < normalizedEnemyProbability.Count; i++)
        {
            if (normalizedEnemyProbability[i] < rng) continue;

            return CurrentWaveSetting.spawnEnemySettings[i].enemy;
        }

        return CurrentWaveSetting.spawnEnemySettings[^1].enemy;
    }

    public float GetNextBugWaitTime()
    {
        return Random.Range(CurrentWaveSetting.minSpawnTime, CurrentWaveSetting.maxSpawnTime);
    }
}
