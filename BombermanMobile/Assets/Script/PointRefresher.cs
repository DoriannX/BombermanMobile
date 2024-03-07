using MicahW.PointGrass;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointRefresher : MonoBehaviour
{
    void Start()
    {
        GetComponent<PointGrassRenderer>().BuildPoints();
    }

    private void Update()
    {
        GetComponent<PointGrassRenderer>().BuildPoints();
    }
}
