using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : SingletonBase<GameManager>
{
    public GameState gameState { get; private set; }

    [SerializeField] Unity.AI.Navigation.NavMeshSurface navMeshSurface;

    public GameObject character;
    private float keysCollected;

    [SerializeField] GameObject keyPrefab;
    List<Transform> keySpawnPoints = new List<Transform>();
    float keysAmount;
    
    [SerializeField] GameObject enemyPrefab;
    List<Transform> enemySpawnPoint = new List<Transform>();

    [Tooltip("How much seconds player got to win")]
    [SerializeField] float gameTimer;
    [SerializeField] TextMeshProUGUI timerTMP;

    [SerializeField] ExitGate exitGate;

    [Header("Background Music")]
    [SerializeField] List<AudioClip> backgroundMusicList = new List<AudioClip>();
    [Tooltip("In how much SECONDS music should changes")]
    [SerializeField] float musicPlayingFrequency = 240f;

    [Header("UI Settings")]
    [SerializeField] TextMeshProUGUI keyAmountUI;
    [SerializeField] GameObject interactionTip;
    [SerializeField] GameObject endGameUI, pauseUI;
    [SerializeField] GameObject gameplayUI;

    protected override void Awake()
    {
        base.Awake();

        gameState = GameState.GameFlow;
        Time.timeScale = 1f;
        exitGate.Lock();
    }

    private void Start()
    {
        StartCoroutine(BackgroundMusicProcessing());

        // define keys amount
        enemySpawnPoint = GetSpawnPoints("Enemy");
        foreach (Transform t in enemySpawnPoint)
            Instantiate(enemyPrefab, t.position, Quaternion.identity);

        keySpawnPoints = GetSpawnPoints("Key");
        keysAmount = keySpawnPoints.Count;

        foreach (Transform t in keySpawnPoints)
            Instantiate(keyPrefab, t.position, Quaternion.identity);

        UpdateKeysUI();
    }

    private void Update()
    {
        if (gameTimer <= 0)
        {
            EndGame();
        }
        else if (gameState == GameState.GameFlow)
        {
            gameTimer -= Time.deltaTime;
            timerTMP.text = ((int)gameTimer).ToString();
        }
    }

    private List<Transform> GetSpawnPoints(string tag)
    {
        List<Transform> spawnPoints = new List<Transform>();
        var spawnGO = GameObject.FindGameObjectsWithTag(tag);

        for (int i = 0; i < spawnGO.Length; i++)
        {
            spawnPoints.Add(spawnGO[i].transform);
        }

        return spawnPoints;
    }

    private IEnumerator BackgroundMusicProcessing()
    {
        AudioSource backgroundMusic = character.transform.Find("BackgroundMusic").GetComponent<AudioSource>();
        backgroundMusic.clip = backgroundMusicList[Random.Range(0, backgroundMusicList.Count)];
        backgroundMusic.Play();

        yield return new WaitForSeconds(musicPlayingFrequency);
        
        StartCoroutine(BackgroundMusicProcessing());
    }

    public void BakeNavmesh()
    {
        Debug.Log("[test] rebaking navmesh");
        navMeshSurface.BuildNavMesh();
    }

    public void KeyCollected()
    {
        keysCollected++;
        UpdateKeysUI();

        if (keysCollected == keysAmount)
        {
            exitGate.Unlock();
        }
    }

    private void UpdateKeysUI()
    {
        keyAmountUI.text = $"{keysCollected}/{keysAmount}";
    }

    public void ShowTip()
    {
        if (interactionTip.activeSelf) return;

        interactionTip.SetActive(true);
    }

    public void HideTip()
    {
        if (!interactionTip.activeSelf) return;

        interactionTip.SetActive(false);
    }

    public void EndGame(float delay = 0)
    {
        StartCoroutine(DelayedEndGame(delay));
    }

    private IEnumerator DelayedEndGame(float delay)
    {
        yield return new WaitForSeconds(delay);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameState = GameState.Pause;
        gameplayUI.SetActive(false);
        if (pauseUI.activeSelf) pauseUI.SetActive(false);
        Instantiate(endGameUI);
    }

    public void Pause()
    {
        if (gameState == GameState.GameFlow)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            gameState = GameState.Pause;
            pauseUI.SetActive(true);
            gameplayUI.SetActive(false);
        }
        else if (gameState == GameState.Pause)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            gameState = GameState.GameFlow;
            pauseUI.SetActive(false);
            gameplayUI.SetActive(true);
        }

        // change state of game
    }
}
