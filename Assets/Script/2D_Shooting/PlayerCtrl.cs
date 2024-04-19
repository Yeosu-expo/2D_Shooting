using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    public int wallet = 0;
    [SerializeField] private int health = 3;
    private int nowHealth;
    public int powerPoint = 0;

    private Transform healthBar;
    private GameObject greenBar;
    private float greenBarX;

    public int life = 3;
    [SerializeField] private int unbeatableTime;
    private bool unbeatableSign;
    public bool gameoverSignFromPlayer;


    // Start is called before the first frame update
    void Start()
    {
        //p_Bullet.SetActive(true);
        myTr = GetComponent<Transform>();
        firePos = myTr.Find("Fire Position").transform;

        animator = GetComponent<Animator>();

        healthBar = GameObject.Find("HealthBar").transform;

        greenBar = GameObject.Find("GreenBar");
        greenBarX = greenBar.transform.localScale.x;
        nowHealth = health;
        unbeatableSign = false;
        gameoverSignFromPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.position = new Vector3 (transform.position.x+0.03f, transform.position.y+0.9f, 0f);
        
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet" && !unbeatableSign)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;

            //int damage = collision.gameObject.GetComponent<BulletCtrl>().bulletDamage;
            int damage = 1;

            Vector3 greenScale = greenBar.transform.localScale;
            greenScale.x -= (healthBar.localScale.x)/(health/damage);
            float moveValue = (healthBar.localScale.x) / (health/damage) / 2;
            greenBar.transform.parent = null;
            greenBar.transform.localScale = greenScale;
            greenBar.transform.Translate(new Vector3(-moveValue, 0, 0));
            greenBar.transform.parent = healthBar;

            nowHealth -= damage;
            if(nowHealth <= 0)
            {
                StartCoroutine(CheckLifeAndHeal());
            }


            Invoke("ReturnToOrignalSprite", 1);
        }
    }

    private void ReturnToOrignalSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private IEnumerator CheckLifeAndHeal()
    {
        life -= 1;
        if(life <= 0) // 게임오버
        {
            gameoverSignFromPlayer = true;
        }
        else // 무적 및 회복
        {
            // 회복
            Debug.Log("DEAD");
            nowHealth = health;
            Vector3 greenScale = new Vector3(1, 0.09f, 1);
            greenBar.transform.parent = null;
            greenBar.transform.localScale = greenScale;
            Vector3 pos = GameObject.Find("WhiteBar").transform.position;
            pos.y -= 0.0005f;
            greenBar.transform.position = pos;
            greenBar.transform.parent = healthBar;

            // 무적
            unbeatableSign = true;
            yield return new WaitForSeconds(unbeatableTime);
            unbeatableSign = false;
        }

        yield return null;
    }
}
