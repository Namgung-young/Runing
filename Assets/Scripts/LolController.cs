using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LolController : MonoBehaviour
{
    GameManager theManager;

    void Start()
    {
        theManager = FindObjectOfType<GameManager>();        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public  void Dequeuelol()
    {
        theManager.DeleteLol(gameObject);
    }
}
