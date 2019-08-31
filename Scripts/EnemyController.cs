using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
bool isHurt=false;
    public GameObject Player;
    float lastHurt;
    Animator thisAnim;
    // Start is called before the first frame update
    void Start()
    {
        thisAnim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(this.transform.position,Player.transform.position) < 4 && !Player.GetComponent<PlayerController>().isDead){
            float xDist = Mathf.Abs(this.transform.position.x-Player.transform.position.x);
            float yDist = Mathf.Abs(this.transform.position.y-Player.transform.position.y);

            if(!isHurt){
                if(xDist>yDist){
                    setFalse(thisAnim);
                    thisAnim.SetBool("walkLeft",true);
                }else{
                    setFalse(thisAnim);
                    if(this.transform.position.y<Player.transform.position.y){
                        thisAnim.SetBool("walkUp",true);
                    }else{
                        
                        thisAnim.SetBool("walkDown",true);
                    }
                }
          }
            if(this.transform.position.x<Player.transform.position.x){
                this.GetComponent<SpriteRenderer>().flipX = true;
            }else{
                this.GetComponent<SpriteRenderer>().flipX = false;
            }


            if(!isHurt){
                this.transform.position = new Vector2(Mathf.Lerp(this.transform.position.x,Player.transform.position.x,0.01f),Mathf.Lerp(this.transform.position.y,Player.transform.position.y,0.01f));
            }
        }else{
            setFalse(thisAnim);
            if(isHurt){
                thisAnim.SetBool("IsHurt",true);
            }
        }
        if(lastHurt < Time.realtimeSinceStartup-1.5f){
            isHurt=false;
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.name =="Explosion"||coll.gameObject.name=="Slash"){
            if(lastHurt<Time.realtimeSinceStartup){
                if(isHurt){
                    Destroy(this.gameObject);
                }
                   setFalse(thisAnim);
                thisAnim.SetBool("IsHurt",true);
                isHurt=true;
                lastHurt=Time.realtimeSinceStartup+0.5f;
                this.gameObject.GetComponent<Rigidbody2D>().AddForce((this.transform.position-coll.transform.position)*15,ForceMode2D.Impulse);
            }
        }
      

    }
           void setFalse(Animator timeReflectionAnimator){
         foreach(AnimatorControllerParameter parameter in timeReflectionAnimator.parameters) {   
          
                 timeReflectionAnimator.SetBool(parameter.name, false);
          
        }
           }
}
