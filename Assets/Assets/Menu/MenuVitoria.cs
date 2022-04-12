using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MenuVitoria : MonoBehaviour
{
    [SerializeField] private GameObject[] elementosJogo;

    [SerializeField] private Canvas menuIniciarJogo;
    [SerializeField] private Text cronometroText;
    [SerializeField] private float tempo;
    
    [SerializeField] private Canvas menuEncerrarJogo;
    [SerializeField] private Text tituloText;
    [SerializeField] private Text comentarioText;

    [SerializeField] private GameObject panelTutorial;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioBotao;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //cronometro para começar o jogo
        if(menuIniciarJogo.gameObject.activeSelf == true){
            ComeçarJogo();
        }

        if(menuEncerrarJogo.gameObject.activeSelf == true){
            AtivandoTelaFinal();
        }

    }

    public void ComeçarJogo(){
        if(tempo > 0f){
            tempo -= Time.deltaTime;
            if(tempo >= 1f){
                cronometroText.text = (tempo-1).ToString("F0");
            }
            else{
                cronometroText.text = "Preparar... Já!";
            }
        }
        else{
            
            foreach(GameObject elemento in elementosJogo){
                elemento.SetActive(true);
            }

            menuIniciarJogo.gameObject.SetActive(false);

        }
    }

    public void EncerrarJogo(string titulo, string comentario){
        foreach(GameObject elemento in elementosJogo){
            elemento.SetActive(false);
        }

        menuEncerrarJogo.gameObject.SetActive(true);
        tituloText.text = titulo;
        comentarioText.text = comentario;
    }

    public void AtivandoTelaFinal(){
        if(tempo < 3f){
            tempo += Time.deltaTime;
            menuEncerrarJogo.GetComponent<CanvasGroup>().alpha = tempo / 3f;
        }
        else{
            menuEncerrarJogo.GetComponent<CanvasGroup>().interactable = true;
        }
    }

    public void CarregarCena(string nomeCena){
        audioSource.PlayOneShot(audioBotao, 1f);
        SceneManager.LoadScene(nomeCena);
    }

    public void IniciarContagem(){
        audioSource.PlayOneShot(audioBotao, 1f);
        panelTutorial.SetActive(false);
        menuIniciarJogo.gameObject.SetActive(true);

    }
}
