using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class Menus : MonoBehaviour
{
    [SerializeField] private GameObject panelEquipe;
    [SerializeField] private GameObject panelConfiguracoes;

    [SerializeField] private AudioMixer audioMixerMusica;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioBotao;


    public void CarregarCena(string nomeCena){
        SceneManager.LoadScene(nomeCena);
    }

    public void MenuEquipe(){
        audioSource.PlayOneShot(audioBotao, 1f);
        panelEquipe.SetActive(true);
    }

    public void MenuConfiguracoes(){
        audioSource.PlayOneShot(audioBotao, 1f);
        panelConfiguracoes.SetActive(true);
    }

    public void BotaoVoltar(){
        audioSource.PlayOneShot(audioBotao, 1f);
        if(panelEquipe.activeSelf == true){
            panelEquipe.SetActive(false);
        }
        if(panelConfiguracoes.activeSelf == true){
            panelConfiguracoes.SetActive(false);
        }
    }

    public void Sair(){
        Application.Quit();
    }

    public void MudarVolume(float volume){
        audioMixerMusica.SetFloat("VolumeMusica", volume);
        Debug.Log(volume);
    }
}
