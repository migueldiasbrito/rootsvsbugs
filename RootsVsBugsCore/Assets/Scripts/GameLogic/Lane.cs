using System.Collections.Generic;
using UnityEngine;

public class Lane : MonoBehaviour
{
    public List<LaneSlot> slots;
    public float timeToUnlockSlot = 60;

    private bool hasLockedSlots = true;
    private float timeToNextSlot = 0;

    private void Update()
    {
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
}
