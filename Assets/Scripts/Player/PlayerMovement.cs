using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Tentacle Things")]
    public float range;
    public List<GameObject> tentacles;
    public List<Vector3> tentOrgRot;

    private PlayerInputActions playerInput;
    private InputAction mousePos;
    public float deadzone;
    private int selected; // selected tentacle for shooting
    Vector3 targetPos;
    Vector3 startPos;

    [Header("States")]
    public bool Attached;
    public bool prepareing;
    public bool pending; // pending impact of a spike
    public bool negativ;

    [Header("Cooldown")]
    public float cooldown;
    public float curCooldown;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, deadzone);

        try
        {
            Vector2 mouseScreenPosition = mousePos.ReadValue<Vector2>();
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
            //mouseWorldPosition.z = 0;

            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(mouseWorldPosition, 0.1f);
        } catch { }

        //foreach (var t in tentacles)
        //{
        //    Gizmos.color = Color.yellow;
        //    Gizmos.DrawWireSphere(t.transform.position, 0.2f);
        //}
    }

    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void OnDisable()
    {
        playerInput.Player.Disable();
    }

    private void Awake()
    {
        playerInput = new PlayerInputActions();
        mousePos = playerInput.Player.MousePos;
    }

    private void Start()
    {
        tentacles = GetComponent<PlayerSpikeManager>().spikeList;
        foreach (GameObject t in tentacles)
        {
            tentOrgRot.Add(t.transform.eulerAngles);
        }
    }

    private void Update()
    {
        if (prepareing)
        {
            Vector2 mouseScreenPosition = mousePos.ReadValue<Vector2>();
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
            mouseWorldPosition.z = 0;

            int nearestTentacleIndex = GetNearestTentacle();
            //Debug.Log("Nearest Tentacle Index: " + nearestTentacleIndex);

            for (int i = 0; i < tentacles.Count; i++)
            {
                Quaternion targetRotation;

                if (i == nearestTentacleIndex)
                {
                    var dir = mouseWorldPosition - transform.position;
                    var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
                }
                else
                {
                    targetRotation = Quaternion.Euler(tentOrgRot[i]);
                }

                // Use Lerp or Slerp to smoothly interpolate the rotation
                float rotationSpeed = 20.0f; // Adjust the rotation speed as needed

                // Slerp towards the target rotation
                tentacles[i].transform.rotation = Quaternion.Slerp(tentacles[i].transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }

        if (pending)
        {
            float moveSpeed = 17.0f;
            
            tentacles[selected].transform.GetChild(0).position = Vector3.Lerp(tentacles[selected].transform.GetChild(0).position, targetPos, Time.deltaTime * moveSpeed);
            if (tentacles[selected].transform.GetChild(0).position == targetPos)
            {
                Debug.Log("Ready");
                pending = false;
                negativ = true; // when spike doesnt hit
            }
        }

        if (negativ)
        {
            float moveSpeed = 5.0f;

            tentacles[selected].transform.GetChild(0).position = Vector3.Lerp(tentacles[selected].transform.GetChild(0).position, startPos, Time.deltaTime * moveSpeed);
        }

    }


    public void PrepareShoot()
    {
        if (!canShoot()) return;
        prepareing = true;
    }

    public void Shoot()
    {
        // shoot in this direction, if mouse not in deadzone
        Vector2 mouseScreenPosition = mousePos.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
        if (Vector2.Distance(transform.position, mouseWorldPosition) > deadzone)
        {
            Debug.Log("Shoot!");
            selected = GetNearestTentacle();
            targetPos = tentacles[selected].transform.GetChild(0).position + (tentacles[selected].transform.right * range);
            startPos = tentacles[selected].transform.GetChild(0).position;
            pending = true;
        }

    }

    public void PullPlayer()
    {

    }

    public int GetNearestTentacle()
    {
        int shortestIndex = 0;
        float shortestDist = Mathf.Infinity;

        Vector2 mouseScreenPosition = mousePos.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
        mouseWorldPosition.z = 0;

        //Debug.Log(mouseWorldPosition);

        for (int i = 0; i < tentacles.Count; i++)
        {
            GameObject tentacle = tentacles[i];
            float dist = Vector3.Distance(tentacle.transform.position, mouseWorldPosition);

            //Debug.Log("Dist: " + dist);

            if (dist < shortestDist)
            {
                //Debug.Log("Dist: " + dist);
                shortestIndex = i;
                shortestDist = dist;
            }
        }

        return shortestIndex;
    }

    public bool canShoot()
    {
        if (pending || prepareing || negativ || curCooldown > 0) return false;
        return true;
    }
}
