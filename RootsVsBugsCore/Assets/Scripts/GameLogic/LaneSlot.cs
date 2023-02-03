using UnityEngine;
using UnityEngine.Events;

public class LaneSlot : MonoBehaviour
{
    public enum LaneSlotState { Locked, Free, Occupied }

    private LaneSlotState _state = LaneSlotState.Locked;
    public UnityEvent<LaneSlotState> slotStateUpdated;
    public LaneSlotState State { get => _state; set { _state = value; slotStateUpdated?.Invoke(value); } }
}
