using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BtnToPlay : MonoBehaviour
{
    public void MoveToPlayScene()
    {
        SceneManager.LoadScene("Play");
    }
}
