using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public int speed = 5; // 플레이어 속도
    public Transform myTr; // 플레이어의 Transform 컴포넌트
    public GameObject p_Bullet; // 발사할 총알의 프리팹
    public Transform firePos; // 총알이 발사될 위치

    private int iter = 0; // 발사 간격을 조절하기 위한 변수
    public int level = 0; // 플레이어 레벨, 총알 발사 패턴을 조절할 수 있음
    public int type = 0; // 총알 타입

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        myTr = GetComponent<Transform>();
        p_Bullet = GameObject.Find("Player Bullet 0");
        firePos = myTr.Find("Fire Position").transform;

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        animator.SetFloat("p_hori", h);

        Vector3 moveDir = Vector3.up * v + Vector3.right * h;

        transform.Translate(moveDir * speed * Time.deltaTime);

        // 마우스 왼쪽 버튼 클릭 시 총알 발사
        if (Input.GetMouseButton(0))
        {
            iter++;
            if (iter % 60 == 0) // 일정 간격으로 총알 발사
            {
                for (int i = 0; i <= level; i++) // 레벨에 따라 발사되는 총알의 수 조절
                {
                    if (type == 0)
                    {
                        Instantiate(p_Bullet, firePos.position + new Vector3(((1.0f - level) / 2.0f + i) * 0.3f, 0.0f, 0.0f), Quaternion.identity);
                    }
                    else if (type == 1)
                    {
                        Instantiate(p_Bullet, firePos.position, Quaternion.Euler(0f,0f,((1.0f - level)/2.0f +i) * 5.0f));
                    }
                }
            }
        }
    }
}
