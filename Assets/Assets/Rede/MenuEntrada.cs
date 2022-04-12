using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuEntrada : MonoBehaviour
{
    [SerializeField] private Text nomeJogador; //Serialize mantem private, mas permite editar pelo Unity
    [SerializeField] private Text nomeSala;


    public void CriarSala(){
        GestorDeRede.Instancia.MudaNome(nomeJogador.text);
        GestorDeRede.Instancia.CriaSala(nomeSala.text);
    }

    public void EntrarSala(){
        GestorDeRede.Instancia.MudaNome(nomeJogador.text);
        GestorDeRede.Instancia.EntraSala(nomeSala.text);
    }
}
