using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Item : MonoBehaviour, IPointerDownHandler,IBeginDragHandler, IDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler, IEndDragHandler
{
    public TextMeshProUGUI PriceText;
    public Image ItemImage;
    public bool IsEmpty;
    public InventoryType InventoryType;
    [SerializeField] private Item selfPrefab;
    private bool canSwap;
    private void OnEnable()
    {
        IsEmpty = true;
        StastController.CheckPrice += AnswerCallBack;
    }

    private void SetEmpty()
    {
        IsEmpty = true;
        ItemImage.sprite = MouseData.itemPrefab.GetComponent<Item>().ItemImage.sprite;
        PriceText.text = MouseData.itemPrefab.GetComponent<Item>().PriceText.text;
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsEmpty)
            return;
        
        MouseData.LastPosition = transform.position;
        MouseData.FromItem = this;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (IsEmpty)
            return;
        
        GetComponent<CanvasGroup>().blocksRaycasts = false;
        GetComponent<Image>().raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsEmpty)
            return;
        
        GetComponent<RectTransform>().position = Input.mousePosition;
    }
    
    //Этот метод срабатывает на ячейке, куда перетащили
    public void OnDrop(PointerEventData eventData)
    {
        MouseData.ToItem = this;

        if (MouseData.FromItem == null)
        {
            return;
        }
        
        if (MouseData.FromItem.InventoryType == MouseData.ToItem.InventoryType || MouseData.ToItem.IsEmpty == false)
        {
            ResetFromItem();
            return;
        }
        
        MouseData.NeedSwapItem?.Invoke();
    }

    private void AnswerCallBack(bool swaping)
    {
        if (swaping)
        {
            SwapComplite();
            MouseData.FromItem.SetEmpty();
        }
        else
        {
            ResetFromItem();
        }
    }
    
    private void ResetFromItem()
    {
        MouseData.FromItem.gameObject.transform.position = MouseData.LastPosition;
        MouseData.FromItem.GetComponent<CanvasGroup>().blocksRaycasts = true;
        MouseData.FromItem.GetComponent<Image>().raycastTarget = true;
    }

    private void SwapComplite()
    {
        ResetFromItem();
        if (MouseData.FromItem.InventoryType == InventoryType.Player)
        {
            MouseData.FromItem.InventoryType = InventoryType.Trader;
            MouseData.ToItem.InventoryType = InventoryType.Player;
        }
        else
        {
            MouseData.FromItem.InventoryType = InventoryType.Player;
            MouseData.ToItem.InventoryType = InventoryType.Trader;
        }
    }
    
    //Анимации
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.3f; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.localScale = Vector3.one;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (MouseData.ToItem == null)
        {
            ResetFromItem();
        }
        MouseData.ClearData();
    }
}
