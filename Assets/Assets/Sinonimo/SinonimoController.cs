using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SinonimoController : MonoBehaviour
{
    [SerializeField] private Image cronometro;
    [SerializeField] private Text cronometroText;
    [SerializeField] private float tempoMax;
    [SerializeField] private float tempoAtual;

    [SerializeField] private List<List<string>> sinonimos = new List<List<string>>();
    [SerializeField] private List<List<string>> sinonimosEscolhidos = new List<List<string>>();
    [SerializeField] private List<int> aleatorio;

    [SerializeField] private List<GameObject> placas;
    [SerializeField] private List<int> aleatorioPosX;
    [SerializeField] private List<int> aleatorioPosY;

    [SerializeField] private int pontuacaoVitoria;

    // Start is called before the first frame update
    void Start()
    {
        tempoAtual = tempoMax;

        //adicionando palavras
        AdicionarPalavras();

        //ativando placas
        for(int i = 0; i<placas.Count;i++){
            placas[i].SetActive(true);
        }

        foreach(GameObject placa in placas){
            ReposicionarPlaca(placa);
        }

        //randomizando e escolhendo sinonimos
        SelecionarPalavras();

        DefinindoPlacas();
  
    }

    // Update is called once per frame
    void Update()
    {
        //cronometro
        if(tempoAtual > 0f){
            tempoAtual -= Time.deltaTime;
            cronometro.GetComponent<Image>().fillAmount = tempoAtual / tempoMax;
        }
        cronometroText.text = tempoAtual.ToString("F0");

        //reposicionando placas
        foreach(GameObject placa in placas){
            if(placa.transform.position.y <= -12f){
                ReposicionarPlaca(placa);
            }
        }



        //definindo derrota
        if(tempoAtual<=0 && GameObject.Find("Player").GetComponent<SinonimoPlayer>().pontuacao < pontuacaoVitoria){
            GameObject.Find("MenuController").GetComponent<MenuVitoria>().EncerrarJogo("Você Perdeu!", "Deveria estudar mais...");
        }

    }

    public void AdicionarPalavras(){
        sinonimos.Add(new List<string>{"Grande","Enorme", "Imenso"});
        sinonimos.Add(new List<string>{"Pequeno","Baixo", "Minúsculo"});
        sinonimos.Add(new List<string>{"Muito","Bastante", "Demais", "Abundante"});
        sinonimos.Add(new List<string>{"Bonito","Lindo", "Belo"});
        sinonimos.Add(new List<string>{"Certo","Correto"});

        sinonimos.Add(new List<string>{"Mal","Ruim", "Malvado"});
        sinonimos.Add(new List<string>{"Longe","Afastado", "Distante"});
        sinonimos.Add(new List<string>{"Perto","Próximo"});
        sinonimos.Add(new List<string>{"Corajoso","Valente", "Bravo"});
        sinonimos.Add(new List<string>{"Começo","Inicio"});
    }

    public void SelecionarPalavras(){
        aleatorio = new List<int>();
        for(int i = 0; i < sinonimos.Count; i++){
            aleatorio.Add(i);
        }

        for(int j = 0; j < placas.Count; j++){

            int indexEscolhido = Random.Range(0, aleatorio.Count-1);

            sinonimosEscolhidos.Add(sinonimos[aleatorio[indexEscolhido]]);
            aleatorio.RemoveAt(indexEscolhido);
            
        }

    }

    //melhorar aleatoria, saindo mesmas palavras
    public void DefinindoPlacas(){

        //definindo lista das placas
        int x = 0;
        foreach(List<string> listaSinonimos in sinonimosEscolhidos){
            try{
                placas[x].GetComponent<SinonimoPlaca>().SetListaPalavras(listaSinonimos);
                placas[x+1].GetComponent<SinonimoPlaca>().SetListaPalavras(listaSinonimos);                


            aleatorio = new List<int>();
            for(int j = 0; j < listaSinonimos.Count; j++){
                aleatorio.Add(j);
            }
            
            int indexEscolhido = Random.Range(0, aleatorio.Count-1);
            placas[x].GetComponent<SinonimoPlaca>().SetPalavraPlaca(listaSinonimos[aleatorio[indexEscolhido]]);
            aleatorio.RemoveAt(indexEscolhido);

            indexEscolhido = Random.Range(0, aleatorio.Count-1);
            placas[x+1].GetComponent<SinonimoPlaca>().SetPalavraPlaca(listaSinonimos[aleatorio[indexEscolhido]]);
            aleatorio.RemoveAt(indexEscolhido);

            x+=2;
            }
            catch (System.ArgumentOutOfRangeException e){
                Debug.Log("Erro: " + e);
            }
        }

    }

    public void ReposicionarPlaca(GameObject placaAtual){
        if(aleatorioPosX.Count <= 0){
            for(int i = -15; i<=15; i+=10){
                aleatorioPosX.Add(i);    
            }
        }

        if(aleatorioPosY.Count <= 0){
            for(int i = 12; i < 52; i+=4){
                aleatorioPosY.Add(i);    
            }
        }

        int indexEscolhidoX = Random.Range(0, aleatorioPosX.Count-1);
        int indexEscolhidoY = Random.Range(0, aleatorioPosY.Count-1);

        placaAtual.transform.position = new Vector3(aleatorioPosX[indexEscolhidoX], aleatorioPosY[indexEscolhidoY], 0f);
        placaAtual.GetComponent<Rigidbody2D>().velocity = Vector3.zero;

        aleatorioPosX.RemoveAt(indexEscolhidoX);
        aleatorioPosY.RemoveAt(indexEscolhidoY);
    }

    public int GetPontuacaoVitoria(){
        return pontuacaoVitoria;
    }

    public List<GameObject> GetPlacas(){
        return placas;
    }


}
