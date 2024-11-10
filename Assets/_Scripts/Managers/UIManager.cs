// File: UIManager.cs
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image weaponIconImage;          // UI Image to display weapon icon
    public TMP_Text weaponNameText;            // UI Text to display weapon name
    public TMP_Text weaponDescriptionText;     // UI Text to display weapon description

    public void UpdateWeaponUI(WeaponData weaponData)
    {
        weaponIconImage.sprite = weaponData.weaponIcon;   // Update weapon icon
        weaponNameText.text = weaponData.weaponName;             // Update weapon name
        weaponDescriptionText.text = weaponData.weaponDescription; // Update description
    }
}
