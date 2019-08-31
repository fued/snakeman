using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashController : MonoBehaviour
{

        public  PlayerController.direction CurrentDirection;
    public float LifeTime = 0.5f;
    float alivetime;
    // Start is called before the first frame update
    void Start()
    {
        alivetime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.realtimeSinceStartup>alivetime+LifeTime){
            Destroy(this.gameObject);
        }
    }
}
