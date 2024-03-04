using UnityEngine;

public class bonus : MonoBehaviour
{
    private void Awake()
    {
        Invoke("GetBonus", 1);
    }

    private void GetBonus()
    {
        print("jai pose un bonus sur toi mec");
        Destroy(gameObject);
    }
}
