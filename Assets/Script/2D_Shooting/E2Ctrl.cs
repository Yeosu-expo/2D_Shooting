using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class E2Ctrl : MonoBehaviour
{
    [SerializeField] private int e_speed;
    [SerializeField] private int e_Type;

    private int iter = 0;
    private float attackPoint;
    private Transform targetTr;

    public GameObject e_Bullet;
    public Transform firePos; // 총알이 발사될 위치

    private float t = 0f;


    public Transform[] wayPoints;
    private Vector2 gizmoPosition;

    private Animator animator;
    public GameObject item;

    // Start is called before the first frame update
    void Start()
    {
        e_speed = 1;
        attackPoint = Random.Range(2.0f, 4.0f);

        e_Bullet = GameObject.Find("Enemy Bullet 2");
        firePos = transform.Find("E Fire Position2").transform;

        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // StartCoroutine(BaizerCurve());
        iter++;

        targetTr = GameObject.FindWithTag("Player").GetComponent<Transform>();
        Vector2 attackDirection = (targetTr.position - transform.position).normalized;
        //transform.Translate(attackDirection * e_speed * Time.deltaTime, Space.World);
        float _angle = Mathf.Atan2(attackDirection.x, -attackDirection.y) * Mathf.Rad2Deg;
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
            //else if (iter % 120 == 0)
            //{
            //    switch (e_Type)
            //    {
            //        case 1:
            //            Instantiate(e_bullet, transform.position, Quaternion.Euler(0, 0, 180));
            //            break;
            //        case 2:
            //            Instantiate(e_bullet, transform.position, Quaternion.Euler(0, 0, 180));
            //            break;
            //        case 3:
            //            Instantiate(e_bullet, transform.position, Quaternion.Euler(0, 0, 180));
            //            break;
            //        default:
            //            break;
            //    }
            //}
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

        transform.Translate(Vector2.down * e_speed * Time.deltaTime, Space.World);

        if (iter % 60 == 0)
        {
            Instantiate(e_Bullet, transform.position, Quaternion.Euler(0, 0, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "p_bullet")
        {
            Debug.Log("Attacked!");
            animator.SetBool("isBoom", true);

            Vector2 prePos = transform.position;
            Quaternion quaternion = transform.rotation;

            Instantiate(item, prePos, quaternion);

            Destroy(gameObject, 1);
        }
    }

    private IEnumerator BaizerCurve()
    {
        t += Time.deltaTime * e_speed;

        while (t < 1)
        {
            gizmoPosition =
                Mathf.Pow(1 - t, 3) * wayPoints[0].position
                + 3 * t * Mathf.Pow(1 - t, 2) * wayPoints[1].position
                + 3 * t * (1 - t) * wayPoints[2].position
                + Mathf.Pow(t, 3) * wayPoints[3].position;

            transform.position = gizmoPosition;
            yield return new WaitForEndOfFrame();
        }

        t = 0f;
    }
}
