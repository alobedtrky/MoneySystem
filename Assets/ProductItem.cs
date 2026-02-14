using UnityEngine;
using TMPro;

public class ProductItem : MonoBehaviour
{
    [Header("Settings")]
    public int price = 100;
    public float interactionDistance = 3f;
    public bool destroyAfterPurchase = true;

    [Header("UI Elements")]
    public GameObject interactionUI;    // ÇáßÇäİÓ ÇáĞí íÍÊæí Úáì ÍÑİ E
    public TextMeshProUGUI feedbackText; // äÕ ÇáãáÇÍÙÇÊ ãËá -100$
    public GameObject visualModel;      // ãæÏíá ÇáãäÊÌ (Mesh) áíÎÊİí æíÈŞì ÇáÓßÑíÈÊ

    [Header("Audio")]
    public AudioSource audioSource;     // ßÇÆä ÇáÕæÊ
    public AudioClip successSound;      // ÕæÊ ÔÑÇÁ äÇÌÍ
    public AudioClip failSound;         // ÕæÊ ÑÕíÏ ÛíÑ ßÇİí

    private Transform playerTransform;
    private bool isPurchased = false;

    void Start()
    {
        // ÇáÈÍË Úä ÇááÇÚÈ ÊáŞÇÆíÇğ
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerTransform = player.transform;

        // ÅÚÏÇÏÇÊ ÇáÈÏÇíÉ ááÜ UI
        if (interactionUI != null) interactionUI.SetActive(false);
        if (feedbackText != null) feedbackText.gameObject.SetActive(false);
    }

    void Update()
    {
        // ÅĞÇ Êã ÇáÔÑÇÁ Ãæ ÇááÇÚÈ ÛíÑ ãæÌæÏ áÇ ÊİÚá ÔíÆÇğ
        if (playerTransform == null || isPurchased) return;

        float distance = Vector3.Distance(transform.position, playerTransform.position);

        if (distance <= interactionDistance)
        {
            ShowUI();
            if (Input.GetKeyDown(KeyCode.E))
            {
                BuyProduct();
            }
        }
        else
        {
            HideUI();
        }
    }

    void BuyProduct()
    {
        // İÍÕ äÙÇã ÇáÃãæÇá (ÊÃßÏ Ãä ÇÓã ÇáßáÇÓ ÚäÏß MoneySystem Ãæ MoneyManager)
        if (MoneySystem.Instance != null && MoneySystem.Instance.TryPurchase(price))
        {
            isPurchased = true;
            PlaySound(successSound);
            ShowFeedback("-" + price + "$", Color.red);

            if (destroyAfterPurchase)
            {
                ExecutePurchaseEffect();
            }
        }
        else
        {
            PlaySound(failSound);
            ShowFeedback("Need More Cash!", Color.yellow);
        }
    }

    void ExecutePurchaseEffect()
    {
        // ÅÎİÇÁ ÇáãæÏíá æÍÑİ E İæÑÇğ áíÙä ÇááÇÚÈ Ãäå ÇÎÊİì
        if (visualModel != null) visualModel.SetActive(false);
        if (interactionUI != null) interactionUI.SetActive(false);

        // ÊÏãíÑ ÇáßÇÆä äåÇÆíÇğ ÈÚÏ ËÇäíÊíä áÖãÇä ÇäÊåÇÁ ÇáÕæÊ æäÕ ÇáãáÇÍÙÇÊ
        Destroy(gameObject, 2f);
    }

    void ShowUI()
    {
        if (interactionUI == null || isPurchased) return;

        interactionUI.SetActive(true);

        // ÌÚá ÍÑİ E íæÇÌå ÇáßÇãíÑÇ ÏÇÆãÇğ (Billboard)
        interactionUI.transform.LookAt(interactionUI.transform.position + Camera.main.transform.rotation * Vector3.forward,
                                     Camera.main.transform.rotation * Vector3.up);
    }

    void HideUI()
    {
        if (interactionUI != null) interactionUI.SetActive(false);
    }

    void ShowFeedback(string msg, Color col)
    {
        if (feedbackText == null) return;

        feedbackText.text = msg;
        feedbackText.color = col;
        feedbackText.gameObject.SetActive(true);

        CancelInvoke("HideFeedbackText");
        Invoke("HideFeedbackText", 1.5f);
    }

    void HideFeedbackText()
    {
        if (feedbackText != null) feedbackText.gameObject.SetActive(false);
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    void OnDisable()
    {
        CancelInvoke();
    }

    // ÑÓã ÇáÏÇÆÑÉ İí ãÍÑÑ íæäÊí ááÊæÖíÍ
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }
}