using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class initial_Cube : MonoBehaviour
{
    public GameObject start, end;
    public float moveSpeed;
    Vector3 dir;
    void Start()
    {
        dir = end.transform.position - start.transform.position;
        dir = dir.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
        if(transform.position.x < end.transform.position.x)
        {
            Destroy(this.gameObject);
        }
    }
}
