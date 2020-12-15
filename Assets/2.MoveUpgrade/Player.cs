using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject avatar;
    
    void Start()
    {
        Animate();
    }

    void Update()
    {

    }

    void Animate() {
        avatar.GetComponent<Animation>().Play();
    }
}
