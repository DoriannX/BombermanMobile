using UnityEngine;

public class AdManager : MonoBehaviour
{

    private void Start()
    {
        GameManager.Instance.WinEvent.AddListener(OnWin);
        GameManager.Instance.LoseEvent.AddListener(OnLose);
    }
    private void OnWin()
    {
        loadInterstitial.Instance.LoadAd();
    }

    private void OnLose()
    {
        if(PlayerPrefs.GetInt("Lose") >= 1)
        {
            loadInterstitial.Instance.LoadAd();
            PlayerPrefs.SetInt("Lose", 0);
        }
    }
}
