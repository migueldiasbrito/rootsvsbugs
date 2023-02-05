using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SinglePlayerGameController : GameController
{
    public List<PlantSelector> plantSelectors;
    private PlantSelector selectedPlantSelected = null;
    public Player player;

    public Camera sceneCamera;
    public Transform HealthBarsHolder;

    private void Start()
    {
        foreach(PlantSelector plantSelector in plantSelectors)
        {
            plantSelector.OnClicked += PlantSelectorSelected;
        }

        foreach (Lane lane in lanes)
        {
            lane.SetUiOptions(sceneCamera, HealthBarsHolder);
            lane.SetEnemySettings(enemySettings);

            lane.LaneDied += LaneDied;
        }
    }

    private void LaneDied()
    {
        foreach (Lane lane in lanes)
        {
            if (!lane.isDead) continue;
        }

        MainMenuController.MainMenuState = 1;
        GoToMainMenu();
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

        if (player.Resources >= plantSelector.plantPrefab.cost)
        {
            selectedPlantSelected = plantSelector;
            selectedPlantSelected.Selected = true;
        }
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
                Lane lane = laneSlot.GetComponentInParent<Lane>();
                AddPlantToLaneSlot(player, selectedPlantSelected.plantPrefab, lane, laneSlot);
            }

            if (player.Resources < selectedPlantSelected.plantPrefab.cost)
            {
                selectedPlantSelected.Selected = false;
                selectedPlantSelected = null;
            }
        }
    }
}
