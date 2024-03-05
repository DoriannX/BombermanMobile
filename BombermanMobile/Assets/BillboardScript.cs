using UnityEngine;

public class BillboardScript : MonoBehaviour
{
    void Update()
    {
        if (Camera.main)
        {
            transform.LookAt(Camera.main.transform.position);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, 0);
        }
    }
}
