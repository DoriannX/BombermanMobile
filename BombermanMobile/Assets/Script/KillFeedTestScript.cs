using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillFeedTestScript : MonoBehaviour
{
    private RectTransform _rectTransform;
    private TextMeshProUGUI _text;
    public Vector3 GoingToPos = Vector3.zero;
    private bool _goingUp = false;

    [SerializeField] private float _timeBeforeDestroy = 3;
    [SerializeField] private float _timeBeforeGoingUp = 1;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _text = GetComponent<TextMeshProUGUI>();
        KillFeedManager.Instance.NewKillFeedEvent.AddListener(ActivateGoingUp);
        KillFeedManager.Instance.NewKillFeedEvent.AddListener(LittleBoostUp);
        StartCoroutine(WaitForDisappear(_timeBeforeDestroy));
    }

    private void LittleBoostUp()
    {
        GoingToPos += Vector3.up * 125;
    }

    private void ActivateGoingUp()
    {
        _goingUp = true;
    }

    void Update()
    {
       
    }

    private IEnumerator WaitForDisappear(float waitTime)
    {
        float currentTime = 0;
        while (currentTime <= _timeBeforeDestroy) 
        {
            transform.position = Vector3.Lerp(transform.position, GoingToPos, Time.unscaledDeltaTime * 6);
            if (_goingUp)
            {
                GoingToPos += Vector3.up * Time.unscaledDeltaTime * 100;
            }
            else
            {
                if (currentTime > _timeBeforeGoingUp)
                {
                    ActivateGoingUp();
                }
            }

            if (_timeBeforeDestroy - currentTime <= 1)
            {
                _text.color += new Color(0, 0, 0, -Time.unscaledDeltaTime);
            }
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }
}
