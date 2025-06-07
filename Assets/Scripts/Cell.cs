using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cell : MonoBehaviour, IPointerClickHandler {
    [Header("Cell Info")]
    [SerializeField] int row;
    [SerializeField] int column;
    
    [Header("X's and O's")]
    [SerializeField] Sprite xSprite;
    [SerializeField] Sprite oSprite;
    
    CellState _currentState = CellState.None;
    Image _image;

    void Start() {
        _image = GetComponent<Image>();

        UpdateVisual();
    }

    public void OnPointerClick(PointerEventData eventData) {
        CycleState();
    }

    void CycleState() {
        _currentState = (CellState)(((int)_currentState + 1) % 3);
        UpdateVisual();
    }

    void UpdateVisual() {
        switch (_currentState) {
            case CellState.None:
                _image.sprite = null;
                break;
            case CellState.X:
                _image.sprite = xSprite;
                break;
            case CellState.O:
                _image.sprite = oSprite;
                break;
            default:
                _image.sprite = null;
                break;
        }
    }
}
