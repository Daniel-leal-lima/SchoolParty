using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatematicaPiso : MonoBehaviour
{
    [SerializeField] private string respostaPiso;


    public void PlayCerto(){
        GetComponent<Animator>().Play("PisoCerto", 0, 0f);
    }

    public void PlayErrado(){
        GetComponent<Animator>().Play("PisoErrado", 0, 0f);
    }

    public string GetRepostaPiso(){
        return respostaPiso;
    }

    public void SetRespostaPiso(string resposta){
        respostaPiso = resposta;
    }


}
