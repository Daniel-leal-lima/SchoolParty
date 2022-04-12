using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;


public class PlayerController : MonoBehaviourPunCallbacks
{
    [SerializeField] private int velocidade;
    Vector2 velInput;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private bool click;
    [SerializeField] private string proximoJogo;
    [SerializeField] private Text jogoText;


    [SerializeField] private Player photonPlayer;
    [SerializeField] private int id;

    [PunRPC]
    public void Inicializa(Player player){
        photonPlayer = player;
        id = player.ActorNumber;
        GerenciadorJogo.Instancia.Jogadores.Add(this);

        if(!photonView.IsMine){
            rb.isKinematic = false; // desligando o jogador inimigo
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        click = false;
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * velocidade * Time.deltaTime;
        
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0){
            GetComponent<Animator>().SetBool("Andar", true);
            
            if(Input.GetAxisRaw("Horizontal") != 0){
                transform.localScale = new Vector3(Input.GetAxisRaw("Horizontal"), transform.localScale.y, transform.localScale.z);
            }
        }
        else{
            GetComponent<Animator>().SetBool("Andar", false);
        }

        if(click = true && Input.GetKeyDown("space")){
            SceneManager.LoadScene(proximoJogo);
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Inicializador"){
            proximoJogo = other.gameObject.GetComponent<CarregadorJogo>().nomeJogo;
            other.gameObject.GetComponent<SpriteRenderer>().color = other.gameObject.GetComponent<CarregadorJogo>().corColisao;
            click = true;
            jogoText.gameObject.SetActive(true);
            jogoText.text = other.gameObject.GetComponent<CarregadorJogo>().frase;
        }
    }

    private void OnCollisionExit2D(Collision2D other) {
        if(other.gameObject.tag == "Inicializador"){
            other.gameObject.GetComponent<SpriteRenderer>().color = other.gameObject.GetComponent<CarregadorJogo>().corOriginal;
            jogoText.gameObject.SetActive(false);
            proximoJogo = null;
            click = false;
        }
    }
}
