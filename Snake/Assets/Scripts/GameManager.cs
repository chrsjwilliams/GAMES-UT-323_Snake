using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    SwipeManager swipeManager;
    [SerializeField] CanvasGroup startMenu;
    [SerializeField] CanvasGroup quitButton;

    [SerializeField] Player player;

    // Start is called before the first frame update
    void Start()
    {
        swipeManager = GetComponent<SwipeManager>();
        ReturnToMenu();
    }

    public void StartGame()
    {
        swipeManager.EnableSwipes(true);

        startMenu.alpha = 0;
        startMenu.blocksRaycasts = false;
        startMenu.interactable = false;

        quitButton.alpha = 1;
        quitButton.blocksRaycasts = true;
        quitButton.interactable = true;

    }

    public void ReturnToMenu()
    {
        swipeManager.EnableSwipes(false);

        player.RestartGame();

        player.moveDirection = Vector2.zero;

        startMenu.alpha = 1;
        startMenu.blocksRaycasts = true;
        startMenu.interactable = true;

        quitButton.alpha = 0;
        quitButton.blocksRaycasts = false;
        quitButton.interactable = false;
    }
}
