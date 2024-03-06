using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUDUnitCountScript : MonoBehaviour
{
    [SerializeField] private Unit.Team _team;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (_team == Unit.Team.Player)
            _text.text = UnitManager.Instance.AllyUnits.Count.ToString();
        else
            _text.text = UnitManager.Instance.EnemiesUnits.Count.ToString();

    }
}
