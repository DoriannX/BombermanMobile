using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Unit
{
    public static GameManager Instance;
    private bool _battleStarted = false; public bool BattleStarted { get { return _battleStarted; } }
    [HideInInspector] public bool GameOver = false;

    [HideInInspector] public UnityEvent BattleStartEvent;
    [SerializeField] private GameObject _buttonStart = null;
    [SerializeField] private GameObject _panel = null;
    [SerializeField] private TextMeshProUGUI _textSkip = null;
    [SerializeField] private GameObject _skipButton = null;
    [SerializeField] private GameObject _endMenu = null;
    [SerializeField] private TextMeshProUGUI _winLoseText = null;
    [SerializeField] private GameObject _unitCount = null;
    public UnityEvent LoseEvent;
    public UnityEvent WinEvent;

    public float MaximumGoatUnits = 8;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        UnitManager.Instance.SpawnUnit(Team.Player, 1, new Vector3(0, 0, -14.3000002f), UnitManager.Instance.GetUnitToSpawn(Type.Goass));
        UnitManager.Instance.SpawnUnit(Team.Ennemy, 1, new Vector3(0, 0, 14.3000002f), UnitManager.Instance.GetUnitToSpawn(Type.Goass));
        EnableHUD();
    }

    private void EnableHUD()
    {
        if (_buttonStart)
            switchStateEnable(_buttonStart, true, true);
        else
            Debug.Log("_buttonStart is empty");

        if (_endMenu)
            switchStateEnable(_endMenu, true, false);
        else
            Debug.LogError("_endMenu is empty");

        if (_skipButton)
            switchStateEnable(_skipButton, true, false);
        else
            Debug.LogError("_skipButton is empty");

        if (_panel)
            switchStateEnable(_panel, true, true);
        else
            Debug.LogError("_panel is empty");

        if (_unitCount)
            switchStateEnable(_unitCount, true, true);
        else
            Debug.LogError("_unitCount is empty");
    }

    private void switchStateEnable(GameObject objectToDisable, bool switchState = false, bool state = false)
    {
        if (switchState)
            objectToDisable.SetActive(state);
        else
            objectToDisable.SetActive(!objectToDisable.activeSelf);
    }

    public void StartBattle()
    {
        switchStateEnable(_buttonStart);
        _battleStarted = true;
        BattleStartEvent.Invoke();
        loadBanner.Instance.LoadBanner();
        switchStateEnable(_skipButton);
        switchStateEnable(_panel);
    }

    private void Update()
    {
        if (_battleStarted && UnitManager.Instance.EnemiesUnits.Count == 0)
        {
            Win();
        }
    }

    public void Lose()
    {
        if (!GameOver)
        {
            if (_winLoseText)
            {
                _winLoseText.text = "You Lose";
            }
            else
            {
                Debug.LogError("WinLoseText is empty");
            }
            switchStateEnable(_endMenu);

            switchStateEnable(_skipButton);

            print("you lose");
            LoseEvent.Invoke();
            PlayerPrefs.SetInt("Lose", PlayerPrefs.GetInt("Lose") + 1);
            Time.timeScale = 0f;
            
        }
        GameOver = true;
    }

    public void Win()
    {
        if (!GameOver)
        {
            if (_winLoseText)
            {
                _winLoseText.text = "You Win";
            }
            else
            {
                Debug.LogError("WinLoseText is empty");
            }
            switchStateEnable(_endMenu);
            switchStateEnable(_skipButton);

            print("you win");
            WinEvent.Invoke();
            PlayerPrefs.SetInt("Lose", 0);
            Time.timeScale = 0f;
        }
        GameOver = true;
    }

    public void Skip()
    {
        if(_textSkip != null)
        {
            switch (Time.timeScale)
            {
                case 1.0f:
                    Time.timeScale = 2.0f;
                    _textSkip.text = "2x";
                    break;
                case 2.0f:
                    Time.timeScale = 3.0f;
                    _textSkip.text = "3x";
                    break;
                case 3.0f:
                    Time.timeScale = 4.0f;
                    _textSkip.text = "4x";
                    break;
                default:
                    Time.timeScale = 1.0f;
                    _textSkip.text = "1x";
                    break;
            }
        }
        else
        {
            Debug.LogError("Text skip is empty");
        }
    }

    public void RestartGame()
    {
        new Tools().SetScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartMenu()
    {
        new Tools().SetScene(0);
    }
}
