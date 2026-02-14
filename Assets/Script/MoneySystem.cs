using UnityEngine;
using TMPro; // ÊÃßÏ ãä ÇÓÊÎÏÇã TextMeshPro áÌãÇáíÉ ÇáÎØæØ

public class MoneySystem : MonoBehaviour
{
    public static MoneySystem Instance; // áÓåæáÉ ÇáæÕæá ãä Ãí ÓßÑíÈÊ ÂÎÑ

    public int currentMoney = 1000;
    public TextMeshProUGUI moneyText;

    void Awake() { Instance = this; }

    void Start() { UpdateUI(); }

    public bool TryPurchase(int amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            UpdateUI();
            return true; // ÊãÊ ÚãáíÉ ÇáÔÑÇÁ ÈäÌÇÍ
        }
        return false; // áÇ íãáß ãÇáÇğ ßÇİíÇğ
    }

    void UpdateUI()
    {
        moneyText.text = "$" + currentMoney.ToString();
    }
}