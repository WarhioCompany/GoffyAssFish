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
    public float aimTime;
    public float aimTimeOffset;
    private float curAimTimer;
    private bool aiming;

    [Header("Harpoon objects")]
    public Transform shootingPoint;
    public GameObject harpoonWeapon;

    public LayerMask waterLayer;
    public float fireRate;
    private float curTimer;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (!GetComponent<EnemyMovement>().active) return;

        if (curTimer > 0)
        {
            curTimer -= Time.deltaTime;
        }
        else if (curTimer <= 0 && canShoot())
        {
            // set aimtimer, when up: shoot
            if (!aiming)
            {
                aiming = true;
                curAimTimer = Random.Range(aimTime - aimTimeOffset, aimTime + aimTimeOffset);
            }
            if (aiming && curAimTimer > 0)
            {
                curAimTimer -= Time.deltaTime;

                Vector3 directionToTarget = target.position - harpoonWeapon.transform.position;
                float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
                harpoonWeapon.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else if (curAimTimer <= 0)
            {
                aiming = false;
                Shoot();
            }
        }
    }

    private void LateUpdate()
    {
        RaycastHit[] col = Physics.RaycastAll(new Vector3(transform.position.x, transform.position.y + 100, transform.position.z), Vector3.down);
        foreach (RaycastHit hit in col)
        {
            if (hit.collider.gameObject.layer == waterLayer)
            {
                Debug.Log("Found Water");
                transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
            }
        }
    }

    public void Shoot()
    {
        // instanciate harpoon prefab
        Instantiate(harpoonPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
        curTimer = fireRate;
    }

    public bool canShoot()
    {
        // if: not cc`d, cooldown up (firerate)
        if (GetComponent<EnemyMovement>().concussedTimer <= 0 && GetComponent<EnemyMovement>().active)
        {
            return true;
        }
        return false;
    }
}
