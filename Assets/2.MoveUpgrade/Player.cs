using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject avatar;
    
    void Start()
    {
        Animate();
        Debug.Log("크큭");
        Debug.Log("ㅋㅋ루삥뽕");
    }
    void Update()
    {
        
    }

    void Animate() {
        avatar.GetComponent<Animation>().Play();
    }
}
