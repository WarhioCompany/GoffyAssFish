using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoat : MonoBehaviour
{
    // shooting the harpoon

    // 1. Harpoon always facing the Player

    // 2. Harpoon shoot

    // 3. Harpoon reload + cooldown

    [Header("Shooting Values")]
    public GameObject harpoonPrefab;

    [Header("Harpoon objects")]
    public Transform shootingPoint;
    public GameObject harpoonWeapon;

    public float fireRate;

    private void Update()
    {
        
    }

    public void canShoot()
    {
        // if: not cc`d, cooldown up (firerate)
    }
}
