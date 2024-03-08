using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneTransition : MonoBehaviour
{

    [SerializeField] Image _transitionImage;
    float _i = 0;
    bool _startedInTransition = false;
    bool _startedOutTransition = false;
    public static SceneTransition Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            _transitionImage.color = new Color(0, 0, 0, 255);
            StartTransition();
        }
        else
        {
            _transitionImage.color = new Color(0, 0, 0, 0);
        }
    }

    public void StartTransition()
    {
        _i = 0;
        _startedInTransition = true;
    }
    private void InTransition()
    {
        if (_startedInTransition)
        {
            _transitionImage.color = new Color(0, 0, 0, _i);
            _i += Time.deltaTime;
            if (_i >= 1)
            {
                _startedInTransition = false;
                print("started out transition");
            }
        }
    }

    private void StartOutTransition()
    {
        _i = 255;
        _startedOutTransition = true;
        print("Started Out Transition");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }



    private void OutTransition()
    {
        if (_startedOutTransition)
        {
            _transitionImage.color = new Color(0, 0, 0, _i);
            _i -= Time.deltaTime;
            if (_i >= 255)
            {
                _startedOutTransition = false;
            }
        }
    }

    private void Update()
    {
        InTransition();
    }

    
}
