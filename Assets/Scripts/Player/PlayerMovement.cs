using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Tentacle Things")]
    public List<GameObject> tentacles;
    public List<Vector3> tentOrgRot;

    private PlayerInputActions playerInput;
    private InputAction mousePos;
    public float deadzone;

    [Header("States")]
    public bool Attached;
    public bool prepareing;

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

    }


    public void PrepareShoot()
    {
        prepareing = true;
    }
    public void Shoot()
    {
        prepareing = false;

        // shoot in this direction, if mouse not in deadzone
        Vector2 mouseScreenPosition = mousePos.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
        mouseWorldPosition.z = 0;
        if (Vector2.Distance(transform.position, mouseWorldPosition) > deadzone)
        {
            Debug.Log("Shoot!");
        }

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
}
