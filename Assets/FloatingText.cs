using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 50f;
    public float lifeTime = 1f;

    private TextMeshProUGUI text;
    private Color startColor;
    private float timer;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        startColor = text.color;
        timer = lifeTime;
    }

    void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        timer -= Time.deltaTime;

        float alpha = timer / lifeTime;
        text.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

        if (timer <= 0)
            Destroy(gameObject);
    }
}