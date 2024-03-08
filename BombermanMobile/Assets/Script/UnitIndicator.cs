using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitIndicator : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.BattleStartEvent.AddListener(DeactivateIndicator);
        GetComponentInParents(gameObject).UnitDeathEvent.AddListener(DeactivateIndicator);
    }

    void DeactivateIndicator()
    {
        gameObject.SetActive(false);
    }

    private AIUnit GetComponentInParents(GameObject child)
    {
        AIUnit unit = child.GetComponentInParent<AIUnit>();
        if (unit == null)
        {
            if (child.transform.parent != null)
            {
                print("recursif");
                return GetComponentInParents(child.transform.parent.gameObject);
            }
            else
            {
                Debug.LogError("GetComponent failed");
                return null;
            }
        }
        else
        {
            return unit;
        }
    }
}
