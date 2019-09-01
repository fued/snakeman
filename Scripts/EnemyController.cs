using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
bool isHurt=false;
public string enemyType="default";
public int detectionRange=4;
    public GameObject Player;
    Vector2 StartPosition;
    float lastHurt;
    Animator thisAnim;
    float pantheroffset;
    // Start is called before the first frame update
    void Start()
    {
        
        pantheroffset = Random.Range(-0.99f,0.99f);
        StartPosition = this.transform.position;
        thisAnim = this.GetComponent<Animator>();
        Player =   GameObject.Find("character");
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(this.transform.position,Player.transform.position) < detectionRange && !Player.GetComponent<PlayerController>().isDead){
            float xDist = Mathf.Abs(this.transform.position.x-Player.transform.position.x);
            float yDist = Mathf.Abs(this.transform.position.y-Player.transform.position.y);

            if(!isHurt){
                if(enemyType=="default"){
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
                if(enemyType=="panther"){
                          setFalse(thisAnim);
                    thisAnim.SetBool("walkLeft",true);
                }
                if(enemyType=="hidden"){
                        setFalse(thisAnim);
                        thisAnim.SetBool("inRange",true);
                }
          }
    


            if(!isHurt){
                if(enemyType=="default"){
                            if(this.transform.position.x<Player.transform.position.x){
                this.GetComponent<SpriteRenderer>().flipX = true;
            }else{
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
                this.transform.position = new Vector2(Mathf.Lerp(this.transform.position.x,Player.transform.position.x,0.01f),Mathf.Lerp(this.transform.position.y,Player.transform.position.y,0.01f));
                }
                   if(enemyType=="panther"){
                                         if(this.transform.position.x<Player.transform.position.x-(Mathf.Sin(Time.realtimeSinceStartup+pantheroffset)*10)){
                this.GetComponent<SpriteRenderer>().flipX = true;
            }else{
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
                this.transform.position = new Vector2(Mathf.Lerp(this.transform.position.x,Player.transform.position.x-(Mathf.Sin(Time.realtimeSinceStartup)*10),0.04f),this.transform.position.y);
                }
                if(enemyType=="hidden"){
                                          if(this.transform.position.x<Player.transform.position.x){
                this.GetComponent<SpriteRenderer>().flipX = true;
            }else{
                this.GetComponent<SpriteRenderer>().flipX = false;
            }
                this.transform.position = new Vector2(Mathf.Lerp(this.transform.position.x,Player.transform.position.x,0.015f),Mathf.Lerp(this.transform.position.y,Player.transform.position.y,0.015f));
             
                }
            }
        }else{
            if(enemyType!="hidden"){
            setFalse(thisAnim);
            if(isHurt){
                thisAnim.SetBool("IsHurt",true);
            }
            }else{
     setFalse(thisAnim);
            if(isHurt){
                thisAnim.SetBool("IsHurt",true);
            }
           if( Vector2.Distance(this.transform.position,StartPosition) >0.5f){
   this.transform.position = new Vector2(Mathf.Lerp(this.transform.position.x,StartPosition.x,0.01f),Mathf.Lerp(this.transform.position.y,StartPosition.y,0.01f));
           }else{
                thisAnim.SetBool("walkBack",true);
           }
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
                if(isHurt||enemyType=="panther"){
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
