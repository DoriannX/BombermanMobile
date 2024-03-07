using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIndicator : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.BattleStartEvent.AddListener(DeactivateIndicator);
    }

    void DeactivateIndicator()
    {
        gameObject.SetActive(false);
    }
}
