using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;



public class MenuLooby : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text listaJogadores;
    [SerializeField] private Button playJogo;
    
    
    [PunRPC]//atributo que o torna detector
    public void AtualizaLista(){
        listaJogadores.text = GestorDeRede.Instancia.ObterListaJogadores(); //passando lista de jogadores
        playJogo.interactable = GestorDeRede.Instancia.DonoDaSala();
        //desativando botão para não criador da sala
        //interative é uma propriedade booleana
    }

}
/*
Photon View detecta envios feitos para o master
*/