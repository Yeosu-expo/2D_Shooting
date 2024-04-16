using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectBox : MonoBehaviour
{
    [SerializeField]
    private RectTransform selectSqImage; // 드래그로 선택 영역을 나타낼 이미지

    Vector3 startPos; // 드래그 시작 위치
    Vector3 endPos; // 드래그 종료 위치

    // Start is called before the first frame update
    void Start()
    {
        selectSqImage.gameObject.SetActive(false); // 시작 시 드래그 박스를 비활성화 상태로 설정
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition; // 마우스 클릭 시 시작 위치를 설정
        }

        if (Input.GetMouseButtonUp(0))
        {
            selectSqImage.gameObject.SetActive(false); // 마우스 버튼을 놓으면 드래그 박스를 비활성화
        }

        if (Input.GetMouseButton(0))
        {
            if (!selectSqImage.gameObject.activeInHierarchy)
            {
                selectSqImage.gameObject.SetActive(true); // 마우스를 드래그 중이면 드래그 박스를 활성화
            }

            endPos = Input.mousePosition; // 마우스 위치를 드래그 종료 위치로 지속적으로 업데이트

            Vector3 squareStart = startPos;

            Vector3 centre = (squareStart + endPos) / 2f; // 드래그 시작점과 종료점의 중간 위치를 계산
            selectSqImage.position = centre; // 드래그 박스의 위치를 중간 위치로 설정

            float sizeX = Mathf.Abs(squareStart.x - endPos.x); // 드래그 박스의 너비 계산
            float sizeY = Mathf.Abs(squareStart.y - endPos.y); // 드래그 박스의 높이 계산

            selectSqImage.sizeDelta = new Vector2(sizeX, sizeY); // 계산된 너비와 높이로 드래그 박스의 크기 설정
        }
    }
}
