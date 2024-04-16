using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_bulletCtrl : MonoBehaviour
{
    [SerializeField] private int bulletSpeed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * bulletSpeed * Time.deltaTime, Space.Self);
    }
}
