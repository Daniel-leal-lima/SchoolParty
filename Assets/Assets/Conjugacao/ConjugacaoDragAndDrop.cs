using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ConjugacaoDragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{

    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Canvas canvas;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Vector2 posicao;

    private void Start() {
        posicao = transform.position;
    }

    public void OnBeginDrag(PointerEventData eventData){
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.5f;
    }

    public void OnEndDrag(PointerEventData eventData){
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1f;
    }
    
    public void OnDrag(PointerEventData eventData){
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void ResetarPosicao(){
        transform.position = posicao;
    }
}
