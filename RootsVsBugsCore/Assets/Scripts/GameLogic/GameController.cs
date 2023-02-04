using UnityEngine;

public class GameController : MonoBehaviour
{
    protected void AddPlantToLaneSlot(Player player, Plant plantPrefab, LaneSlot laneSlot)
    {
        if (laneSlot.State != LaneSlot.LaneSlotState.Free) return;

        if (player.Resources >= plantPrefab.cost)
        {
            laneSlot.State = LaneSlot.LaneSlotState.Occupied;
            Instantiate(plantPrefab, laneSlot.plantRoot);
            player.Resources -= plantPrefab.cost;
        }
    }
}
