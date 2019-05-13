using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelingScript : MonoBehaviour
{
    private BasicAI AI;
    private UIController UIControls;

    public float currentXP = 0;
    public float xpTillNextLevel = 100;
    public float STARTINGXPNEEDEDTOLEVELUP = 100;
    public int currentLevel = 1;
    public bool hasPossessed = false;

    //Make a List containing all NPC ID's and potential levels
    public List<int> NPC_Levels = new List<int>();


    // Start is called before the first frame update
    void Start()
    {
        NPC_Levels.Add(-1); //make sure the list isn't empty on load
    }

    // Update is called once per frame
    void Update()
    {
       
        if(this.gameObject.transform.parent != null)
        {
            
            AI = this.gameObject.transform.parent.gameObject.GetComponent<BasicAI>();
            UIControls = this.gameObject.transform.parent.gameObject.GetComponent<UIController>();
            if (NPC_Levels.Contains(AI.NPC_ID) && !hasPossessed) //if the NPC has been possessed before then it will be in the list
            {
                
                currentLevel = AI.currentLevel; //sets current level to whatever the npc was before it was depossessed
                xpTillNextLevel = STARTINGXPNEEDEDTOLEVELUP * (2 ^ (currentLevel - 1)); //sets xp needed to level up according to level and the formula
                hasPossessed = true; //makes sure that you are constantly setting the level to the level it started with
            }
            else if(!hasPossessed)
            {
                NPC_Levels.Add(AI.NPC_ID); //adds a new entry to the list
                currentLevel = AI.currentLevel; //probably not needed but makes sure its consistantly setting current level based off of the newly possessed NPC, which if its new, its always 1
                hasPossessed = true; //same as above
                xpTillNextLevel = STARTINGXPNEEDEDTOLEVELUP * (2 ^ (currentLevel - 1));
            }

            AI.currentLevel = currentLevel; //sets AI level to current level if being possessed
            
            

        }
        if (!this.gameObject.GetComponent<PhantomControls>().isPossessing) //if not possessing anything, allow the above code to work
        {
            hasPossessed = false;
        }

        if (currentXP >= xpTillNextLevel)
        {
            currentLevel++;
            UIControls.increaseStats();
            currentXP = 0;
            xpTillNextLevel = STARTINGXPNEEDEDTOLEVELUP * (2^(currentLevel - 1));
        }

        if (!GetComponent<PhantomControls>().isPossessing) currentXP = 0;
    }


    public void gainXP(float xpIncrease)
    {
        currentXP += xpIncrease;
    }


    public void removeID(int NPCID) //removes an entry if the npc is deceased
    {
        NPC_Levels.Remove(NPCID);
    }
   
}
