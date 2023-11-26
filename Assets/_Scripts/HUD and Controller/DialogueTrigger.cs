using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private int nextLevel;
    [SerializeField] private bool loadOtherLevel;

    //[SerializeField] private Collider col;
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private Dialogue[] dialogues;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Entrou");
            if(!dialogueController.dialogueIsActive)
            {
                dialogueController.loadOtherLevel = this.loadOtherLevel;
                dialogueController.dialogues.Clear();
                for (int i = 0; i < dialogues.Length; i++)
                {
                    dialogueController.dialogues.Add(dialogues[i]);
                }
                dialogueController.ShowDialogue();
				Debug.Log("Passou");


				if (loadOtherLevel)
                {
                    dialogueController.nextLevel = this.nextLevel;
                }
            }

        }
    }

}
