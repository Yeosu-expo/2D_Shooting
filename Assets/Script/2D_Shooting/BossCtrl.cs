using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCtrl : MonoBehaviour
{
    [SerializeField] private int e_speed;
    [SerializeField] private int e_Type;
    [SerializeField] private int b_Type;

    private int iter = 0;
    private float attackPoint;
    public Transform targetTr;

    public GameObject e_Bullet;
    public Transform firePos; // ?????? ?????? ????

    //private float t = 0f;


    public Transform[] wayPoints;
    private Vector2 gizmoPosition;

    private Animator animator;
    public GameObject item;
    public GameObject coin;

    public int health = 5;

    // Start is called before the first frame update
    void Start()
    {
        e_speed = 1;
        attackPoint = Random.Range(2.0f, 4.0f);

        animator = gameObject.GetComponent<Animator>();
        //targetTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        coin = GameObject.Find("Coin 0");
        item = GameObject.Find("Power 0");
    }

    // Update is called once per frame
    void Update()
    {
        // StartCoroutine(BaizerCurve());
        iter++;

        Vector2 attackDirection = (targetTr.position - transform.position).normalized;
        //transform.Translate(attackDirection * e_speed * Time.deltaTime, Space.World);
        float _angle = Mathf.Atan2(attackDirection.x, -attackDirection.y) * Mathf.Rad2Deg - 180;
        Quaternion _rot = Quaternion.Euler(0, 0, _angle + 180);


        if (transform.position.y > attackPoint)
        {
            switch (e_Type)
            {
                case 1:
                    transform.Translate(Vector2.down * e_speed * Time.deltaTime, Space.World);
                    break;
                case 2:
                    transform.rotation = _rot;
                    transform.Translate(Vector2.down * e_speed * Time.deltaTime, Space.World);
                    break;
                case 3:
                    transform.rotation = _rot;
                    transform.Translate(Vector2.down * e_speed * Time.deltaTime, Space.World);
                    break;
                default:
                    break;
            }
        }
        else
        {
            if (iter / 120 > 5)
            {
                switch (e_Type)
                {
                    case 1:
                        transform.Translate(Vector2.down * e_speed * Time.deltaTime, Space.World);
                        break;
                    case 2:
                        transform.rotation = _rot;
                        transform.Translate(Vector2.down * e_speed * Time.deltaTime, Space.World);
                        break;
                    case 3:
                        transform.rotation = _rot;
                        transform.Translate(Vector2.up * e_speed * Time.deltaTime, Space.Self);
                        break;
                    default:
                        break;
                }
            }
            else if (iter % 120 == 0)
            {
                switch (b_Type)
                {
                    case 1:
                        Instantiate(e_Bullet, transform.position, Quaternion.Euler(0, 0, 0));
                        break;
                    case 2:
                        //_rot.z -= 180;
                        Instantiate(e_Bullet, transform.position, _rot);
                        break;
                    case 3:
                        Instantiate(e_Bullet, transform.position, Quaternion.Euler(0, 0, _angle + 165));
                        Instantiate(e_Bullet, transform.position, Quaternion.Euler(0, 0, _angle + 195));
                        break;
                    case 4:
                        for (int i = 0; i < 5; i++)
                        {
                            Instantiate(e_Bullet, transform.position, Quaternion.Euler(0, 0, _angle + 140 + i * 20));
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        switch (e_Type)
        {
            case 1:
                transform.Translate(Vector2.down * e_speed * Time.deltaTime, Space.World);
                break;
            case 2:
                transform.rotation = _rot;
                transform.Translate(Vector2.down * e_speed * Time.deltaTime, Space.World);
                break;
            case 3:
                transform.rotation = _rot;
                transform.Translate(Vector2.up * e_speed * Time.deltaTime, Space.Self);
                break;
            default:
                break;
        }

        //transform.Translate(Vector2.down * e_speed * Time.deltaTime, Space.World);

        //if (iter % 60 == 0)
        //{
        //    Instantiate(e_Bullet, transform.position, Quaternion.Euler(0,0,0));
        //}
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "p_bullet" && !animator.GetBool("isBoom"))
        {
            health -= 1;
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;

            if (health <= 0)
            {
                animator.SetBool("isBoom", true);

                Vector2 prePos = transform.position;
                Quaternion quaternion = transform.rotation;

                Instantiate(item, prePos, quaternion);
                prePos.x += 0.5f;
                Instantiate(coin, prePos, quaternion);
                item.GetComponent<SpriteRenderer>().color = Color.yellow;

                GameObject.Find("GameManager").GetComponent<gameManagerCtrl>().Score += 5;

                Destroy(gameObject, 1);
            }
            else
            {
                Invoke("ReturnToOrignalSprite", 1);
            }
        }
    }

    private void ReturnToOrignalSprite()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }
}
