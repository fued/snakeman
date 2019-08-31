using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombController : MonoBehaviour
{
public GameObject Explosion;
public float LifeTime;
float alivetime;
  
    // Start is clled before the first frame update
    void Start()
    {
        alivetime = Time.realtimeSinceStartup+LifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.realtimeSinceStartup>alivetime){
           GameObject explosion = GameObject.Instantiate(Explosion);
           explosion.transform.position = this.transform.position;
           explosion.name = "Explosion";
           Destroy(this.gameObject);
        }
        float timeLeft = alivetime-Time.realtimeSinceStartup;
   
        if(timeLeft<2){
            this.GetComponent<SpriteRenderer>().color =Color.white;
            if(timeLeft<1.75f){
                 this.GetComponent<SpriteRenderer>().color =Color.red;
                if(timeLeft<1.5f){
                    this.GetComponent<SpriteRenderer>().color =Color.white;
                    if(timeLeft<1.25f){
                         this.GetComponent<SpriteRenderer>().color =Color.red;
                        if(timeLeft<1f){
                                 this.GetComponent<SpriteRenderer>().color =Color.white;
                            if(timeLeft<0.85f){
                                 this.GetComponent<SpriteRenderer>().color =Color.red;
                                if(timeLeft<0.7f){
                                         this.GetComponent<SpriteRenderer>().color =Color.white;
                                    if(timeLeft<0.55f){
 this.GetComponent<SpriteRenderer>().color =Color.red;
  if(timeLeft<0.4f){
 this.GetComponent<SpriteRenderer>().color =Color.white;
   if(timeLeft<0.25f){
 this.GetComponent<SpriteRenderer>().color =Color.red;
    if(timeLeft<0.1f){
 this.GetComponent<SpriteRenderer>().color =Color.white;
    }
   }
  }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D coll){

        if(coll.gameObject.name=="Slash"){
      
            switch(coll.gameObject.GetComponent<SlashController>().CurrentDirection){
                case PlayerController.direction.up:
               
                   this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,15000),ForceMode2D.Impulse);
                break;
                       case PlayerController.direction.down:
               
                   this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0,-15000),ForceMode2D.Impulse);
                break;
                       case PlayerController.direction.left:
               
                   this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(-15000,0),ForceMode2D.Impulse);
                break;
                       case PlayerController.direction.right:
               
                   this.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(15000,0),ForceMode2D.Impulse);
                break;
            }
        }
    }
}
