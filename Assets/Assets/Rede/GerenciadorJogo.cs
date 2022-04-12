using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GerenciadorJogo : MonoBehaviourPunCallbacks
{
//Sington script com instancia que instancia a si mesmo
    public static GerenciadorJogo Instancia {get; private set;}

    [SerializeField] private string localizacaoPrefab;
    [SerializeField] private Transform[] spawns;


    private int jogadoresEmJogo = 0;

    private List<PlayerController> jogadores;
    public List<PlayerController> Jogadores { get => jogadores; private set => jogadores = value; }

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
        photonView.RPC("AdicionaJogador", RpcTarget.AllBuffered);//só chama quando estiver carregada

        jogadores = new List<PlayerController>();
    }

    [PunRPC]
    private void AdicionaJogador(){
        jogadoresEmJogo++;
        if(jogadoresEmJogo == PhotonNetwork.PlayerList.Length){
            CriarJogador();
        }
    }

    private void CriarJogador(){
        var jogadorObjeto = PhotonNetwork.Instantiate(localizacaoPrefab, spawns[Random.Range(0, spawns.Length)].position, Quaternion.identity);
        var jogador = jogadorObjeto.GetComponent<PlayerController>();

        jogador.photonView.RPC("Inicializa", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
}
