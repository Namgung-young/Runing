using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    BlockManager theBlock;
    float endline;
    Vector3 dir;
    public float moveSpeed;
    GameObject child;
    public bool getMove = true;

    void Start()
    {
        theBlock = FindObjectOfType<BlockManager>();
        dir = theBlock.dir;
        endline = theBlock.endline;
        moveSpeed = theBlock.cubeMoveSpeed;
        child = transform.GetChild(0).gameObject;
    }

    private void FixedUpdate()
    {
        transform.Translate(dir * moveSpeed * Time.deltaTime);
        if(transform.position.x < endline)
        {
            theBlock.DeleteCube(this.gameObject);
        }

        if (!getMove)
        {
            getMove = true;
            MoveLeftRight();
        }
    }

    public void ChangeSpeed(float _spd)
    {
        moveSpeed = _spd;
    }

    public void MoveLeftRight()
    {
        StopAllCoroutines();
        StartCoroutine(MoveLeftRightCoroutine());
    }

    IEnumerator MoveLeftRightCoroutine()
    {
        yield return new WaitForSeconds(1.5f);

        int a = Random.Range(1, 4);

        if(a == 1)
        {
            while (child.transform.localPosition.x <= 2)
            {
                child.transform.localPosition += new Vector3(0.1f, 0, 0);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else if(a ==2)
        {
            while (child.transform.localPosition.x >= -2)
            {
                child.transform.localPosition -= new Vector3(0.1f, 0, 0);
                yield return new WaitForSeconds(0.1f);
            }
        }
        else
        {
            while (child.transform.localPosition.y >= -2)
            {
                child.transform.localPosition -= new Vector3(0, 0.2f, 0);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
}
