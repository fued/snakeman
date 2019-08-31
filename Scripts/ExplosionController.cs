using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionController : MonoBehaviour
{
        public float LifeTime = 1f;
    float alivetime;
    // Start is called before the first frame update
    void Start()
    {
             alivetime = Time.realtimeSinceStartup;
    }

    // Update is called once per frame
    void Update()
    {

        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1,1,1,1-((Time.realtimeSinceStartup-alivetime)));
        if(Time.realtimeSinceStartup>alivetime+LifeTime){
            Destroy(this.gameObject);
        }
    }
}
