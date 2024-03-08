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
        if (_transitionImage)
        {
            if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                _transitionImage.color = new Color(0, 0, 0, 1);
                StartOutTransition();
            }
            else
            {
                _transitionImage.color = new Color(0, 0, 0, 0);
            }
        }
        else
        {
            Debug.LogError("transition image is empty");
        }
    }

    public void StartTransition()
    {
        _i = 0;
        _startedInTransition = true;
    }
    private void InTransition()
    {
        if (_transitionImage)
        {
            if (_startedInTransition)
            {
                _transitionImage.color = new Color(0, 0, 0, _i);
                _i += Time.deltaTime;
                if (_i >= 1)
                {
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
                    _startedInTransition = false;
                }
            }
        }
        else
        {
            Debug.LogError("transition image is empty");
        }
    }

    private void StartOutTransition()
    {
        _i = 1;
        _startedOutTransition = true;
    }



    private void OutTransition()
    {
        if (_startedOutTransition)
        {
            _transitionImage.color = new Color(0, 0, 0, _i);
            _i -= Time.deltaTime;
            if (_i <= 0 )
            {
                _startedOutTransition = false;
            }
        }
    }

    private void Update()
    {
        InTransition();
        OutTransition();
    }

    
}
