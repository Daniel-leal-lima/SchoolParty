using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class Menu : MonoBehaviourPunCallbacks
{
    [SerializeField] private MenuEntrada menuEntrada;
    [SerializeField] private MenuLooby menuLooby;

    private void Start() {
        menuEntrada.gameObject.SetActive(false);
        menuLooby.gameObject.SetActive(false);
    }

    public override void OnConnectedToMaster(){//sobrecarga
        menuEntrada.gameObject.SetActive(true);

    }

    public override void OnJoinedRoom(){
        MudaMenu(menuLooby.gameObject);
        menuLooby.photonView.RPC("AtualizaLista", RpcTarget.All);//chamando por RPC, para todos os jogadores na sala
    }

    public override void OnPlayerLeftRoom(Player otherPlayer){
        menuLooby.AtualizaLista();
    }


    public void MudaMenu(GameObject menu){
        menuEntrada.gameObject.SetActive(false);
        menuLooby.gameObject.SetActive(false);

        menu.SetActive(true);
    }

    public void SairLooby(){
        GestorDeRede.Instancia.SairDoLooby();
        MudaMenu(menuEntrada.gameObject);
    }

    public void ComecarJogo(string nomeCena){
        GestorDeRede.Instancia.photonView.RPC("PlayJogo", RpcTarget.All, nomeCena);
        //para levar todos os jogadores juntos
    }
}
