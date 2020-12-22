using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject avatar;
    
    void Start()
    {
        Animate();
        Debug.Log("익스트림이닝");
    }

    void Animate() {
        avatar.GetComponent<Animation>().Play();
    }
}
