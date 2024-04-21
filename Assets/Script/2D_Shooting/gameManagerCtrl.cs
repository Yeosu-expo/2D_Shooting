using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class gameManagerCtrl : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyBPrefab; // enemy B 프리팹을 인스펙터에서 할당
    public GameObject enemyCPrefab; // enemy C 프리팹을 인스펙터에서 할당
    public GameObject Boss;
    public GameObject WhiteBar;
    
    public float startDelay = 2.0f; // 시작 전 딜레이 시간
    public float enemyBSpawnInterval = 1.0f; // enemy B 생성 간격
    public int enemyBCount = 5; // 생성할 enemy B의 수

    public float enemyCSpawnInterval = 1.0f; // enemy C 생성 간격
    public int enemyCCount = 5; // 생성할 enemy C의 수

    public bool gameStartSign;
    private bool gameStarted;
    private bool gameoverSign;
    private bool gameovered;

    public GameObject GameOverPanel;
    private GameObject GameOverText;
    public Button TryAgainBtn;

    public GameObject StartPannel;

    public int Score = 0;

    private void Start()
    {
        Boss = GameObject.Find("Boss 0");
        GameOverPanel.SetActive(false);
        TryAgainBtn.onClick.AddListener(TryAganinHandler);

        gameStartSign = false;
        gameStarted = false;
        gameovered = false;
    }

    void Update()
    {
        if (gameStartSign && !gameStarted)
        {
            player.SetActive(true);
            gameStarted = true;
            enemyBPrefab.SetActive(true);
            enemyCPrefab.SetActive(true);
            Boss.SetActive(true);
            StartCoroutine(SpawnEnemyRoutine());
        }
        if (gameovered)
        {
            return;
        }
        gameoverSign = GameObject.Find("Player").GetComponent<PlayerCtrl>().gameoverSignFromPlayer;
        if (gameoverSign)
        {
            GameoverHandler();
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(startDelay);

            // enemy B 생성
            for (int i = 0; i < enemyBCount; i++)
            {
                Instantiate(enemyBPrefab, GenerateSpawnPosition(), enemyBPrefab.transform.rotation);
                yield return new WaitForSeconds(enemyBSpawnInterval);
            }

            // enemy C 생성
            for (int i = 0; i < enemyCCount; i++)
            {
                Instantiate(enemyCPrefab, GenerateSpawnPosition(), enemyCPrefab.transform.rotation);
                yield return new WaitForSeconds(enemyCSpawnInterval);
            }

            Instantiate(Boss, new Vector3(0, 3.5f, 0f), Boss.transform.rotation);
        }
    }

    // 적이 나타날 위치를 무작위로 생성하는 메서드
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(-3.0f, 3.0f); // 예시 범위, 게임 환경에 맞게 조정 필요
        float spawnPosY = Random.Range(2.0f, 4.5f); // 예시 범위, 게임 환경에 맞게 조정 필요
        Vector3 spawnPosition = new Vector3(spawnPosX, spawnPosY, 0);
        return spawnPosition;
    }

    private void GameoverHandler()
    {
        enemyBPrefab.SetActive(false);
        enemyCPrefab.SetActive(false);
        Boss.SetActive(false);
        int wallet = GameObject.Find("Player").GetComponent<PlayerCtrl>().wallet;
        GameObject.Find("Player").SetActive(false);
        GameObject.Find("WhiteBar").SetActive(false);
        GameOverPanel.SetActive(true);

        GameOverText = GameObject.Find("Text over");
        GameOverText.GetComponent<TMPro.TextMeshProUGUI>().text += "\nScore: " + Score.ToString() + "\nCoin: " + wallet;

        gameovered = true;
    }

    private void TryAganinHandler()
    {
        player.GetComponent<PlayerCtrl>().gameoverSignFromPlayer = false;
        StartPannel.SetActive(true);
        
        player.GetComponent<PlayerCtrl>().wallet = 0;
        player.GetComponent<PlayerCtrl>().nowHealth = player.GetComponent<PlayerCtrl>().health;
        WhiteBar.SetActive(true);
        GameOverText.GetComponent<TMPro.TextMeshProUGUI>().text = "GameOver";
        player.GetComponent<PlayerCtrl>().life = player.GetComponent<PlayerCtrl>().lifeSet;

        GameOverPanel.SetActive(false);
        gameStarted = false;

        gameovered = false;
    }
}
