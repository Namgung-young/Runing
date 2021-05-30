using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMovement : MonoBehaviour
{

    BlockManager theBlock;
    public float speed;

    void Start()
    {
        theBlock = FindObjectOfType<BlockManager>();        
    }
   
    void Update()
    {
        speed = theBlock.cubeMoveSpeed;

        if(transform.localPosition.x <= -64)
        {
            transform.localPosition = new Vector3(62.5f, 7.5f, 0);
        }
        else
        {
            transform.Translate(Vector3.left * speed * 0.5f * Time.deltaTime);
        }
                   
    }
}
