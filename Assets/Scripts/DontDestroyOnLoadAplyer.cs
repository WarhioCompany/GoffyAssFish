using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadAplyer : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
