using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillFeedManager : MonoBehaviour
{
    public static KillFeedManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void NewKillFeed(string victim, string killer)
    {
        print(killer + " killed " + victim);
    }
}
