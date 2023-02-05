using System;
using System.Collections.Generic;
using UnityEngine;

public class ProtoLaneSlotView : MonoBehaviour
{
    [Serializable]
    public class ProtoLaneSlotViewOptions
    {
        public LaneSlot.LaneSlotState state;
        public Sprite sprite;
        public Color color;
    }

    public SpriteRenderer spriteRenderer;
    public List<ProtoLaneSlotViewOptions> protoLaneSlotViewOptions;

    public void UpdateLaeneSlotState(LaneSlot.LaneSlotState state)
    {
        foreach (ProtoLaneSlotViewOptions option in protoLaneSlotViewOptions)
        {
            if (option.state != state) continue;

            spriteRenderer.sprite = option.sprite;
            spriteRenderer.color = option.color;
            break;
        }
    }
}
