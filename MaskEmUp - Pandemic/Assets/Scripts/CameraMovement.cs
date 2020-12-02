using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public GameObject objectToFollow;
    public float cameraDistance = 7.5f;

    void Update()
    {
        Vector3 cameraPos = transform.localPosition;
        cameraPos.z = objectToFollow.transform.position.z - cameraDistance;
        cameraPos.x = objectToFollow.transform.position.x;
        transform.localPosition = cameraPos;
    }
}
