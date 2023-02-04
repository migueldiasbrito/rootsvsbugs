using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public List<PlantSelector> plantSelectors;
    private PlantSelector selectedPlantSelected = null;

    private void Start()
    {
        foreach(PlantSelector plantSelector in plantSelectors)
        {
            plantSelector.OnClicked += PlantSelectorSelected;
        }
    }

    private void Update()
    {
        ReadMouse();
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void PlantSelectorSelected(PlantSelector plantSelector)
    {
        if (selectedPlantSelected != null)
        {
            selectedPlantSelected.Selected = false;
        }

        selectedPlantSelected = plantSelector;
        selectedPlantSelected.Selected = true;
    }

    public void ReadMouse()
    {
        if (selectedPlantSelected == null || !Mouse.current.leftButton.wasPressedThisFrame) return;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 ray = mousePosition;
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero, 0f);

        
        if (hit)
        {
            LaneSlot laneSlot = hit.collider.GetComponent<LaneSlot>();
            if (laneSlot != null)
            {
                AddPlantToLaneSlot(selectedPlantSelected.plantPrefab, laneSlot);
            }
        }
    }

    private void AddPlantToLaneSlot(Plant plantPrefab, LaneSlot laneSlot)
    {
        if (laneSlot.State != LaneSlot.LaneSlotState.Free) return;
        laneSlot.State = LaneSlot.LaneSlotState.Occupied;
        Instantiate(plantPrefab, laneSlot.plantRoot);
    }
}
