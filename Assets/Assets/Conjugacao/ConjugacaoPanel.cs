using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConjugacaoPanel : MonoBehaviour, IDropHandler
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private string palavraPainel;

    public void OnDrop(PointerEventData eventData){
        eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;
        palavraPainel = eventData.pointerDrag.GetComponent<Text>().text;
    }

    public string GetPalavraPainel(){
        return palavraPainel;
    }

    public void SetPalavraPainel(string palavra){
        palavraPainel = palavra;
    }
}
