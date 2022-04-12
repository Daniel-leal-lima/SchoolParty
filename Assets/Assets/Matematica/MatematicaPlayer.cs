using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;


public class MatematicaPlayer : MonoBehaviourPunCallbacks
{
    [SerializeField] private int velocidade;
    Vector2 velInput;
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private List<GameObject> respostas;

    [SerializeField] private string resposta;



        // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
    }
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Piso"){
            respostas.Add(other.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Piso"){
            respostas.Remove(other.gameObject);
        }
    }
    public List<GameObject> GetRespostas(){
        return respostas;
    }

}
