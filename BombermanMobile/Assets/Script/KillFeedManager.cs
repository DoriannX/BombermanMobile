using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillFeedManager : MonoBehaviour
{
    public static KillFeedManager Instance;

    [SerializeField] private GameObject _hud;
    [SerializeField] private TextMeshProUGUI _killfeedText;

    [SerializeField] private List<string> _deathLines = new List<string>();

    private void Awake()
    {
        Instance = this;
    }

    public void NewKillFeed(string victim, string killer)
    {
        print(killer + " killed " + victim);
        TextMeshProUGUI newFeed = Instantiate(_killfeedText, _killfeedText.transform.position, Quaternion.identity, _hud.transform);
        newFeed.gameObject.SetActive(true);
        newFeed.text = killer + " killed " + victim;
        //Destroy(Instantiate(victim));
    }
}
