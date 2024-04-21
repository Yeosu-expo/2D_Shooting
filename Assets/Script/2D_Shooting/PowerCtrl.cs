using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PowerCtrl : MonoBehaviour
{
    public GameObject p_bullet_upgraded;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        p_bullet_upgraded = GameObject.Find("p_bullet_upgraded");
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            player.GetComponent<PlayerCtrl>().powerPoint += 1;
            Destroy(gameObject);
            CheckAndUpgrade();
        }
    }

    private void CheckAndUpgrade()
    {
        if (player.GetComponent<PlayerCtrl>().powerPoint == 5 )
        {
            player.GetComponent<PlayerCtrl>().p_Bullet = p_bullet_upgraded;
        }
    }
}
