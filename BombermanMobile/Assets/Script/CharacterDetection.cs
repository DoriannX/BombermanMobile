using System.Collections.Generic;
using UnityEngine;

public class CharacterDetection : MonoBehaviour
{
    [HideInInspector] public List<GameObject> DetectedPlayers = new List<GameObject>();
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            DetectedPlayers.Add(other.gameObject);
            print("player detected");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print(other.name);
            DetectedPlayers.Remove(other.gameObject);
        }
    }
}
