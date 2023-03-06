using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float yaxis;
    private Vector3 offset;
    private void Awake()
    {
        offset = player.transform.position - transform.position;
    }
    private void LateUpdate()
    {
        if (player)
        {
            this.transform.position = player.transform.position - offset;
        }
    }

}