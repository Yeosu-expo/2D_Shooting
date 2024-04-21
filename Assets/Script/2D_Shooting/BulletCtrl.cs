using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BulletCtrl : MonoBehaviour
{
    [SerializeField] private int bulletSpeed = 10;
    public int bulletDamage = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up*bulletSpeed*Time.deltaTime , Space.Self);

    }
}
