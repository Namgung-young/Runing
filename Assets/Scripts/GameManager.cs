using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject chicken = null;
    public GameObject pnlFadeOut = null;
    public GameObject lol = null;
    public GameObject BG = null;
    public GameObject FirstCube = null;
    Camera theCam;
    public Text txtGameOver = null;
    public Text txtScore = null;
    public Text txtBestScore = null;
    public int score = 0;
    public int bestScore;

    public Queue<GameObject> queuelol = new Queue<GameObject>();
    public List<GameObject> ListLol = new List<GameObject>();

    public Image timeBar = null;
    float limitTime, curTime;

    bool isGameOver = false;
    bool isisGameOver = false;
    bool isTerminal = false;
        
    void Start()
    {
        for (int i = 0; i < 26; i++)
        {
            GameObject clone = Instantiate(lol);
            clone.transform.SetParent(FindObjectOfType<Canvas>().transform);
            clone.GetComponent<RectTransform>().localPosition = Vector3.zero;

            queuelol.Enqueue(clone);
            clone.gameObject.SetActive(false);
        }

        theCam = FindObjectOfType<Camera>();
        BG.transform.position = new Vector3(theCam.transform.position.x, theCam.transform.position.y, 0);

        limitTime = 60f;
        curTime = limitTime;
        
        int Bs = PlayerPrefs.GetInt("bestScore");
        bestScore = Bs;
    }

    IEnumerator WaitTime(float _time)
    {
        yield return new WaitForSeconds(_time);
    }
   
    void Update()
    {
        if (!isGameOver)
        {
            curTime -= Time.deltaTime;
        }
        timeBar.fillAmount = curTime / limitTime;
        if (chicken.transform.position.y < -15f || curTime <= 0 || chicken.transform.position.x <-27)
        {
            if (!isisGameOver)
            {
                isisGameOver = true;
                GameOver();
            }
        }

        if(isTerminal && Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            SceneManager.LoadScene("Start");
        }
    }

    void GameOver()
    {        
        isGameOver = true;
        chicken.gameObject.SetActive(false);
        FadeIn();
        txtScore.text = "Your Score : " + score.ToString();
        txtBestScore.gameObject.SetActive(true);
        if(score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("bestScore", bestScore);
        }
        PlayerPrefs.Save();
        txtBestScore.text = "Best Score : " + bestScore.ToString();
        StartCoroutine(TxtGameOverCoroutine());
        StartCoroutine(lolCountCoroutine());
        
    }

    IEnumerator lolCountCoroutine()
    {
        while (isGameOver)
        {
            int _x = Random.Range(-300, 300);
            int _y = Random.Range(-120, 100);

            if (queuelol.Count == 0)
            {
                break;
            }            
            else
            {
                if(queuelol.Count < 20)
                {
                    isTerminal = true;
                }
                GameObject lol = queuelol.Dequeue();
                ListLol.Add(lol.gameObject);
                lol.gameObject.SetActive(true);
                lol.transform.localPosition = new Vector3(_x, _y, 0);

                yield return new WaitForSeconds(0.2f);
            }            
        }
    }

    void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(FadeOutCoroutine());        
    }

    IEnumerator FadeOutCoroutine()
    {
        pnlFadeOut.SetActive(true);
        Color color = pnlFadeOut.GetComponent<Image>().color;
        color.a = 0f;
        while(color.a >= 0.98f)
        {
            color.a += 0.01f;            
            color = pnlFadeOut.GetComponent<Image>().color;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator TxtGameOverCoroutine()
    {
        txtGameOver.gameObject.SetActive(true);

        Color color = txtGameOver.GetComponent<Text>().color;
        color.a = 0f;
        while (color.a >= 0.98f)
        {
            color.a += 0.005f;
            color = txtGameOver.GetComponent<Text>().color;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Score()
    {
        score++;
        txtScore.text = score.ToString();
    }

    public void GetTimer(float _plusTime)
    {
        if(curTime +_plusTime >= limitTime)
        {
            curTime = limitTime;
        }
        else
        {
            curTime += 10;
        }    
    }

    public void ResetGame()
    {
        isisGameOver = false;
        isGameOver = false;
        score = 0;
        chicken.gameObject.SetActive(true);
        chicken.transform.position = new Vector3(0, 2, 0);
        txtBestScore.gameObject.SetActive(false);
        txtScore.text = score.ToString();

        Color color = txtGameOver.GetComponent<Text>().color;
        color.a = 0f;
        txtGameOver.GetComponent<Text>().color = color;
        txtGameOver.gameObject.SetActive(false);

        for (int i = 0; i < ListLol.Count; i++)
        {
            DeleteLol(ListLol[i]);            
        }
        ListLol.Clear();

        FirstCube.transform.position = new Vector3(-0.02f, -6.6f, 0);
    }

    public void DeleteLol(GameObject _obj)
    {
        _obj.SetActive(false);
        queuelol.Enqueue(_obj);
        _obj.transform.localPosition = Vector3.zero;
    }
    
}
