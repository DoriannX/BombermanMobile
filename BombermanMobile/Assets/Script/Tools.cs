using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public void SetScene(int scene)
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(scene);
    }
}
