using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    public GameObject chicken;


    private void Start()
    {
        transform.position = new Vector3(chicken.transform.position.x, chicken.transform.position.y + 3, transform.position.z);        
    }
}
