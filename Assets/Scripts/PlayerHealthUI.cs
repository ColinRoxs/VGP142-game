using UnityEngine;
using TMPro; // Required for TextMeshPro

public class PlayerHealthUI : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    void Update()
    {
        healthText.text = "Health: " + GameManager.Instance.playerHealth.ToString();
    }
}