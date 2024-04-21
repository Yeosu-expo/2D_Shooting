using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FireCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
           
    }

    public void PointerDown()
    {
        GameObject.Find("Player").GetComponent<PlayerCtrl>().isFire = true;
    }
    
    public void PointerUp()
    {
        GameObject.Find("Player").GetComponent<PlayerCtrl>().isFire = false;
    }
}
