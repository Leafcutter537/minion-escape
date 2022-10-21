using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Diagnostics.Tracing;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] private List<TextMeshProUGUI> potionCountTexts;
    [SerializeField] private Inventory inventory;
    [SerializeField] private Color selectedColor;
    [SerializeField] private Color defaultColor;
    [SerializeField] private List<Image> potionBackgroundImages;

    public void UpdatePotionUI()
    {
        int i = 0;
        foreach (PotionPickup.PotionType type in Enum.GetValues(typeof(PotionPickup.PotionType)))
        {
            potionCountTexts[i].text = inventory.potions[type].ToString();
            i++;
        }
        for (int j = 0; j < potionBackgroundImages.Count; j++)
        {
            potionBackgroundImages[j].color = j == (int)inventory.selectedType ? selectedColor : defaultColor;
        }
    }

}
