using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowed : MonoBehaviour
{
    [SerializeField] private Transform followed;
    void Update()
    {
        transform.position = new Vector3(followed.transform.position.x, followed.transform.position.y+11, followed.transform.position.z-10);
    }
}
