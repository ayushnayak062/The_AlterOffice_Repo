using UnityEngine;
using UnityEngine.VFX;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponData : ScriptableObject
{
    public WeaponType weaponType;                  // Type of weapon (Fire, Ice, Electric)
    public float fireRate;                         // Fire rate of the weapon
    public int maxAmmo;
    public float reloadTime;
    public AudioClip emptyMagClip;
    public AudioClip fireSound;
    public AudioClip reloadClip;
    public int damage;                             // Damage dealt by the weapon
    public GameObject gunModel;                    // 3D model of the weapon (Gun model)
    public GameObject firePoint;                    // Fire point where the projectile or VFX originates
    public VisualEffect fireVFXPrefab;               // Prefab for fire-related visual effects (e.g., fireball, electricity)
    public Shader weaponShader;                    // Shader to apply to the weapon model
    public Sprite weaponIcon;                      // Icon to represent the weapon in the UI
    public string weaponName;
    public string weaponDescription;               // Description of the weapon
}
