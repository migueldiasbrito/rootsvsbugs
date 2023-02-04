using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtoResourcesView : MonoBehaviour
{
    public ProtoSingleResourceView waterResourceView;
    public ProtoSingleResourceView mineralResourceView;
    public ProtoSingleResourceView seedsResourceView;

    public void UpdateResources(Resources resources)
    {
        waterResourceView.text.text = resources.Water.ToString();
        mineralResourceView.text.text = resources.Minerals.ToString();
        seedsResourceView.text.text = resources.Seeds.ToString();
    }
}
