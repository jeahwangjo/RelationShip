using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{

    [SerializeField] private Transform Player;
    [SerializeField] private Vector3 offset;


    private void LateUpdate()
    {
        transform.position = Player.position + offset;
    }
}
