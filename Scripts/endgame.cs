using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endgame : MonoBehaviour
{

    public DialogueSpawner finished;
    public Sprite Shocked;
    public Sprite Happy;
    public Sprite RedLive;
    public Sprite Pantha;
public SpriteRenderer mainscreen;
    // Start is called before the first frame update
    void Start()
    {
        if(GameStateManager.Get().GetClues()==0||GameStateManager.Get().GetKills()>0)
        {
            finished._text = "...Me!";
        }
        if(GameStateManager.Get().GetClues()>0 )
        {
            finished._text = "A Pantha!";  
            finished._portrait = Shocked;
             if(GameStateManager.Get().GetClues()==5 )
            {
                     finished._text = "...Redd. He isn't dead! He's just dehydrated!"; 
                     finished._portrait = Happy; 
            }
        }


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
