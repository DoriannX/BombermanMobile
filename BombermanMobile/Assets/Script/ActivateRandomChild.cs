using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateRandomChild : MonoBehaviour
{
    private void Start()
    {
        foreach(GameObject meshObject in new Tools().GetAllChildren(gameObject))
        {
            meshObject.SetActive(false);
        }
        transform.GetChild(Random.Range(0, transform.childCount)).gameObject.SetActive(true);
    }
}
