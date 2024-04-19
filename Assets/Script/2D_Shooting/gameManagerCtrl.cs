using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerCtrl : MonoBehaviour
{
    public GameObject enemyBPrefab; // enemy B 프리팹을 인스펙터에서 할당
    public GameObject enemyCPrefab; // enemy C 프리팹을 인스펙터에서 할당
    public GameObject Boss;

    public float startDelay = 2.0f; // 시작 전 딜레이 시간
    public float enemyBSpawnInterval = 1.0f; // enemy B 생성 간격
    public int enemyBCount = 5; // 생성할 enemy B의 수

    public float enemyCSpawnInterval = 1.0f; // enemy C 생성 간격
    public int enemyCCount = 5; // 생성할 enemy C의 수

    private void Start()
    {
        Boss = GameObject.Find("Boss 0");
        StartCoroutine(SpawnEnemyRoutine());
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
}
