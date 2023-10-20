using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModoDev : MonoBehaviour
{
    [SerializeField] ScrollRect teste;

    private void Start()
    {
        
    }
    public void EscolherFase(int i)
    {
        //Debug.Log("Ir para cena: " + i);
        SceneManager.LoadScene(i);
    }

    private void Update()
    {
        //AjustarScroll();
    }

    private void AjustarScroll()
    {
        teste.verticalScrollbar.value += Time.deltaTime;
    }
}
