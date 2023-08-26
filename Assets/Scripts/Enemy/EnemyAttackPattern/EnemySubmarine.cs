using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySubmarine : MonoBehaviour
{
    // shooting Harpoon and missils and drop bombs

    [Header("Shooting Harpoon Values")]
    public GameObject harpoonPrefab;
    public float aimTime;
    public float aimTimeOffset;
    private float curAimTimer;
    private bool aiming;
    public float fireRate;
    private float curTimer;

    [Header("Shooting Missile Values")]
    public GameObject missilePrefab;
    public float fireRateMissile;
    private float curTimerMissile;

    [Header("Shooting Bomb Values")]
    public GameObject bombPrefab;
    public float fireRateBomb;
    private float curTimerBomb;

    [Header("Harpoon objects")]
    public Transform shootingPoint;
    public GameObject harpoonWeapon;

    [Header("Missile objects")]
    public Transform shootingPointMissile;
    public GameObject missileWeapon;

    [Header("Bomb objects")]
    public Transform shootingPointBomb;
    public GameObject bombWeapon;

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

        if (curTimerMissile > 0)
        {
            curTimerMissile -= Time.deltaTime;
        }
        else if (curTimerMissile <= 0 && canShoot())
        {
                ShootMissile();
        }

        if (curTimerBomb > 0)
        {
            curTimerBomb -= Time.deltaTime;
        }
        else if (curTimerBomb <= 0 && canShoot())
        {
                ShootBomb();
        }

    }

    public void Shoot()
    {
        // instanciate harpoon prefab
        Instantiate(harpoonPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
        GetComponentInChildren<submarinAudio>().ShootHarpoon();
        curTimer = fireRate;
    }
    public void ShootMissile()
    {
        // instanciate harpoon prefab
        Instantiate(missilePrefab, shootingPointMissile.transform.position, missilePrefab.transform.rotation);
        GetComponentInChildren<submarinAudio>().ShootMissile();
        curTimerMissile = fireRateMissile;
    }
    public void ShootBomb()
    {
        // instanciate harpoon prefab
        Instantiate(bombPrefab, shootingPointBomb.transform.position, bombPrefab.transform.rotation);
        curTimerBomb = fireRateBomb;
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
