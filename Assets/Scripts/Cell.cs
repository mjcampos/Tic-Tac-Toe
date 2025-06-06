using UnityEngine;
using UnityEngine.EventSystems;

public class Cell : MonoBehaviour, IPointerClickHandler {
    [SerializeField] int row;
    [SerializeField] int column;
    
    public void OnPointerClick(PointerEventData eventData) {
        Debug.Log(row + "," + column);
    }
}
