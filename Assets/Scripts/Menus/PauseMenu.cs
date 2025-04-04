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
        SaveManager.Instance.Save();
    }

    public void QuitGame()
    {
        // Go back to the main menu
        SceneManager.LoadScene("MainMenu");
    }
}
