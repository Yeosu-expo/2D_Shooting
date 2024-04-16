using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManagerCtrl : MonoBehaviour
{
    public float e_making_speed = 2.0f;
    [SerializeField] private GameObject enemy;
    private Vector2 pos;
    private Vector2 nowPos;
    private bool stage2sign;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GameObject.Find("Enemy B");
        pos = new Vector2(-2.0f, 4.0f);
        nowPos = pos;
        stage2sign = false;

        StartCoroutine(SpawnAtIntervals());
        StartCoroutine(Stage2());
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    IEnumerator SpawnAtIntervals()
    {
        // 무한 루프
        while (true)
        {
            if (e_making_speed > 0.5f)
            {
                e_making_speed -= 0.1f;
            }
            //else if(e_making_speed <= 0.5f)
            //{
            //    stage2sign = true;
            //    e_making_speed = 2.0f;
           
            //    yield break;
            //}

            nowPos.x += 1.0f;
            if(nowPos.x > 4.0f)
            {
                nowPos.x = -2.0f;
            }

            
            yield return new WaitForSeconds(e_making_speed); // 지정된 시간만큼 대기
            Instantiate(enemy, nowPos, Quaternion.identity); // 객체 생성
        }
    }

    IEnumerator Stage2()
    {
        for (; ; ) {
            if(stage2sign)
            {
                break;
            }
        }
        
        GameObject e2 = GameObject.Find("Enemy C");
        while (true)
        {
            if (e_making_speed > 0.5f)
            {
                e_making_speed -= 0.1f;
            }
            else
            {
                yield break;
            }

            nowPos.x += 1.0f;
            if (nowPos.x >= 4.0f)
            {
                nowPos.x = -2.0f;
            }


            yield return new WaitForSeconds(e_making_speed); // 지정된 시간만큼 대기
            Instantiate(e2, nowPos, Quaternion.identity); // 객체 생성
        }
    }
}
