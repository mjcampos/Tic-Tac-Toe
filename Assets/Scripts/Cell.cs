using Singletons;
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
    
    Image _image;

    void Start() {
        _image = GetComponent<Image>();

        UpdateVisual();
    }

    public void OnPointerClick(PointerEventData eventData) {
        BattleManager.Instance.OnPlayerClickCell(row, column);
        UpdateVisual();
    }

    void UpdateVisual() {
        CellState state = BattleManager.Instance.BoardPosition[row, column];
        
        switch (state) {
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

    public void ClearCell() {
        UpdateVisual();
    }

    public void LockCell() {
        _image.raycastTarget = false;
    }

    public void Highlight(Color color) {
        _image.color =  color;
    }
}
