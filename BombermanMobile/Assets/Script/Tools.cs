using System.Collections.Generic;
using UnityEngine;

public class Tools
{
    public List<GameObject> GetAllChildren(GameObject objectToGatherChild)
    {
        List<GameObject> children = new List<GameObject>();

        foreach(Transform child in objectToGatherChild.transform)
        {
            children.Add(child.gameObject);
        }

        return children;
    }
}
