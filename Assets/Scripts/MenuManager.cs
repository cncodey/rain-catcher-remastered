using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject optionsMenu;
    public GameObject credits;
    public PlayerMovement player;

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ToggleOptions()
    {
        menu.SetActive(!menu.activeSelf);
        optionsMenu.SetActive(!optionsMenu.activeSelf);
    }

    public void ToggleCredits()
    {
        credits.SetActive(!credits.activeSelf);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetSkin(int num)
    {
        PlayerPrefs.SetInt("Skin", num);
        player.SkinCheck();
    }
}
