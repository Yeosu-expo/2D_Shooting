using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField]
    private LayerMask clickableLayer; // 클릭 인정 범위

    private List<GameObject> selectedObjects; // 선택된 객체 리스트

    private Vector3 mousePos1, mousePos2; // 마우스 좌표. 시작, 종단
    private Vector2 worldPos;

    [HideInInspector]
    public List<GameObject> selectableObjects; // 선택될 수 있는 객체 리스트

    private void Awake()
    {
        selectedObjects = new List<GameObject>(); // 객체 할당
        selectableObjects = new List<GameObject>(); // 객체 할당
    }
  
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ClearSelection();
        }
        else if (Input.GetMouseButtonDown(0)) // 마우스를 누른 순간 
        {
            mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition); // 시작 위치 입력
        }

        else if(Input.GetMouseButtonUp(0)) // 마우스를 뗀 순간
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if(Vector3.Distance(mousePos1, mousePos2) > 0.1)
            //if(mousePos1 != mousePos2)
            {
                SelectObjects();
            }
            else // 선택범위가 작아 클릭으로 인식
            {
                worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
                RaycastHit2D rayHit = 
                    Physics2D.Raycast(worldPos, transform.forward, Mathf.Infinity, clickableLayer);

                Debug.DrawRay(worldPos, transform.forward * 20, Color.red, 3.0f);

                if (rayHit)
                /*RaycastHit rayHit;
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),
                    out rayHit, Mathf.Infinity, clickableLayer))*/
                {
                    ClickOn clickOnScript = rayHit.collider.gameObject.GetComponent<ClickOn>();

                    if (Input.GetKey("left ctrl")) // 추가 선택
                    {
                        if (rayHit.collider.GetComponent<ClickOn>().currentSelected == false) 
                        {
                            selectedObjects.Add(rayHit.collider.gameObject);
                            
                            rayHit.collider.GetComponent<ClickOn>().currentSelected = true;
                        }
                        else
                        {
                            selectedObjects.Remove(rayHit.collider.gameObject);
                            rayHit.collider.GetComponent<ClickOn>().currentSelected = false;
                        }
                        rayHit.collider.GetComponent<ClickOn>().ClickMe();
                    }
                    else // 추가 선택이 아니여서 기존의 선택된 오브젝트 클리어 후 새로운 오브젝트 선택
                    {
                        ClearSelection();

                        selectedObjects.Add(rayHit.collider.gameObject);
                        rayHit.collider.GetComponent<ClickOn>().currentSelected = true;
                        rayHit.collider.GetComponent<ClickOn>().ClickMe();
                    }
                }
            }
        }
    }

    void SelectObjects()
    {
        List<GameObject> remObjects = new List<GameObject>(); // 선택해제할 오브젝트 리스트

        if(Input.GetKey("left ctrl") == false) // left ctrl을 누르고 하지 않으면 선택 해제
        {
            ClearSelection();
        }
        Rect selectRect = new Rect(mousePos1.x, mousePos1.y,
            mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y); // 드래그 시작 , 종단을 기준으로 드래그 박스 생성 

        foreach(GameObject selectObj in selectableObjects)
        {
            if(selectObj != null)
            {
                if(selectRect.Contains(Camera.main.
                    WorldToViewportPoint(selectObj.transform.position), true)) 
                {
                    if (selectObj.GetComponent<ClickOn>().currentSelected == false) // 선택되지 않은 것 선택
                    {
                        selectedObjects.Add(selectObj);
                        selectObj.GetComponent<ClickOn>().currentSelected = true;
                        selectObj.GetComponent<ClickOn>().ClickMe();
                    }
                    else // 선택되었던 것 선택해제 및 remObjects에 추가
                    {
                        remObjects.Add(selectObj);
                        selectObj.GetComponent<ClickOn>().currentSelected = false;
                        selectObj.GetComponent<ClickOn>().ClickMe();
                    }

                    
                }
            }
            else 
            {
                remObjects.Add(selectObj);
            }
        }

        if(remObjects.Count >0) // remObjects에 있는 오브젝트 selectedObjects에서 제거
        {
            foreach (GameObject rem in remObjects)
            {
                selectedObjects.Remove(rem);
            }
            remObjects.Clear(); 
        }
    }
    void ClearSelection()
    {
        if (selectedObjects.Count > 0) // 선택된 오브젝트가 있으면
        {
            foreach(GameObject obj in selectedObjects)
            {
                if(obj != null)
                {
                    obj.GetComponent<ClickOn>().currentSelected = false; // 선택해제
                    obj.GetComponent<ClickOn>().ClickMe(); // 선택해제 정보를 반영한 색깔로 바꿈
                }
            }
            selectedObjects.Clear(); // 리스트 비우기
        }
    }
}
