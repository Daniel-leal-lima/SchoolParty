using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SinonimoPlayer : MonoBehaviour
{

    public GameObject sinonimoAtual;
    
    public int pontuacao;


    private void Update() {

        if(pontuacao >= GameObject.Find("Gerenciador").GetComponent<SinonimoController>().GetPontuacaoVitoria()){
            GameObject.Find("MenuController").GetComponent<MenuVitoria>().EncerrarJogo("VENCEDOR!!!", "Você é um ótimo aluno!");
        }
    }
}
