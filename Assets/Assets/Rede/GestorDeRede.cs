using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GestorDeRede : MonoBehaviourPunCallbacks //MonoBehaviour especial para verificar conexão com server
{
//Sington script com instancia que instancia a si mesmo
    public static GestorDeRede Instancia {get; private set;}


    private void Awake() {
        if(Instancia != null && Instancia != this){
            gameObject.SetActive(false);
            return;
        }
        Instancia = this; //se voltar ao menu, recria essa instancia


        //nunca pode perder o gestor de rede
        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        PhotonNetwork.ConnectUsingSettings();//Conectando ao servidor usando as configurações já definidas
    }

    public override void OnConnectedToMaster(){//sobrecarga
        Debug.Log("Conectou ao Servidor!"); //conecta ao servidor master pre configurado
    }

    public void CriaSala(string nomeSala){
        PhotonNetwork.CreateRoom(nomeSala); //para criar salas
    }
    
    public void EntraSala(string nomeSala){
        PhotonNetwork.JoinRoom(nomeSala); //para entrar numa sala já existente
    }

    public void MudaNome(string nomeJogador){
        PhotonNetwork.NickName = nomeJogador; //adicionando o nickname do jogador
    }

    public string ObterListaJogadores(){
        var lista = "";
        foreach (var jogador in PhotonNetwork.PlayerList){//da lista de jogadores
            lista+= jogador.NickName + "\n";
        }
        return lista;
    }

    public bool DonoDaSala(){
        return PhotonNetwork.IsMasterClient;
    }

    public void SairDoLooby(){
        PhotonNetwork.LeaveRoom(); //saindo da sala
    }

    [PunRPC]
    public void PlayJogo(string nomeCena){
        PhotonNetwork.LoadLevel(nomeCena);//carregando cena
    }
}
