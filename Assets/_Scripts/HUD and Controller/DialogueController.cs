using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [Header ("Configs")]
    private int currentDialogue = 0;
    private int currentMessage = 0;
    private string[] textsDialogue;
    [SerializeField] float speedTextTransition;

    [Header("Link UI Objects")]
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TextMeshProUGUI talkerName;
    [SerializeField] private Image rightImageUI;
    [SerializeField] private Image  leftImageUI;
    [SerializeField] private TextMeshProUGUI textAreaUI;
    [SerializeField] private Button nextBT;
    [SerializeField] private Button backBT;

    [Header("Controllers")]
    public int nextLevel;
    public bool loadOtherLevel = false;
    public bool dialogueIsActive = false;
   
    [Header("Dialogues")]
    public List<Dialogue> dialogues;
   
    private void Awake()
    {
        backBT.interactable = false;
        if(dialogues.Count>0)
        {
            UpdateParameters(dialogues[0]);
        }
    }

    public void UpdateParameters(Dialogue dialogue)
    {
        textAreaUI.text = "";
        talkerName.text = dialogue.talkerName;
        if(dialogue.isRightSide)
        {
            this.rightImageUI.sprite = dialogue.sprite;
            this.rightImageUI.enabled = true;
            this.leftImageUI.enabled = false;
            
        }
        else
        {
            this.leftImageUI.sprite = dialogue.sprite;
            this.rightImageUI.enabled = false;
            this.leftImageUI.enabled = true;
        }
        //this.texto.text = dialogo.messagens[messagemAtual];
        textsDialogue = dialogue.messages; //
        StartCoroutine(WriteText());
    }

    public void NextMessage()
    {
        backBT.interactable = true;

        if(currentDialogue < dialogues.Count)
        {
            if(currentMessage< dialogues[currentDialogue].messages.Length-1)
            {
                currentMessage++;
                StartCoroutine(WriteText());
                //texto.text = dialogos[dialogoAtual].messagens[messagemAtual];
            }
            else if (currentDialogue < dialogues.Count-1)
            {
                currentDialogue++;
                currentMessage = 0;
                UpdateParameters(dialogues[currentDialogue]);
            }

            if (currentMessage == dialogues[currentDialogue].messages.Length - 1 && currentDialogue == dialogues.Count - 1)
            {
                //Debug.Log("Cheguei no limite");
                nextBT.interactable = false;
            }
        }

        //Debug.Log("Messagem Atual: " + messagemAtual + "; Dialogo Atual: " + dialogoAtual);
    }

    public void BackMessage()
    {
        nextBT.interactable = true;

        if (currentMessage>0)
        {
            currentMessage--;
            //texto.text = dialogos[dialogoAtual].messagens[messagemAtual];

            StartCoroutine(WriteText());
           //Debug.Log("MessagemAtual: " + messagemAtual);
        }
        else if(currentDialogue>0)
        {
            currentDialogue--;
            currentMessage = dialogues[currentDialogue].messages.Length - 1;
            UpdateParameters(dialogues[currentDialogue]);
        }

        if(currentMessage <= 0 && currentDialogue<=0)
        {
            //Debug.Log("Cheguei ao limite papah");
            backBT.interactable = false;
        }
        //Debug.Log("Messagem Atual: " + messagemAtual + "; Dialogo Atual: " + dialogoAtual);
    }

    IEnumerator WriteText()
    {
        textAreaUI.text = "";
        int messagemAtiva = currentMessage;
        //Debug.Log(currentMessage);
        foreach (char letra in textsDialogue[currentMessage].ToCharArray())
        {
            if(messagemAtiva == currentMessage)
            {
                textAreaUI.text += letra;
                yield return new WaitForSeconds(speedTextTransition);
            }
        }
    }

    public void ShowDialogue()
    {
        currentMessage = 0;
        currentDialogue = 0;
        backBT.interactable = false;
        nextBT.interactable = true;
        UpdateParameters(dialogues[0]);  
        dialogueCanvas.SetActive(true);
        dialogueIsActive = true;
    }

    public void SkipBT()
    {
        dialogueCanvas.SetActive(false);
        dialogueIsActive = false;
        if(loadOtherLevel)
        {
            SceneManager.LoadScene(nextLevel);
        }
    }
    //dialogos[dialogoAtual].messagens[messagemAtual]
}

