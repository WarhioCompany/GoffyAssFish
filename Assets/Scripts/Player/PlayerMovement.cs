using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Tentacle Things")]
    public GameObject[] tentacles;

    private PlayerInputActions playerInput;
    private InputAction mousePos;
    public float deadzone;

    [Header("States")]
    public bool Attached;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, deadzone);

        foreach (var t in tentacles)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(t.transform.position, 0.2f);
        }
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

    private void Update()
    {
        int nearestTentacleIndex = GetNearestTentacle();
        //Debug.Log("Nearest Tentacle Index: " + nearestTentacleIndex);
    }

    public void Shoot()
    {
        // get direction

        // shoot in this direction, if mouse not in deadzone
        Vector2 mouseScreenPosition = mousePos.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
        mouseWorldPosition.z = 0;
        if (Vector2.Distance(transform.position, mouseWorldPosition) > deadzone)
        {
            
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

        for (int i = 0; i < tentacles.Length; i++)
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
