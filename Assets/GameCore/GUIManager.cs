using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUIManager : MonoBehaviour
{
    public static GUIManager Instance { get; set; }
    public Action<bool> changeStateMenu;

    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject uiOnGame;
    [SerializeField] private Animator cameraAnimator;
    [SerializeField] private GameObject restartButton;

    private bool _stateMenu = true;
    private GlobalStateManager _globalManager;

    public bool MenuIsActive => _stateMenu;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _globalManager = GlobalStateManager.Instance;
    }


    public void OnClickPlay()
    {
        menuPanel.SetActive(false);
        uiOnGame.SetActive(true);
        cameraAnimator.SetTrigger("Start");

        _stateMenu = false;
        if(changeStateMenu != null)
        {
            changeStateMenu(_stateMenu);
        }
    }

    public void OnClickMenu()
    {
        menuPanel.SetActive(true);
        uiOnGame.SetActive(false);
        cameraAnimator.SetTrigger("Menu");

        _stateMenu = true;
        if (changeStateMenu != null)
        {
            changeStateMenu(_stateMenu);
        }

        if(_globalManager.ScoreP1 != 0 || _globalManager.ScoreP2 != 0)
        {
            restartButton.SetActive(true);
        }
        else
            restartButton.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    public void OnClicExit()
    {
        Application.Quit();
    }

}
