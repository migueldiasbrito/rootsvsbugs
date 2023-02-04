using UnityEngine;

public class HealthDisplayView : MonoBehaviour
{
    public RectTransform healthBar;
    private float width;

    private void Start()
    {
        width = healthBar.sizeDelta.x;
    }

    public void UpdateHealthBar(int current, int total)
    {
        float healthPercentage = (float)current / (float)total;
        float healthBarWidth = healthPercentage * width;

        Vector2 sizeDelta = healthBar.sizeDelta;
        sizeDelta.x = healthBarWidth;
        healthBar.sizeDelta = sizeDelta;
    }
}
