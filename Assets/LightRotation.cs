using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        //Light rotates when active
        transform.Rotate(new Vector3(0, 100, 0) * Time.deltaTime);
    }
}
