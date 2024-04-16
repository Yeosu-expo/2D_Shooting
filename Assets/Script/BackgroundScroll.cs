using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    
    [SerializeField] private int bgSpeed = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * bgSpeed * Time.deltaTime, Space.Self);
        if(transform.position.y <= -12.0f)
        {
            transform.position = transform.position + new Vector3(0f, 24.0f, 0f);
        }
    }
}
