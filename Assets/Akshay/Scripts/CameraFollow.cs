using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform GroundPosition;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x, GroundPosition.position.y,-10f);
    }
}
