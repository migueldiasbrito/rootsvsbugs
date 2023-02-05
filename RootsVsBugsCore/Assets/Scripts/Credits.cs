using UnityEngine;
using UnityEngine.Events;

public class Credits : MonoBehaviour
{
    public UnityEvent canExitCredits;

    public void CanExitCredits()
    {
        canExitCredits?.Invoke();
    }
}
