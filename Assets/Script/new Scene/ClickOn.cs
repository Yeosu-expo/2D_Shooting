using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickOn : MonoBehaviour
{
    private SpriteRenderer myRenderer;

    [HideInInspector]
    public bool currentSelected = false; // 최근 선택되었는지
    // Start is called before the first frame update
    void Start()
    {
        myRenderer = GetComponent<SpriteRenderer>(); // 객체 할 

        Camera.main.gameObject.GetComponent<Click>().selectableObjects.Add(this.gameObject);// 카메라의 선택될 수 있는 오브젝트에 자신을 추가

        ClickMe(); // 선택되었다면 초록색으로 , 그렇지 않으면 빨간색 
    }

    // Update is called once per frame
    public void ClickMe() // currentSelected에 따라 색깔 변경
    {
        if(currentSelected == false)
        {
            myRenderer.color = Color.red; 
        }
        else
        {
            myRenderer.color = Color.green;
        }
    }
}
