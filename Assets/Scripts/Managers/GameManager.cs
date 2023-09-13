using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    public Dictionary<string, Sprite> SpriteDic = new Dictionary<string, Sprite>();
    public Sprite sp;
    //======================================================================//
    //UI
    public TextMeshProUGUI leftText;
    //======================================================================//
    //�� �ý��� �� ������Ƽ �Լ� ��
    #region
    //������ ���� ����
    public int brickCreate;
    //�μ� ���� ���� (Need Count)
    private int brickDestroy;
    public int BrickDestroy
    {
        get
        {
            return brickDestroy;
        }

        set
        {
            BrickLeft--;
            if (brickCreate == BrickDestroy)
            {
                GameOver();
            }
            brickDestroy = value;
        }
    }
    // ���� ���� ���� (���� = ���� - �μ�)
    private int brickLeft;
    public int BrickLeft
    {
        get
        {
            //Debug.Log("C : " + brickCreate + "/ D : " + BrickDestroy);
            return brickCreate - brickDestroy;
        }

        set { brickLeft = value; }
    }

    //Ȱ������ �� ���� (0�� �Ǹ� ���� ����)
    private int ballCount;
    public int BallCount
    {
        get
        {
            return ballCount;
        }

        set
        {
            if (ballCount < 1)
            {
                GameClear();
            }
            ballCount = value;
        }
    }

    private int stageLevel;
    public int StageLevel
    {
        get
        {
            return stageLevel;
        }

        set
        {
            stageLevel = value;
        }
    }
    #endregion


    //======================================================================//
    //������ �����ɶ� ���Ǵ� �Լ�

    //public void ViewLeft()
    //{
    //    leftText.text = "" + BrickLeft;
    //}
    
    public void BrickTouch()
    {
        brickDestroy++;
    }

    public void GameClear()
    {
        Time.timeScale = 0;
        UIManager.Instance.optionBtn.SetActive(false);
        UIManager.Instance.gameOverWindow.SetActive(true);
        UIManager.Instance.gameOverText.text = "STAGE CLEAR";
        UIManager.Instance.gameOverText.color = Color.green;
    }

    public void GameOver()
    {
        //GameOver
        Time.timeScale = 0;
        UIManager.Instance.optionBtn.SetActive(false);
        UIManager.Instance.gameOverWindow.SetActive(true);
        UIManager.Instance.gameOverText.text = "GAME OVER";
        UIManager.Instance.gameOverText.color = Color.red;
    }

    public override void Awake()
    {
        base.Awake();
    }

    // ���ο� ���� �߰�
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ���ο� ���� �Ʒ� ������ ���� ȣ��
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Time.timeScale != 1.0f) Time.timeScale = 1.0f;

        if (stageLevel != 0)
        {
            StageInit();
            UIManager.Instance.ViewLeft();
        }
    }

    // ���� ���� ��
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void SpriteLoad(GameObject obj , string name)
    {
        obj.GetComponent<SpriteRenderer>().sprite = SpriteDic[name];

    }

    public void SpriteInit()
    {
        string path = "Assets/Resources/Sprites";
        DirectoryInfo di = new DirectoryInfo(path);
        foreach (FileInfo file in di.GetFiles())
        {
            if (!file.Name.Contains(".meta"))
            {
                string[] fileName = file.Name.Split('.');
                Sprite s = Resources.Load<Sprite>("Sprites/" + fileName[0]);
                SpriteDic.Add(fileName[0], s);
                //Debug.Log("���ϸ� : " + fileName[0]);

            }
        }
    }

    private void StageInit()
    {
        brickDestroy = 0;

        switch (stageLevel)
        {
            case 1:
                brickCreate = 45;
                break;
            case 2:
                brickCreate = 98;
                break;
            case 3:
                brickCreate = 87;
                break;
            case 4:
                brickCreate = 74;
                break;
            default:
                return;
        }
    }
}
