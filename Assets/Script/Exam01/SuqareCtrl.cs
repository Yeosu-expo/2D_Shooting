using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuqareCtrl : MonoBehaviour
{
    private string param;
    private Animator animator;
    private SpriteRenderer sRenderer;
    [SerializeField] private int speed;
    //private Renderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        param = "clickType";
        animator = gameObject.GetComponent<Animator>();
        speed = 5;
        sRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //renderer = gameObject.GetComponent<Renderer>();
        if (sRenderer.color == Color.red)
        {
            moving();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);
          
            if(hit.collider.name == gameObject.name)
            {
                GameObject obj = hit.transform.gameObject;
                if(!obj.name.Contains("Box"))
                {
                    return;
                }
                
                if (animator.GetInteger(param) == 0)
                {
                    Debug.Log("0");
                    
                    animator.SetInteger(param, 1);
                    sRenderer.color = Color.green;
                    return;
                }

                if (animator.GetInteger(param) == 1)
                {
                    Debug.Log("1");
                    animator.SetInteger(param, 2);
                }
            }
        }
    }

    void moving()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        animator.SetFloat("p_hori", h);

        Vector3 moveDir = Vector3.up * v + Vector3.right * h;

        transform.Translate(moveDir * speed * Time.deltaTime);
    }

    void SizingHandler()
    {
        Debug.Log("2");
        gameObject.GetComponent<Animator>().SetInteger(param, 0);
        gameObject.GetComponent<SpriteRenderer>().color = Color.red;
    }
}
