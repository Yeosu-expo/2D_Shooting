using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject Enemy01;
    public GameObject Enemy02;
    public GameObject Enemy03;

    public int maxEnemy = 10;
    private float createTime = 1.0f;

    public bool isGameover = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.LogWarning("Already Exist.");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CreateEnemy(int a)
    {
        if (a == 0)
        {
            GameObject.Instantiate(Enemy01);

        }

        else if (a == 1)
        {

            GameObject.Instantiate(Enemy02);
        }
        else
        {
            GameObject.Instantiate(Enemy03);
        }
    }

    //IEnumerator GenerateEnemy()
    //{

    //}
}
