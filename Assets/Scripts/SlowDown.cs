using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDown : MonoBehaviour
{
    public Vector3 dir;       
   
    void FixedUpdate()
    {
        transform.Translate(dir * 10 * Time.deltaTime);
    }
}
