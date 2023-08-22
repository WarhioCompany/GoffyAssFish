using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTentacle : MonoBehaviour
{
    public enum spikeState
    {
        IDLE,
        LOOKAT,
        MOVETO,
        HIT
    }

    public spikeState state;

    private Vector3 orgPos; private Vector3 orgRot;
    private GameObject player;

    [Header("Positioning")]
    private GameObject spike; // move this for position, rotate this for rotation

    [Header("Moving")]
    private Vector3 target;
    public Vector3 targetPos;

    public float timerAmount;
    public float retrieveTimer = 0;

    private void Start()
    {
        spike = transform.GetChild(0).gameObject;
        orgPos = spike.transform.position;
        orgRot = transform.eulerAngles;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
        if (retrieveTimer > 0)
        {
            retrieveTimer -= Time.fixedDeltaTime;
        } else if (retrieveTimer <= 0 && state == spikeState.MOVETO)
        {
            GetComponentInParent<PlayerMovement>().spikeState = PlayerMovement.SPIKESTATE.RETRIEVE;
        }

        if (state == spikeState.LOOKAT)
        {
            // look at the target
            //////////////////////////////////////
            Quaternion targetRotation;
            var dir = target - player.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Use Lerp or Slerp to smoothly interpolate the rotation
            float rotationSpeed = 20.0f; // Adjust the rotation speed as needed

            // Slerp towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            //////////////////////////////////////
        }
        else if (state == spikeState.MOVETO)
        {
            // look at and move towards the target
            //////////////////////////////////////
            Quaternion targetRotation;
            var dir = target - player.transform.position;
            var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Use Lerp or Slerp to smoothly interpolate the rotation
            float rotationSpeed = 20.0f; // Adjust the rotation speed as needed

            // Slerp towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            if (retrieveTimer <= 0)
            {
                retrieveTimer = timerAmount;
            }
            //////////////////////////////////////

            //////////////////////////////////////
            float moveSpeed = 20.0f;

            spike.transform.position = Vector3.Lerp(spike.transform.position, targetPos, Time.deltaTime * moveSpeed);

            //Debug.Log("Spike: " + spike.transform.position + "\nShould: " + targetPos);
            //////////////////////////////////////
        }
        else if (state == spikeState.IDLE)
        {
            // reset and Idel Anim
            //////////////////////////////////////
            Quaternion targetRotation;
            targetRotation = Quaternion.Euler(orgRot);

            // Use Lerp or Slerp to smoothly interpolate the rotation
            float rotationSpeed = 20.0f; // Adjust the rotation speed as needed

            // Slerp towards the target rotation
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            //////////////////////////////////////
            
            //////////////////////////////////////
            float moveSpeed = 17.0f;

            spike.transform.position = Vector3.Lerp(spike.transform.position, orgPos, Time.deltaTime * moveSpeed);
            //////////////////////////////////////
        }
    }

    public void LookAt(Vector3 point)
    {
        target = point;
        state = spikeState.LOOKAT;
    }

    public void MoveTo(Vector3 point, float range)
    {
        if (state == spikeState.MOVETO) return;
        target = point;
        targetPos = transform.position + (transform.right * range) - (transform.right);
        //targetPos.x = (float)(Math.Truncate(targetPos.x * 100) / 100);
        //targetPos.y = (float)(Math.Truncate(targetPos.y * 100) / 100);
        state = spikeState.MOVETO;
    }

    public void ResetSpike()
    {
        if (state == spikeState.IDLE) return;
        target = Vector3.zero; state = spikeState.IDLE;
    }


    public void Collided(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Object"))
        {
            // send msg to PM that it has to pullitself into the direction of the spike
        }
    }
}
