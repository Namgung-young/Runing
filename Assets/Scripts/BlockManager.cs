using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public GameObject prefab_Cube = null;
    public GameObject posStart, posEnd = null;
    public GameObject prefab_Timer = null;
    public GameObject prefab_coin = null;
    public GameObject prefab_slowDown = null;
    public GameObject prefab_floatingText = null;
    Queue<GameObject> poolingObjectQueue = new Queue<GameObject>();
    ChickenManager theChicken;
    public Vector3 dir;
    public float endline;
    bool cubeCreat = false;
    int randomNum;
    public float cubeMoveSpeed;
    bool changeSpd;
    GameManager theManager;
    public int slowDownCount = 0;
    int slowdownCount = 0;


    void Start()
    {
        
        theChicken = FindObjectOfType<ChickenManager>();
        theManager = FindObjectOfType<GameManager>();

        

        for (int i = 0; i < 100; i++)
        {
        GameObject cube = Instantiate(prefab_Cube, posStart.transform.position, Quaternion.identity);      
        float a = Random.Range(1f , 3f);
        float b = Random.Range(-1f, 5f);
        cube.transform.GetChild(0).localScale = new Vector3(a, 2, cube.transform.GetChild(0).localScale.z);
        cube.transform.position = new Vector3(cube.transform.position.x, b, cube.transform.position.z);
        poolingObjectQueue.Enqueue(cube);
        cube.SetActive(false);
        }

        dir = posEnd.transform.position - posStart.transform.position;
        dir = dir.normalized;
        endline = posEnd.transform.position.x;

        cubeMoveSpeed = 10;
    }

    private void Update()
    {
        if (!cubeCreat)
        {
            cubeCreat = true;
            CreatCube();
        }

        if (!changeSpd)
        {
            ControlTime();
        }
    }

    void CreatCube()
    {
        StartCoroutine(CreateCube());        
    }

    IEnumerator CreateCube()
    {
        randomNum = Random.Range(0, 100);        

        GameObject cube = poolingObjectQueue.Dequeue();
        cube.GetComponent<Cube>().moveSpeed = cubeMoveSpeed;
        Vector3 cubeScale = cube.transform.GetChild(0).transform.localScale;
        cube.SetActive(true);

        float energyPoint = Random.Range(1f, 2.5f);
        
        Vector3 left = new Vector3(- cubeScale.x * 0.5f - 3f, cubeScale.y +1.5f * 0.5f + energyPoint, 0);
        Vector3 right = new Vector3(cubeScale.x * 0.5f + 3f, cubeScale.y +1.5f * 0.5f + energyPoint, 0);      
        

        GameObject coin = Instantiate(prefab_coin);
        coin.transform.SetParent(cube.transform);

        int a = Random.Range(5, 9);

        coin.transform.position = 
            new Vector3(cube.transform.GetChild(0).transform.position.x, 
            cube.transform.GetChild(0).transform.position.y + cube.transform.GetChild(0).transform.localScale.y * 1f + (randomNum * 0.025f) , 
            cube.transform.localPosition.z);

        
        if (randomNum <= 5)
        {
            GameObject timer = Instantiate(prefab_Timer);
            timer.transform.SetParent(cube.transform);
            timer.transform.localPosition = left;          
        }
        else if(5 < randomNum && randomNum <= 10)
        {
            GameObject timer = Instantiate(prefab_Timer);
            timer.transform.SetParent(cube.transform);
            timer.transform.localPosition = right;            
        }
        else if(randomNum >= 97 || slowdownCount == 16)
        {
            GameObject slowDown = Instantiate(prefab_slowDown);
            slowDown.transform.position = posStart.transform.position + new Vector3(Random.Range(1,10), 4, 0);
            slowDown.GetComponent<SlowDown>().dir = (posEnd.transform.position + new Vector3(0, 12, 0)) -
                (posStart.transform.position + new Vector3(0, 6, 0));
            slowDown.GetComponent<SlowDown>().dir = slowDown.GetComponent<SlowDown>().dir.normalized;
            slowdownCount = 0;            
        }
        int _time = Random.Range(0, 100);
        float _delay = 1 + _time * 0.01f;
        yield return new WaitForSeconds(_delay);
        cubeCreat = false;
        slowDownCount++;        
    }

    public void DeleteCube(GameObject obj)
    {
        obj.SetActive(false);
        poolingObjectQueue.Enqueue(obj);
        obj.transform.position = posStart.transform.position;
    }   

    public void ControlTime()
    {
        changeSpd = true;
        StopCoroutine(TimeControl());
        StartCoroutine(TimeControl());
    }

    IEnumerator TimeControl()
    {
        float cms = 10 * (1 + (theManager.score - 3 * slowDownCount) * 0.03f);
        if(cms <= 10)
        {
            cubeMoveSpeed = 10;
        }
        else if(cms > 18)
        {
            cubeMoveSpeed = 18;
        }
        else
        {
            cubeMoveSpeed = cms;
        }
        
        yield return new WaitForSeconds(0.2f);
        changeSpd = false;
    }

    public void MakeFloatingText()
    {        
        GameObject floatingText = Instantiate(prefab_floatingText, theChicken.transform.position + new Vector3(0,2,0)
            , Quaternion.identity);
        floatingText.transform.SetParent(FindObjectOfType<Canvas>().transform);
    }

    public void ResetCube()
    {
        cubeMoveSpeed = 0;
        Cube[] cubesArr = FindObjectsOfType<Cube>();
        for (int i = 0; i < cubesArr.Length; i++)
        {
            if (cubesArr[i].gameObject.activeSelf)
            {
                DeleteCube(cubesArr[i].gameObject);
            }
        }
    }
}
