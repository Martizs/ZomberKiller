using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using TMPro;
using UnityEngine;

public class EndGameHandler : MonoBehaviour
{
    [SerializeField]
    Canvas gameOverCanvas;

    [SerializeField]
    Zombers zombers;

    bool gameOver = false;
    public bool GameOver
    {
        get { return gameOver; }
    }

    private void Start()
    {
        gameOverCanvas.enabled = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void HandleEndGame(bool isDead)
    {
        TextMeshProUGUI textMeshPro = gameOverCanvas.GetComponentInChildren<TextMeshProUGUI>();
        if (isDead)
        {
            textMeshPro.text = "You dead";
            textMeshPro.color = Color.red;
        }
        else
        {
            textMeshPro.text = "You win!!!!";
            textMeshPro.color = Color.green;
        }

        zombers.BroadcastGameOver();

        gameOver = true;
        gameOverCanvas.enabled = true;
        Time.timeScale = 0;
        FindObjectOfType<WeaponSwitcher>().enabled = false;
        FindObjectOfType<FirstPersonController>().enabled = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HandleEndGame()
    {
        HandleEndGame(true);
    }
}
