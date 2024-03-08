using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class KillFeedManager : MonoBehaviour
{
    public static KillFeedManager Instance;

    [SerializeField] private GameObject _hud;
    [SerializeField] private TextMeshProUGUI _killfeedText;

    [SerializeField] private List<string> _deathLines = new List<string>();
    [SerializeField] private List<string> _suicideLines = new List<string>();

    [HideInInspector] public UnityEvent NewKillFeedEvent;

    private void Awake()
    {
        Instance = this;
    }

    public void NewKillFeed(string victim, string killer, Unit.Team victimTeam, string forceString = null)
    {
        NewKillFeedEvent.Invoke();
        TextMeshProUGUI newFeed = Instantiate(_killfeedText, _killfeedText.transform.position, Quaternion.identity, _hud.transform);
        newFeed.gameObject.SetActive(true);

        string feedText = "";
        if (victim == killer)
        {
            feedText = _suicideLines[UnityEngine.Random.Range(0, _suicideLines.Count)];
        }
        else
        {
            feedText =_deathLines[UnityEngine.Random.Range(0, _deathLines.Count)];
        }

        if (forceString == null)
        {
            if (victimTeam == Unit.Team.Player)
            {
                feedText = feedText.Replace("[KILLER]", "<color=red>" + killer + "</color>");
                feedText = feedText.Replace("[VICTIM]", "<color=blue>" + victim + "</color>");
            }
            else
            {
                feedText = feedText.Replace("[KILLER]", "<color=blue>" + killer + "</color>");
                feedText = feedText.Replace("[VICTIM]", "<color=red>" + victim + "</color>");
            }
        }
        else
        {
            feedText = forceString;
        }

        newFeed.text = feedText;
        newFeed.GetComponent<KillFeedTestScript>().GoingToPos = _killfeedText.transform.position;
        newFeed.transform.position += Vector3.left * 100;
    }
}
