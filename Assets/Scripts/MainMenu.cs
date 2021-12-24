using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    GameObject lastSelectedButton;

    void Awake()
    {
        lastSelectedButton = EventSystem.current.currentSelectedGameObject;
    }

    void Update()
    {
        // Workaround to prevent background mouse click to deselect a button.
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedButton);
        }

        else
        {
            lastSelectedButton = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void StartLocalMulti()
    {
        StaticGameData.Instance.GetComponent<StaticGameData>().isLocalMulti = true;
        StartGame();
    }

    public void StartVSCpu()
    {
        StaticGameData.Instance.GetComponent<StaticGameData>().isLocalMulti = false;
        StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
