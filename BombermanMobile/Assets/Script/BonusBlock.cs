using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : MonoBehaviour
{
    private bool _isBonusTile = false;
    public void DestroyBlock()
    {
        GetBonus();
        print("bonus try spawn");
    }

    private GameObject GetBonus()
    {
        GameObject bonus = null;
        if(transform.parent.TryGetComponent<MapManager>(out MapManager component))
        {
            List<GameObject> bonuses = component.Bonuses;
            _isBonusTile = (Random.Range(0, 100) < 50);
            if (_isBonusTile)
            {
                if (bonuses != null && bonuses.Count > 0)
                {
                    bonus = bonuses[Random.Range(0, bonuses.Count)];

                    Instantiate(bonus, transform.position, Quaternion.identity, transform.parent);
                    print("bonus spawned");
                }
                else
                {
                    Debug.LogWarning("Bonuses is empty");
                }
            }
        }
        else
        {
            Debug.LogWarning("There's no map manager on parent");
        }
        return bonus;
    }
}
