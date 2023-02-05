using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProtoTreeView : MonoBehaviour
{
    public RectTransform tree;
    public float timeToWin;

    private float currentTime = 0;

    void Update()
    {
        currentTime += Time.deltaTime;
        Vector2 sizeDelta = tree.sizeDelta;
        sizeDelta.y = 100 + 980 * currentTime / timeToWin;
        tree.sizeDelta = sizeDelta;

        if (currentTime >= timeToWin)
        {
            SceneManager.LoadScene("Victory");
        }
    }
}
