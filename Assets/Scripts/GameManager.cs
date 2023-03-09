using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] private LevelController[] levelControllers;
    [SerializeField] private Transform player;

    [SerializeField] private Transform gameObjectTransform;
    [SerializeField] private Transform ballCollection;
    [SerializeField] private Transform ballPool;
    [SerializeField] private GameObject NextLevelTxt;
    [SerializeField] private GameObject ReplayTxt;
    private LevelController currentLevel;

    private int randomValue = 0;

    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 60;
        
    }
    private void Start()
    {
        LoadLevel();
    }
    public void LoadLevel()
    {
        // tạo ra đường chơi (level)
        if(currentLevel)
        {
            Destroy(currentLevel.gameObject);
        }
        
        randomValue = Random.Range(0, levelControllers.Length);
        currentLevel = Instantiate(levelControllers[randomValue], Vector3.zero, Quaternion.identity);
        currentLevel.InitBalls(gameObjectTransform, ballCollection, ballPool);
        EventDispatcher.Instance.PostEvent(EventID.LoadLevel, currentLevel);
        NextLevelTxt.SetActive(false);
        ReplayTxt.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Replay()
    {
        //Destroy(currentLevel.gameObject);
        //currentLevel = Instantiate(levelControllers[randomValue], Vector3.zero, Quaternion.identity);
        //currentLevel.InitBalls(gameObjectTransform, ballCollection, ballPool);
        //ReplayTxt.SetActive(false);
        //NextLevelTxt.SetActive(false);
        LoadLevel();
        EventDispatcher.Instance.PostEvent(EventID.Replay, currentLevel);
        Time.timeScale = 1f;
    }
}
