using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SinonimoPlaca : MonoBehaviour
{
    [SerializeField] private List<string> listaPalavras = new List<string>();
    [SerializeField] private string palavraPlaca;
    
    [SerializeField] private Text palavraText;
    [SerializeField] private GameObject player;

    [SerializeField] private float velocidade;

    [SerializeField] private int pontos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() 
    {
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, velocidade, 0)*Time.deltaTime;
    }

    private void OnMouseDown() {
        if(player.GetComponent<SinonimoPlayer>().sinonimoAtual == null){
            player.GetComponent<SinonimoPlayer>().sinonimoAtual = this.gameObject;
            GetComponent<Animator>().SetBool("Correto", true);
        }
        else{
            if(listaPalavras.Contains(player.GetComponent<SinonimoPlayer>().sinonimoAtual.GetComponent<SinonimoPlaca>().GetPalavraPlaca())){
                player.GetComponent<SinonimoPlayer>().sinonimoAtual.GetComponent<Animator>().SetBool("Correto", true);
                player.GetComponent<SinonimoPlayer>().pontuacao+=pontos;
                GameObject.Find("Gerenciador").GetComponent<SinonimoController>().GetPlacas().Remove(player.GetComponent<SinonimoPlayer>().sinonimoAtual);
                Destroy(player.GetComponent<SinonimoPlayer>().sinonimoAtual);
                GameObject.Find("Gerenciador").GetComponent<SinonimoController>().GetPlacas().Remove(this.gameObject);
                Destroy(this.gameObject);
            }else{
                player.GetComponent<SinonimoPlayer>().sinonimoAtual.GetComponent<Animator>().GetComponent<Animator>().SetBool("Correto", false);
                player.GetComponent<SinonimoPlayer>().sinonimoAtual.GetComponent<Animator>().Play("PlacaErrado", 0, 0f);
                player.GetComponent<SinonimoPlayer>().sinonimoAtual = null;
                GetComponent<Animator>().SetBool("Correto", false);
                GetComponent<Animator>().Play("PlacaErrado", 0, 0f);    
            }
        }
    }

    public void SetPalavraPlaca(string palavra){
        palavraPlaca = palavra;
        palavraText.text = palavraPlaca;
    }

    public string GetPalavraPlaca(){
        return palavraPlaca;
    }

    public void SetListaPalavras(List<string> palavras){
        listaPalavras = palavras;
    }

    public List<string> GetListaPalavras(){
        return listaPalavras;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Placa"){
            GameObject.Find("Gerenciador").GetComponent<SinonimoController>().ReposicionarPlaca(this.gameObject);
        }
    }

}
