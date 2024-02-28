using UnityEngine;
using UnityEngine.UI;

public class DeactivateHorizontalLayout : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<HorizontalLayoutGroup>().enabled = false;
    }
}
