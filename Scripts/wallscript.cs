using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallscript : MonoBehaviour
{
    public TilesetRenderer hiddenFloor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void OnTriggerEnter2D(Collider2D coll)
    {
          if(coll.gameObject.name =="Explosion"){
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled=false;
          }
        }
}
