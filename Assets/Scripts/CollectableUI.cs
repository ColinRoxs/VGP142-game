using UnityEngine;
using TMPro; // Required for TextMeshPro

public class CollectableUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    void Update()
    {
        healthText.text = "Collectables found: " + GameManager.Instance.collectableCount.ToString();
    }
}