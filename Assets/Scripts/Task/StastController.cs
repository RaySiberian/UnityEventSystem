using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class StastController : MonoBehaviour
{
    public GameObject itemPrefab;
    public static event Action<bool> CheckPrice; 
    [SerializeField] private Stash playerStash;
    [SerializeField] private Stash traderStash;
    private Sequence sequence;
    private void Start()
    {
        MouseData.NeedSwapItem += SwapItem;
        MouseData.itemPrefab = itemPrefab;
    }

    private void SwapItem()
    {
        if (MouseData.ToItem.InventoryType == InventoryType.Player)
        {
            if (playerStash.Money - int.Parse(MouseData.FromItem.PriceText.text) >= 0)
            {
                MouseData.ToItem.ItemImage.sprite = MouseData.FromItem.ItemImage.sprite;
                MouseData.ToItem.PriceText.text = MouseData.FromItem.PriceText.text;
                MouseData.ToItem.IsEmpty = false;
                playerStash.Money -= int.Parse(MouseData.FromItem.PriceText.text);
                playerStash.UpdateGUI();
                traderStash.Money += int.Parse(MouseData.FromItem.PriceText.text);
                traderStash.UpdateGUI();
                sequence = DOTween.Sequence();
                sequence.Append(MouseData.ToItem.transform.DOScale(Vector3.one * 1.2f, 0.3f));
                sequence.Append(MouseData.ToItem.transform.DOScale(Vector3.one , 0.3f));
                CheckPrice?.Invoke(true);
            }
        }
        if (MouseData.ToItem.InventoryType == InventoryType.Trader)
        {
            if (traderStash.Money - int.Parse(MouseData.FromItem.PriceText.text) >= 0)
            {
                MouseData.ToItem.ItemImage.sprite = MouseData.FromItem.ItemImage.sprite;
                MouseData.ToItem.PriceText.text = MouseData.FromItem.PriceText.text;
                MouseData.ToItem.IsEmpty = false;
                traderStash.Money -= int.Parse(MouseData.FromItem.PriceText.text);
                traderStash.UpdateGUI();
                playerStash.Money += int.Parse(MouseData.FromItem.PriceText.text);
                playerStash.UpdateGUI();
                sequence.Append(MouseData.ToItem.transform.DOScale(Vector3.one * 1.2f, 0.3f));
                sequence.Append(MouseData.ToItem.transform.DOScale(Vector3.one, 0.3f));
                CheckPrice?.Invoke(true);
            }
        }
        CheckPrice?.Invoke(false);
    }
}

public static class MouseData
{
    public static Vector3 LastPosition;
    public static Item FromItem;
    public static Item ToItem;
    public static Action NeedSwapItem;
    public static GameObject itemPrefab;
    public static void ClearData()
    {
        FromItem = null;
        ToItem = null;
        LastPosition = default;
    }
}

public enum InventoryType
{
    Player,
    Trader
}
