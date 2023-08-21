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
        HIT,
        RETRIEVE
    }

    public spikeState state;

    private Vector3 orgPos; private Vector3 orgRot;
    private GameObject player;

    [Header("Positioning")]
    private GameObject spike; // move this for position, rotate this for rotation

    [Header("Moving")]
    private Vector3 target;
    private Vector3 targetPos;

    private void Start()
    {
        spike = transform.GetChild(0).gameObject;
        orgPos = spike.transform.position;
        orgRot = transform.eulerAngles;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Update()
    {
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
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
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
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            //////////////////////////////////////

            //////////////////////////////////////
            float moveSpeed = 17.0f;

            spike.transform.position = Vector3.Lerp(spike.transform.position, targetPos, Time.deltaTime * moveSpeed);

            if (spike.transform.position == targetPos)
            {
                // reach end and retrieve, bcs nothing hit
                state = spikeState.IDLE;
            }
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
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            //////////////////////////////////////
            
            //////////////////////////////////////
            float moveSpeed = 17.0f;

            spike.transform.position = Vector3.Lerp(spike.transform.position, orgPos, Time.deltaTime * moveSpeed);
            //////////////////////////////////////
        }
        else if (state == spikeState.RETRIEVE)
        {
            // reset and Idel Anim
            //////////////////////////////////////
            Quaternion targetRotation;
            targetRotation = Quaternion.Euler(orgRot);

            // Use Lerp or Slerp to smoothly interpolate the rotation
            float rotationSpeed = 20.0f; // Adjust the rotation speed as needed

            // Slerp towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            //////////////////////////////////////

            //////////////////////////////////////
            float moveSpeed = player.GetComponent<PlayerMovement>().retrieveTime;

            spike.transform.position = Vector3.Lerp(spike.transform.position, orgPos, Time.deltaTime * moveSpeed);

            if (spike.transform.position == orgPos)
            {
                // reach end and retrieve, bcs nothing hit
                state = spikeState.IDLE;
            }
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
        target = point;
        targetPos = orgPos + (transform.right * range);
        Debug.Log(targetPos);
        state = spikeState.MOVETO;
    }

    public void ResetSpike()
    {
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
