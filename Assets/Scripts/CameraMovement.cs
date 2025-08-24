using System;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private void Update()
    {
        transform.position = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y, transform.position.z);
    }
}
