using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StartPannelCtrl : MonoBehaviour
{
    public Button button;
    // Start is called before the first frame update
    void Start()
    {
        button.onClick.AddListener(GameStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameStart()
    {
        gameObject.SetActive(false);
        GameObject.Find("GameManager").GetComponent<gameManagerCtrl>().gameStartSign = true;
    }
}
