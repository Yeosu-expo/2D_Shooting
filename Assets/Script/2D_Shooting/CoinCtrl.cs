using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CoinCtrl : MonoBehaviour
{
    public GameObject p_bullet_upgraded;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            player.GetComponent<PlayerCtrl>().wallet += 1;
            Destroy(gameObject);
        }
    }
}
