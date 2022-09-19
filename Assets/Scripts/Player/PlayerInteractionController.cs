using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionController : MonoBehaviour
{
    public Animator animator;
    public SpawnProjectileScript projectileSpawner;
    private DialogueTrigger lastDialogueTriggerEntered;
    internal bool active;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Primary")){

            if (lastDialogueTriggerEntered) //Speak
            {
                lastDialogueTriggerEntered.TriggerDialogue();
            }
            else //Attack
            {
                animator.SetTrigger("Primary");
            }
        }
        if (Input.GetButtonDown("Secondary"))
        {
            projectileSpawner.SpawnAtLocation();
        }
    }

    public void AddDialougeTriggerEntered(DialogueTrigger dialogueTrigger)
    {
        lastDialogueTriggerEntered = dialogueTrigger;
    }

    public void DialougeTriggerExited(DialogueTrigger dialogueTrigger)
    {
        if(lastDialogueTriggerEntered == dialogueTrigger)
        {
            lastDialogueTriggerEntered = null;
        }
    }

}