using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float maxLifeTime_ = 1;
    private float time_ = 0;
    // Start is called before the first frame update
    void Start()
    {
        time_  = maxLifeTime_;
        
    }

    // Update is called once per frame
    void Update()
    {

        time_ -= Time.deltaTime;
        if(time_ > 0) { return; }
        Destroy(gameObject);
        
    }
}
