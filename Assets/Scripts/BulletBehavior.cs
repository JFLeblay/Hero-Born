using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    // Public properties
    public float onScreenDelay = 3f;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, onScreenDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
