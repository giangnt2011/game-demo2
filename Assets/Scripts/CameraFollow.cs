using PlayerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public static CameraFollow instance;
    [SerializeField] private Transform player;
    [SerializeField] private Transform end;
    private Vector3 storeCameraPositionOrigin;
    private Vector3 storeCameraRotationOrigin;

    [SerializeField] private float turnSpeed = 10.0f;

    
    private float yaxis;
    private Vector3 offset;
    private void Awake()
    {
        instance = this;
        offset = player.transform.position - transform.position;
        storeCameraPositionOrigin = transform.position;
        storeCameraRotationOrigin = transform.localEulerAngles;
    }

    
    private void LateUpdate()
    {
        if (player)
        {
            if (PlayerMovement.instance.RotateCamera)
            {
                Vector3 relativePos = player.position - transform.position;

                Quaternion targetRotation = Quaternion.LookRotation(relativePos, Vector3.up);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 0.5f * Time.deltaTime);

                transform.RotateAround(player.transform.position, Vector3.up, 20 * Time.deltaTime);
            }
            else
            {
                this.transform.position = player.transform.position - offset;
            }
        }
    }

    public void ResetCamera()
    {
        transform.position = storeCameraPositionOrigin;
        transform.localEulerAngles = storeCameraRotationOrigin;
    }

}