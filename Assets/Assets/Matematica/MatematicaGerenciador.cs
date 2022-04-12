using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatematicaGerenciador : MonoBehaviour
{

    [SerializeField] private float tempoMax;
    [SerializeField] private float tempoAtual;
    [SerializeField] private Image cronometro;
    [SerializeField] private Text cronometroText;

    [SerializeField] private Text operacaoAtualText;

    [SerializeField] private List<string> operacoes;
    [SerializeField] private List<string> respostas;
    [SerializeField] private string operacaoAtual;
    [SerializeField] private string respostaAtual;

    [SerializeField] private GameObject[] pisos;
    [SerializeField] private Canvas canvasRespostas;

    [SerializeField] private List<int> posicoesAleatorias;
    [SerializeField] private List<Text> respostasText;

    [SerializeField] private GameObject player;

    [SerializeField] private int rounds;



    // Start is called before the first frame update
    void Start()
    {
        posicoesAleatorias = new List<int>();

        player = GameObject.Find("Player");

        foreach(GameObject piso in pisos){
            piso.SetActive(true);
        }
        canvasRespostas.gameObject.SetActive(true);

        IniciarNovoRound();
    }

    // Update is called once per frame
    void Update()
    {
        //cronometro
        if(tempoAtual > 0f){
            tempoAtual -= Time.deltaTime;
            cronometro.GetComponent<Image>().fillAmount = tempoAtual / tempoMax;
        }
        else{
            if(rounds > 0){
                ValidarResposta();
                IniciarNovoRound();
            }else{
                GameObject.Find("MenuController").GetComponent<MenuVitoria>().EncerrarJogo("VENCEDOR!!!", "Você é um ótimo aluno!");
            }
        }
        cronometroText.text = tempoAtual.ToString("F0");
    }

    private void ValidarResposta(){

        if(player.GetComponent<MatematicaPlayer>().GetRespostas().Count > 1){
            foreach(GameObject resposta in player.GetComponent<MatematicaPlayer>().GetRespostas()){
                resposta.GetComponent<MatematicaPiso>().PlayErrado();
            }
            GameObject.Find("MenuController").GetComponent<MenuVitoria>().EncerrarJogo("Você Perdeu!", "Deveria estudar mais...");
        }
        else{
            if(player.GetComponent<MatematicaPlayer>().GetRespostas()[0].GetComponent<MatematicaPiso>().GetRepostaPiso() == respostaAtual){
                player.GetComponent<MatematicaPlayer>().GetRespostas()[0].GetComponent<MatematicaPiso>().PlayCerto();
            }else{
            foreach(GameObject resposta in player.GetComponent<MatematicaPlayer>().GetRespostas()){
                resposta.GetComponent<MatematicaPiso>().PlayErrado();
            }
            GameObject.Find("MenuController").GetComponent<MenuVitoria>().EncerrarJogo("Você Perdeu!", "Deveria estudar mais...");
                
            }
        }

    }

    private void IniciarNovoRound(){

        //restaurando padrões
        cronometro.GetComponent<Image>().fillAmount = 1;
        tempoAtual = tempoMax;
        
        rounds--;

        //apagando lista do jogador
        player.GetComponent<BoxCollider2D>().enabled = false;
        player.GetComponent<MatematicaPlayer>().GetRespostas().Clear();
        player.GetComponent<BoxCollider2D>().enabled = true;

        //pega operação e resposta aleatoriamente
        int x = Random.Range(0, operacoes.Count);

        operacaoAtual = operacoes[x];
        operacoes.Remove(operacaoAtual);
        respostaAtual = respostas[x];
        respostas.Remove(respostaAtual);

        operacaoAtualText.text = operacaoAtual;

        //randomiza posições/pisos das respostas
        for(int i = 0; i<16; i++){
            posicoesAleatorias.Add(i);
        }

        for(int i = 0; i<16; i++){
            int IndexAleatorio = Random.Range(0, posicoesAleatorias.Count-1);
            if(i < 3){
                pisos[posicoesAleatorias[IndexAleatorio]].GetComponent<MatematicaPiso>().SetRespostaPiso(respostaAtual);
            }else{
                do{
                    pisos[posicoesAleatorias[IndexAleatorio]].GetComponent<MatematicaPiso>().SetRespostaPiso(Random.Range(int.Parse(respostaAtual)-3, int.Parse(respostaAtual)+3).ToString());
                }while(pisos[posicoesAleatorias[IndexAleatorio]].GetComponent<MatematicaPiso>().GetRepostaPiso() == respostaAtual);
            }
            posicoesAleatorias.RemoveAt(IndexAleatorio);
        }

        for(int i = 0; i<16; i++){
            respostasText[i].text = pisos[i].GetComponent<MatematicaPiso>().GetRepostaPiso();
        }

    }
}
