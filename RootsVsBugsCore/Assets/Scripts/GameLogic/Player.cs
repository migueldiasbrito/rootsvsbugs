using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Resources _resources;
    public UnityEvent<Resources> resourcesUpdated;
    public Resources Resources { get => _resources; set { _resources = value; resourcesUpdated?.Invoke(value); } }

    public float seedIncrease = 5;

    private void Start()
    {
        resourcesUpdated?.Invoke(_resources);

        StartCoroutine(SeedIncreaseRoutine());
    }

    private IEnumerator SeedIncreaseRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(seedIncrease);
            Resources += new Resources { Seeds = 1 };
        }
    }
}
