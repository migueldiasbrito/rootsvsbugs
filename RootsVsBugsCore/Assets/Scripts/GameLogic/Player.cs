using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Resources _resources;
    public UnityEvent<Resources> resourcesUpdated;
    public Resources Resources { get => _resources; set { _resources = value; resourcesUpdated?.Invoke(value); } }

    private void Start()
    {
        resourcesUpdated?.Invoke(_resources);
    }
}
