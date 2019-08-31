using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
public GameObject Slash;
public GameObject Bomb;
public Animator thisAnimator;
public SpriteRenderer thisRender;
GameObject CurrentBomb;
GameObject CurrentSlash;
float isHurt=0;

    public float moveSpeed;
   public  enum direction{left,right,up,down};
    direction CurrentDirection;
    enum animation{left,up,down,walkLeft,walkDown,walkUp}
    animation currentAnimation;
    bool isSwinging=false;
    public bool isDead=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead){

        if(isHurt>Time.realtimeSinceStartup){
                  setFalse(thisAnimator);
            thisAnimator.SetBool("IsHurt",true);
                thisRender.color = new Color(1,0.66f,0.66f);
              if(isHurt-1>Time.realtimeSinceStartup){
            
            thisRender.color = new Color(1,0.5f,0.5f);
              }
           
            moveSpeed=0.02f;
        }else{
              thisRender.color = Color.white;
            moveSpeed=0.05f;
        }

        if(!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)&& !Input.GetKey(KeyCode.A)&& !Input.GetKey(KeyCode.D)&& isHurt<Time.realtimeSinceStartup+1)
        {
         
            setIdle(thisAnimator);
        }
       
        if(isSwinging==false && isHurt<Time.realtimeSinceStartup+1){//cant slash and move
            if(Input.GetKey(KeyCode.W)){
                this.transform.position = new Vector2(this.transform.position.x,this.transform.position.y+moveSpeed);
                 
                setFalse(thisAnimator);
                thisAnimator.SetBool("walkUp",true);
                currentAnimation=animation.walkUp;
                
                     CurrentDirection=direction.up;
            }
            if(Input.GetKey(KeyCode.S)){
                this.transform.position = new Vector2(this.transform.position.x,this.transform.position.y-moveSpeed);
                   
                currentAnimation=animation.walkDown;
                  setFalse(thisAnimator);
                  thisAnimator.SetBool("walkDown",true);
                
                CurrentDirection=direction.down;
                
            }
            if(Input.GetKey(KeyCode.A)){
 
                this.transform.position = new Vector2(this.transform.position.x-moveSpeed,this.transform.position.y);
                
                CurrentDirection=direction.left;
                  setFalse(thisAnimator);
              thisAnimator.SetBool("walkSideways",true);
          
            thisRender.flipX = false;
            }
            if(Input.GetKey(KeyCode.D)){
                this.transform.position = new Vector2(this.transform.position.x+moveSpeed,this.transform.position.y);
       
                CurrentDirection=direction.right;
                  setFalse(thisAnimator);
                thisAnimator.SetBool("walkSideways",true);
               
                thisRender.flipX = true;
            }

            if(Input.GetKey(KeyCode.Space) ){
        
               isSwinging=true;
                StartCoroutine("SlashWait");
                switch(CurrentDirection)
                {
                    case direction.up:
                      setFalse(thisAnimator);
              thisAnimator.SetBool("swingUp",true);
                    break;
                    case direction.down:
                          setFalse(thisAnimator);
              thisAnimator.SetBool("swingDown",true);
                    break;
                    case direction.left:
                          setFalse(thisAnimator);
              thisAnimator.SetBool("swingLeft",true);

                    break;
                    case direction.right:
                              setFalse(thisAnimator);
              thisAnimator.SetBool("swingLeft",true);
       
                    break;

                }
            }
            if(Input.GetKey(KeyCode.LeftShift) ){
                if(CurrentBomb==null){
                    CurrentBomb  = GameObject.Instantiate(Bomb);
                
                    switch(CurrentDirection)
                    {
                        case direction.up:
                            CurrentBomb.transform.position = new Vector2(this.transform.position.x,this.transform.position.y+0.66f);
                        break;
                        case direction.down:
                            CurrentBomb.transform.position = new Vector2(this.transform.position.x,this.transform.position.y-0.66f);
                        break;
                        case direction.left:
                            CurrentBomb.transform.position = new Vector2(this.transform.position.x-0.66f,this.transform.position.y);
                        break;
                        case direction.right:
                            CurrentBomb.transform.position = new Vector2(this.transform.position.x+0.66f,this.transform.position.y);
                        break;

                    }
                }
            }
        }
        }
    }
       void setFalse(Animator timeReflectionAnimator){
    
         foreach(AnimatorControllerParameter parameter in timeReflectionAnimator.parameters) {   
          
                 timeReflectionAnimator.SetBool(parameter.name, false);
          
    
        }
    }    void setIdle(Animator timeReflectionAnimator){
         foreach(AnimatorControllerParameter parameter in timeReflectionAnimator.parameters) {   
             if(parameter.name.Contains("walk") && timeReflectionAnimator.GetBool(parameter.name)==true){         
                 Debug.Log(parameter.name);
             
                 timeReflectionAnimator.SetBool(parameter.name, false);
                timeReflectionAnimator.SetBool(parameter.name.Replace("walk","idle"), true);
              
             }            
        }
    }
  void OnCollisionEnter2D(Collision2D col){
      if(col.gameObject.name=="Enemy"){

          
                    if(isHurt>Time.realtimeSinceStartup){
                  isDead=true;
                    setFalse(thisAnimator);
                       thisAnimator.SetBool("IsDead",true);
                       thisRender.color = Color.white;
                }else{

           thisAnimator.SetBool("IsHurt",true);
        isHurt=Time.realtimeSinceStartup+2;
      this.gameObject.GetComponent<Rigidbody2D>().AddForce((this.transform.position-col.transform.position)*15,ForceMode2D.Impulse);
      

                }
      }

    }

    IEnumerator SlashWait() 
    {
       
               yield return new WaitForSeconds(0.33f);
    
                CurrentSlash  = GameObject.Instantiate(Slash);
                CurrentSlash.name = "Slash";
             
                CurrentSlash.GetComponent<SlashController>().CurrentDirection = CurrentDirection;

         switch(CurrentDirection)
                {
                    case direction.up:
                         setFalse(thisAnimator);
                  thisAnimator.SetBool("idleUp",true);
                        CurrentSlash.transform.position = new Vector2(this.transform.position.x,this.transform.position.y+0.66f);
                           CurrentSlash.transform.eulerAngles = new Vector3(0,0,0);
                    break;
                    case direction.down:
                         setFalse(thisAnimator);
                  thisAnimator.SetBool("idleDown",true);
                        CurrentSlash.transform.position = new Vector2(this.transform.position.x,this.transform.position.y-0.66f);
                             CurrentSlash.transform.eulerAngles = new Vector3(0,0,180);
                             CurrentSlash.GetComponent<SpriteRenderer>().flipX = true;
                    break;
                    case direction.left:
                           setFalse(thisAnimator);
                  thisAnimator.SetBool("idleSideways",true);
                        CurrentSlash.transform.position = new Vector2(this.transform.position.x-0.66f,this.transform.position.y);
                             CurrentSlash.transform.eulerAngles = new Vector3(0,0,90);
                    break;
                    case direction.right:
                           setFalse(thisAnimator);
                  thisAnimator.SetBool("idleSideways",true);
                        CurrentSlash.transform.position = new Vector2(this.transform.position.x+0.66f,this.transform.position.y);
                             CurrentSlash.transform.eulerAngles = new Vector3(0,0,270);
                              CurrentSlash.GetComponent<SpriteRenderer>().flipX = true;
                    break;

                }
             
          isSwinging=false;
    }

}
