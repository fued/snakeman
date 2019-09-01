using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallscript : MonoBehaviour
{
    public GameObject tilemaphidden;
       public GameObject tilemaphidden2;
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
              AudioManager.Get().PlaySfxOnce(AudioManager.SFX.Secret_Opening_Area);
            this.GetComponent<SpriteRenderer>().enabled = false;
            this.GetComponent<BoxCollider2D>().enabled=false;
            tilemaphidden.GetComponent<UnityEngine.Tilemaps.TilemapRenderer>().enabled=true;
            tilemaphidden2.GetComponent<UnityEngine.Tilemaps.TilemapRenderer>().enabled=true;
          }
        }
}
