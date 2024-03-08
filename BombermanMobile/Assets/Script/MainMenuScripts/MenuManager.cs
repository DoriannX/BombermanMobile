using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainMenu;
    [SerializeField] private GameObject _optionMenu;

    public void StartGame()
    {

        SceneTransition.Instance.StartTransition();
    }

    private void Start()
    {
        if (_mainMenu && _optionMenu)
        {
            _mainMenu.SetActive(true);
            _optionMenu.SetActive(false);
        }
        else
            Debug.LogError("One of the menu buttons is empty");
    }

    public void OptionMenu()
    {
        if (_mainMenu && _optionMenu)
        {
            _mainMenu.SetActive(!_mainMenu.activeSelf);
            _optionMenu.SetActive(!_optionMenu.activeSelf);
        }
        else
            Debug.LogError("One of the menu is empty");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
