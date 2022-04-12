using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;
using System.Data;
using Mono.Data.Sqlite;

public class ConjugacaoController : MonoBehaviour
{
    private IDbConnection connection;
    private IDbCommand command;
    private IDataReader reader;
    [SerializeField] private string nomeBD;

    [SerializeField] private GameObject sujeito;
    [SerializeField] private GameObject toBe;
    [SerializeField] private GameObject forma;

    [SerializeField] private float tempoMax;
    [SerializeField] private float tempoAtual;
    [SerializeField] private Image cronometro;
    [SerializeField] private Text cronometroText;

    [SerializeField] private Text historiaText;
    [SerializeField] private Text perguntaText;
    [SerializeField] private Text respostaText;
    [SerializeField] private string pergunta;
    [SerializeField] private string resposta;
    [SerializeField] private string[] historias;
    [SerializeField] private string[] perguntas;
    [SerializeField] private string[] respostas;
    private List<int> aleatorio = new List<int>();

    [SerializeField] private int rounds;

    [SerializeField] private GameObject[] palavras;

    [SerializeField] private Text textTeste1;
    [SerializeField] private Text textTeste2;


    void Start() {
            //  teste = "URI=file:" + Directory.GetCurrentDirectory()
        connection = new SqliteConnection("URI=file:" + Application.dataPath + "/DBSchoolParty.db");
        
        Debug.Log("URI=file:" + Application.dataPath + "/DBSchoolParty.db");
        textTeste1.text = "URI=file:" + Application.dataPath + "/DBSchoolParty.db";
        //Debug.Log("URI=file:" + Directory.GetCurrentDirectory());
        //textTeste2.text = "URI=file:" + Directory.GetCurrentDirectory();

        for(int i = 0; i < perguntas.Length; i++){
            aleatorio.Add(i);
        }

        IniciaRodada();
    }

    void Update() {
        if(!perguntaText.gameObject.activeSelf){
            perguntaText.gameObject.SetActive(true);
            respostaText.gameObject.SetActive(false);
        }
        //cronometro
        if(tempoAtual > 0f){
            tempoAtual -= Time.deltaTime;
            cronometro.GetComponent<Image>().fillAmount = tempoAtual / tempoMax;
        }else{
            if(rounds > 0){
                BdSelect();
                VerificaResposta();
                IniciaRodada();
            }else{
                GameObject.Find("MenuController").GetComponent<MenuVitoria>().EncerrarJogo("VENCEDOR!!!", "Você é um ótimo aluno!");
            }
        }
        cronometroText.text = tempoAtual.ToString("F0");
    }

    public void BdSelect(){
        connection.Open();
        command = connection.CreateCommand();

        string comandoSELECT = "SELECT Resposta FROM JogoDaConjucacao WHERE Sujeito like '"
            + sujeito.GetComponent<ConjugacaoPanel>().GetPalavraPainel() +
            "' AND ToBe like '" + toBe.GetComponent<ConjugacaoPanel>().GetPalavraPainel() +
            "' AND Forma like '" + forma.GetComponent<ConjugacaoPanel>().GetPalavraPainel() + "';";
        command.CommandText = comandoSELECT;

        reader = command.ExecuteReader();

        while (reader.Read()){
            respostaText.text = reader.GetString(0);
        }
        reader.Close();
        connection.Close();

    }

    private void VerificaResposta(){
        perguntaText.gameObject.SetActive(false);
        respostaText.gameObject.SetActive(true);

        // Debug.Log("Minha " + respostaText.text + " e resposta " + resposta);
        if(resposta != respostaText.text){
            GameObject.Find("MenuController").GetComponent<MenuVitoria>().EncerrarJogo("Você Perdeu!", "Deveria estudar mais...");
        }else{
            respostaText.gameObject.GetComponent<Animator>().Play("RespostaCerta", 0, 0f);
        }
    }

    private void IniciaRodada(){
        //restaurando padrões
        cronometro.GetComponent<Image>().fillAmount = 1;
        tempoAtual = tempoMax;

        rounds--;
        
        //selecionar uma pergunta e resposta aleatoria
        int index = UnityEngine.Random.Range(1, aleatorio.Count);
        pergunta = perguntas[aleatorio[index]];
        resposta = respostas[aleatorio[index]];
        historiaText.text = historias[aleatorio[index]];
        aleatorio.RemoveAt(index);

        sujeito.GetComponent<ConjugacaoPanel>().SetPalavraPainel(null);
        toBe.GetComponent<ConjugacaoPanel>().SetPalavraPainel(null);
        forma.GetComponent<ConjugacaoPanel>().SetPalavraPainel(null);

        foreach(GameObject palavra in palavras){
            palavra.GetComponent<ConjugacaoDragAndDrop>().ResetarPosicao();
        }

        perguntaText.text = pergunta;
    }
}
