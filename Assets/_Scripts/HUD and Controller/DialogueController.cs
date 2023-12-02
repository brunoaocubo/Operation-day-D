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

    [Header("Dialogue UI Objects")]
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TextMeshProUGUI textAreaUI;
    [SerializeField] private Button nextBT;
    [SerializeField] private Button backBT;

    [Header("Talker Left")]
    [SerializeField] private GameObject talkerEmptyLeft;
    [SerializeField] private Image leftImageUI;
    [SerializeField] private TextMeshProUGUI talkerNameTextLeft;
   

    [Header("Talker Right")]
    [SerializeField] private GameObject talkerEmptyRight;
    [SerializeField] private Image rightImageUI;
    [SerializeField] private TextMeshProUGUI talkerNameTextRight;


    [Header("Controllers")]
    public int nextLevel;
    public bool loadOtherLevel = false;
    public bool dialogueIsActive = false;
   
    [Header("Dialogues")]
    public List<Dialogue> dialogues;

    [SerializeField] private GameObject canvasHUD;
    [SerializeField] private GameObject canvasInputs;
    [SerializeField] private RectTransform handleJoystick;

	private void Awake()
    {
        backBT.interactable = false;
    }

	public void UpdateParameters(Dialogue dialogue)
    {
        textAreaUI.text = "";
        
        if (dialogue.isRightSide)
        {
            textAreaUI.horizontalAlignment = HorizontalAlignmentOptions.Right;
            talkerNameTextRight.text = dialogue.talkerName;
            this.rightImageUI.sprite = dialogue.sprite;
            this.rightImageUI.enabled = true;
            this.talkerEmptyLeft.SetActive(false);
            this.talkerEmptyRight.SetActive(true);
            //this.leftImageUI.enabled = false;
        }
        else
        {
            textAreaUI.horizontalAlignment = HorizontalAlignmentOptions.Left;
            talkerNameTextLeft.text = dialogue.talkerName;
            this.leftImageUI.sprite = dialogue.sprite;
            this.rightImageUI.enabled = false;
            this.talkerEmptyRight.SetActive(false);
            this.talkerEmptyLeft.SetActive(true);
        }

        textsDialogue = dialogue.messages;
        StartCoroutine(WriteText());
    }

    public void NextMessage()
    {
        StopAllCoroutines();
        backBT.interactable = true;

        if(currentDialogue < dialogues.Count)
        {
            if(currentMessage< dialogues[currentDialogue].messages.Length-1)
            {
                currentMessage++;
                StartCoroutine(WriteText());
            }
            else if (currentDialogue < dialogues.Count-1)
            {
                currentDialogue++;
                currentMessage = 0;
                UpdateParameters(dialogues[currentDialogue]);
            }

            if (currentMessage == dialogues[currentDialogue].messages.Length - 1 && currentDialogue == dialogues.Count - 1)
            {
                nextBT.interactable = false;
            }
        }
    }

    public void BackMessage()
    {
        StopAllCoroutines();
        nextBT.interactable = true;

        if (currentMessage>0)
        {
            currentMessage--;

            StartCoroutine(WriteText());
        }
        else if(currentDialogue>0)
        {
            currentDialogue--;
            currentMessage = dialogues[currentDialogue].messages.Length - 1;
            UpdateParameters(dialogues[currentDialogue]);
        }

        if(currentMessage <= 0 && currentDialogue<=0)
        {
            backBT.interactable = false;
        }
    }

    IEnumerator WriteText()
    {
        textAreaUI.text = "";
        int messagemAtiva = currentMessage;
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
		canvasHUD.SetActive(false);
        canvasInputs.SetActive(false);
        handleJoystick.localPosition = Vector3.zero;
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
}

