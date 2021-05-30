using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    BlockManager theBlock;
    float endline;
    Vector3 dir;
    public float moveSpeed;

    void Start()
    {
        theBlock = FindObjectOfType<BlockManager>();
        dir = theBlock.dir;
        endline = theBlock.endline;
        moveSpeed = theBlock.cubeMoveSpeed;
    }

    private void FixedUpdate()
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
        if(transform.position.x < endline)
        {
            theBlock.DeleteCube(this.gameObject);
        }
    }

    public void ChangeSpeed(float _spd)
    {
        moveSpeed = _spd;
    }
}
