using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class waterSpawner : MonoBehaviour
{
    // x = width * col
    // y = height
    // z = startZ + (width * row)

    public GameObject waterTop;
    public GameObject waterBottom;

    public float startZ;
    public float waterWidth;
    public float yHeight;
    public float col;
    public float row;

    private float startX;

    private void Start()
    {
        startX = -1 * waterWidth * (col / 2) + waterWidth/2;
    }

    private void Update()
    {
        for (int i = 0; i < col; i++)
        {
            // in each column

            for(int j = 0; j < row; j++)
            {
                // in each row
                Vector3 pos = new Vector3(startX + (i * waterWidth), yHeight, startZ + (waterWidth * j));
                Instantiate(waterTop, pos, waterTop.transform.rotation);
                Instantiate(waterBottom, pos, waterBottom.transform.rotation);
            }
        }
    }
}
