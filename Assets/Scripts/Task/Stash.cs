using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class Stash : MonoBehaviour
{
    [SerializeField] private Item ItemPrefab;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private TextMeshProUGUI MoneyText;
    public Item[] stash = new Item[28];
    [SerializeField]private int maxItemCount = 10;
    
    private int maxItemPrice = 150;
    private int maxMoney = 1000;
    public int Money;
    private void Start()
    {
        int tempCount = Random.Range(0, maxItemCount);
        Money = Random.Range(400, maxMoney);
        MoneyText.text = Money.ToString();
        for (int i = 0; i < 28; i++)
        {
            int tempPrice = Random.Range(0, maxItemPrice);
            Item item = Instantiate(ItemPrefab, transform);
            item.InventoryType = gameObject.CompareTag("Player") ? InventoryType.Player : InventoryType.Trader;
            item.name = $"{gameObject.name} {i}";
            stash[i] = item;
            if (i <= tempCount)
            {
                item.ItemImage.sprite = sprites[Random.Range(0, sprites.Length - 1)];
                item.IsEmpty = false;
                item.PriceText.text = tempPrice.ToString();
            }
        }
    }

    public void UpdateGUI()
    {
        MoneyText.text = Money.ToString();
    }
}