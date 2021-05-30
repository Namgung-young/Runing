using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenManager : MonoBehaviour
{
    public GameObject RayR, RayL = null;    

    GameManager theManager;
    Rigidbody2D therigid;
    Animator theAni;
    BlockManager theBlock;
    int maxJumpCount = 2;
    int curJumpCount;
    public float jumpPower;  

    void Awake()
    {
        therigid = GetComponent<Rigidbody2D>();
        theAni = GetComponent<Animator>();
        theManager = FindObjectOfType<GameManager>();
        theBlock = FindObjectOfType<BlockManager>();
        curJumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        if (curJumpCount!=0)
        {
            if (Input.GetMouseButtonDown(0))
            {              
                curJumpCount--;
                therigid.velocity = Vector3.zero;
                therigid.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            }
        }

        if(curJumpCount != maxJumpCount)
        {
            theAni.SetTrigger("Jump");
        }
        else
        {
            theAni.SetTrigger("Walk");
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "coin")
        {            
            theManager.Score();
            collision.gameObject.SetActive(false);
        }

        if (collision.gameObject.tag == "cubeCollider")
        {   
            curJumpCount = maxJumpCount;            
            collision.gameObject.SetActive(false);
        }

        else if(collision.gameObject.tag == "food")
        {
            theManager.GetTimer(10f);
            collision.gameObject.SetActive(false);
        }

        else if(collision.gameObject.tag == "egg")
        {
            Debug.Log("º¯½Å!");
        }

        else if(collision.gameObject.tag == "slow")
        {
            theBlock.slowDownCount++;
            collision.gameObject.SetActive(false);
            theBlock.MakeFloatingText();
        }
    }    

}
