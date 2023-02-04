using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public List<Lane> lanes;
    public EnemySettings enemySettings;

    protected void AddPlantToLaneSlot(Player player, Plant plantPrefab, Lane lane, LaneSlot laneSlot)
    {
        lane.AddPlantToSlot(player, plantPrefab, laneSlot.transform.GetSiblingIndex());
        
    }
}
