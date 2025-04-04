using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputManager input;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject inventoryMenu;
    [SerializeField] private GameObject usedItemsMenu;
    [SerializeField] private PlayerInventory inventory;
    [SerializeField] private PlayerController playerController;

    private bool _paused = false;

    private void Update()
    {
        PauseGame();
    }

    // Handles pausing
    private void PauseGame()
    {
        if (input.PauseGame && !_paused)
        {
            _paused = true;

            playerController.enabled = false;

            usedItemsMenu.SetActive(false);
            pauseMenu.SetActive(true);
            input.ShowCursor(true);

            if (inventoryMenu.activeSelf)
            {
                inventory.ForceCloseInventory();
            }

            inventory.enabled = false;
            input.PauseGame = false;
        }
    }

    // Handles resuming gameplay
    public void ResumeGame()
    {
        usedItemsMenu.SetActive(true);
        pauseMenu.SetActive(false);
        playerController.enabled = true;
        input.ShowCursor(false);

        inventory.enabled = true;

        _paused = false;
    }

    public void SaveGame()
    {
        ItemSlotType[] inventorySlots = new ItemSlotType[inventory.Slots.Length];

        // Convert the main slots into easier to "interpret" array
        for (int i = 0; i < inventory.Slots.Length; i++)
        {
            // Fetch the icon
            GameObject icon = inventory.Slots[i].GetChild(0).gameObject;

            // If the icon is disabled, then it's empty
            if (!icon.activeSelf)
            {
                inventorySlots[i] = ItemSlotType.Null;
                continue;
            }

            // Fetch the tooltip
            Transform tooltip = inventory.Slots[i].GetChild(1);

            // Check if the slot contains a battery or a magnet
            // Again, hacky, but works
            if (tooltip.GetComponentInChildren<TMP_Text>().text == "Battery")
            {
                inventorySlots[i] = ItemSlotType.Battery;
            }
            else
            {
                inventorySlots[i] = ItemSlotType.Magnet;
            }
        }

        // Prepare main data to be saved
        PlayerData playerData = new()
        {
            batteriesUsed = inventory.UsedBatteries,
            magnetsUsed = inventory.UsedMagnets,
            playerPosition = playerController.transform.position,
            playerRotation = playerController.transform.rotation,
            cameraRotation = playerController.CameraRotation,
            itemSlots = inventorySlots
        };

        // Save the supplied data
        SaveManager.Instance.Save(playerData);
    }

    public void QuitGame()
    {
        // Go back to the main menu
        SceneManager.LoadScene("MainMenu");
    }
}
