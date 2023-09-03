using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogoController : MonoBehaviour
{
    public Image imagemDireita,imagemEsquerda;
    public TextMeshProUGUI textoUI;
    public Button botaoDireito, botaoEsquerdo;
    public float velocidadeTexto;//
    private int messagemAtual = 0, dialogoAtual = 0;//, index = 0;
    private string[] textosDialogo;//

    
    public Dialogo[] dialogos;

    private void Awake()
    {
        botaoEsquerdo.interactable = false;
        AtualizarParametros(dialogos[0]);
    }

    public void AtualizarParametros(Dialogo dialogo)
    {
        textoUI.text = "";
        if(dialogo.fotoLadoDireito)
        {
            this.imagemDireita.sprite = dialogo.foto;
            this.imagemDireita.enabled = true;
            this.imagemEsquerda.enabled = false;
            
        }
        else
        {
            this.imagemEsquerda.sprite = dialogo.foto;
            this.imagemDireita.enabled = false;
            this.imagemEsquerda.enabled = true;
        }
        //this.texto.text = dialogo.messagens[messagemAtual];
        textosDialogo = dialogo.messagens; //
        StartCoroutine(EscreverTexto());
    }

    public void AvancarDialogo()
    {
        botaoEsquerdo.interactable = true;

        if(dialogoAtual < dialogos.Length)
        {
            if(messagemAtual< dialogos[dialogoAtual].messagens.Length-1)
            {
                messagemAtual++;
                StartCoroutine(EscreverTexto());
                //texto.text = dialogos[dialogoAtual].messagens[messagemAtual];
            }
            else if (dialogoAtual < dialogos.Length-1)
            {
                dialogoAtual++;
                messagemAtual = 0;
                AtualizarParametros(dialogos[dialogoAtual]);
            }

            if (messagemAtual == dialogos[dialogoAtual].messagens.Length - 1 && dialogoAtual == dialogos.Length - 1)
            {
                Debug.Log("Cheguei no limite");
                botaoDireito.interactable = false;
            }
        }

        //Debug.Log("Messagem Atual: " + messagemAtual + "; Dialogo Atual: " + dialogoAtual);
    }

    public void RetornarDialogo()
    {
        botaoDireito.interactable = true;

        if (messagemAtual>0)
        {
            messagemAtual--;
            //texto.text = dialogos[dialogoAtual].messagens[messagemAtual];

            StartCoroutine(EscreverTexto());
            Debug.Log("MessagemAtual: " + messagemAtual);
        }
        else if(dialogoAtual>0)
        {
            dialogoAtual--;
            messagemAtual = dialogos[dialogoAtual].messagens.Length - 1;
            AtualizarParametros(dialogos[dialogoAtual]);
        }

        if(messagemAtual <= 0 && dialogoAtual<=0)
        {
            Debug.Log("Cheguei ao limite papah");
            botaoEsquerdo.interactable = false;
        }
        //Debug.Log("Messagem Atual: " + messagemAtual + "; Dialogo Atual: " + dialogoAtual);
    }

    IEnumerator EscreverTexto()
    {
        textoUI.text = "";
        int messagemAtiva = messagemAtual;
        foreach (char letra in textosDialogo[messagemAtual].ToCharArray())
        {
            if(messagemAtiva == messagemAtual)
            {
                textoUI.text += letra;
                yield return new WaitForSeconds(velocidadeTexto);
            }
        }
    }

    //dialogos[dialogoAtual].messagens[messagemAtual]
}

