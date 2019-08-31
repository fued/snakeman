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

    public float moveSpeed;
   public  enum direction{left,right,up,down};
    direction CurrentDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(CurrentSlash==null){//cant slash and move
        if(Input.GetKey(KeyCode.W)){
            this.transform.position = new Vector2(this.transform.position.x,this.transform.position.y+moveSpeed);
            CurrentDirection=direction.up;
        }
        if(Input.GetKey(KeyCode.S)){
            this.transform.position = new Vector2(this.transform.position.x,this.transform.position.y-moveSpeed);
           
              CurrentDirection=direction.down;
              
        }
        if(Input.GetKey(KeyCode.A)){
            this.transform.position = new Vector2(this.transform.position.x-moveSpeed,this.transform.position.y);
               CurrentDirection=direction.left;
           thisRender.flipX = false;
        }
          if(Input.GetKey(KeyCode.D)){
            this.transform.position = new Vector2(this.transform.position.x+moveSpeed,this.transform.position.y);
                    CurrentDirection=direction.right;
                          thisRender.flipX = true;
        }

            if(Input.GetKey(KeyCode.Space) ){
      
                CurrentSlash  = GameObject.Instantiate(Slash);
              CurrentSlash.name = "Slash";
                CurrentSlash.transform.rotation = this.transform.rotation;
                 CurrentSlash.GetComponent<SlashController>().CurrentDirection = CurrentDirection;

                switch(CurrentDirection)
                {
                    case direction.up:
                        CurrentSlash.transform.position = new Vector2(this.transform.position.x,this.transform.position.y+0.66f);
                    break;
                    case direction.down:
                        CurrentSlash.transform.position = new Vector2(this.transform.position.x,this.transform.position.y-0.66f);
                    break;
                    case direction.left:
                        CurrentSlash.transform.position = new Vector2(this.transform.position.x-0.66f,this.transform.position.y);
                    break;
                    case direction.right:
                        CurrentSlash.transform.position = new Vector2(this.transform.position.x+0.66f,this.transform.position.y);
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
