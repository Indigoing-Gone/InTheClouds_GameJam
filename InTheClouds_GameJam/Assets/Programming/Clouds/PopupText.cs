using TMPro;
using UnityEngine;

public class PopupText : MonoBehaviour
{
    private TextMeshProUGUI textComponent;
    [SerializeField] private float destroyTimer = 1.5f;
    private float timer;

    void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        timer = destroyTimer;
    }

    public void SetText(string _text)
    {
        textComponent.text = _text;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0) Destroy(gameObject);
    }
}
