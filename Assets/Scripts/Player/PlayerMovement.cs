using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public enum SPIKESTATE
    {
        READY,
        PREPARE,
        WAITING,
        RETRIEVE
    }
    public enum PLAYERSTATE
    {
        NONE,
        ATTACHED,
        STUNNED
    }

    [Header("PlayerState")]
    public PLAYERSTATE playerState = PLAYERSTATE.NONE;

    [Header("SpikeShoot")]
    public SPIKESTATE spikeState = SPIKESTATE.READY; // state of cur shot
    public float deadzone;
    public bool forceSpikeDissabled;
    public float range = 0.8f;
    public float retrieveTime = 0.5f;

    private int curSpike;
    private Vector3 movePos;
    private List<GameObject> spikes;


    [Header("Countdown")]
    public float cooldownTime;
    private float curTimer;

    // Mousepos
    private PlayerInputActions playerInput;
    private InputAction mousePos;

    private void Start()
    {
        spikes = GetComponent<PlayerSpikeManager>().spikeList;
    }

    private void Update()
    {
        if (curTimer > 0)
        {
            curTimer -= Time.fixedDeltaTime;
            return;
        }

        int nearest = GetNearestTentacle();
        if (spikeState == SPIKESTATE.PREPARE)
        {
            // tell nearest spike to look at mousepos, any other spike resets
            for (int i = 0; i < spikes.Count; i++)
            {
                if (i == nearest)
                {
                    spikes[i].GetComponent<PlayerTentacle>().LookAt(getmousePos());
                } 
                else
                {
                    spikes[i].GetComponent<PlayerTentacle>().ResetSpike();
                }
            }
        }
        else if (spikeState == SPIKESTATE.WAITING)
        {
            // tell nearest spike to movetowards mousepoint + range and save the spike + movePos
            spikes[curSpike].GetComponent<PlayerTentacle>().MoveTo(movePos, range);
            
        }
        else if (spikeState == SPIKESTATE.RETRIEVE)
        {
            // tell saved spike to reset, delete saved spike and movePos
            spikes[curSpike].GetComponent<PlayerTentacle>().ResetSpike();
            movePos = Vector3.zero;
            curSpike = -1;
            curTimer = cooldownTime;
            spikeState = SPIKESTATE.READY;
        }
    }

    public void Prepare()
    {
        if (canShoot())
        {
            spikeState = SPIKESTATE.PREPARE;
        }
    }

    public void Shoot()
    {
        // initiate the Spike Shoot
        if (Vector2.Distance(transform.position, getmousePos()) > deadzone)
        {
            // not in click deadzone
            curSpike = GetNearestTentacle();
            movePos = getmousePos();

            spikeState = SPIKESTATE.WAITING;
        }
    }

    public void Attach(GameObject obj)
    {
        // attach to a Object (set as Child of Obj)
        transform.parent = obj.transform;
    }

    public void Detatch()
    {
        // detatch from gameobject (set as Child from nothing)
        transform.parent = null;
    }

    public bool canShoot()
    {
        if (forceSpikeDissabled) return false;
        if (spikeState == SPIKESTATE.READY && curTimer <= 0) return true;
        return false;
    }

    public Vector3 getmousePos()
    {
        Vector2 mouseScreenPosition = mousePos.ReadValue<Vector2>();
        Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
        return mouseWorldPosition;
    }

    public int GetNearestTentacle()
    {
        int shortestIndex = 0;
        float shortestDist = Mathf.Infinity;

        //Debug.Log(mouseWorldPosition);

        for (int i = 0; i < spikes.Count; i++)
        {
            GameObject tentacle = spikes[i];
            float dist = Vector3.Distance(tentacle.transform.position, getmousePos());

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

    #region inputaction_setup
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
    #endregion
}
