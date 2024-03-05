using System.Collections.Generic;
using UnityEngine;

public class attackBonus : MonoBehaviour
{
    private void Start()
    {
        Invoke("GetBonus", 1);
    }
    private void GetBonus()
    {
        List<GameObject> allyUnits = UnitManager.Instance.AllyUnits;
        if(allyUnits.Count > 0)
        {
            foreach (GameObject unit in allyUnits)
            {
                if (unit != null && unit.TryGetComponent<AIUnit>(out AIUnit component))
                {
                    component.BombDamage *= 1.25f;
                    print("damage is higher now" + component.BombDamage);
                }
                else
                {
                    Debug.LogWarning("unit is null or there's no AIUnit");
                }
            }
        }
        else
        {
            Debug.LogWarning("allyUnit is null");
        }
        Destroy(gameObject);
    }
}
