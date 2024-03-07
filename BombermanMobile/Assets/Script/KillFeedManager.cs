using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFeedManager : MonoBehaviour
{
    public static KillFeedManager Instance;

    [SerializeField] private GameObject _hud;

    [SerializeField] private List<string> _deathLines = new List<string>();

    private void Awake()
    {
        Instance = this;
    }

    public void NewKillFeed(string victim, string killer)
    {
        print(killer + " killed " + victim);
        //Destroy(Instantiate(victim));
    }
}
