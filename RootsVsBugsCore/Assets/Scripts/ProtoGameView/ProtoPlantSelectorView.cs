using UnityEngine;
using UnityEngine.UI;

public class ProtoPlantSelectorView : MonoBehaviour
{
    public Animator animator;

    public void OnSelected(bool selected)
    {
        animator.SetBool("selected", selected);
    }
}
