using UnityEngine;
using UnityEngine.VFX;

public abstract class Weapon
{
    public WeaponData Data { get; private set; }  // Reference to WeaponData
    protected IWeaponBehavior WeaponBehavior;  // Unified behavior for fire and reload

    public GameObject GunModel => Data.gunModel;  // Accessor for gun model
    public Transform FirePoint => Data.firePoint.transform;  // Accessor for fire point
    public Shader WeaponShader => Data.weaponShader;  // Accessor for the weapon shader
    public VisualEffect FireVFXPrefab => Data.fireVFXPrefab;  // Accessor for VFX Prefab
    public float FireRate => Data.fireRate;  // Accessor for fire rate
    public int Damage => Data.damage;  // Accessor for damage

    public AudioClip FireSound => Data.fireSound;
    public AudioSource WeaponAudioSource;
    public AudioClip EmptyMagazineSound => Data.emptyMagClip;
    public AudioClip ReloadClip => Data.reloadClip;
    // Ammo count properties
    public int CurrentAmmo { get; set; }
    public int MaxAmmo { get; private set; }

    // Reload time in seconds
    public float ReloadTime => Data.reloadTime;

    protected Weapon(WeaponData data, IWeaponBehavior weaponBehavior)
    {
        Data = data;
        WeaponBehavior = weaponBehavior;

        // Initialize ammo and reload properties from WeaponData
        MaxAmmo = data.maxAmmo;
        CurrentAmmo = MaxAmmo;  // Start with full ammo
    }

    // Fire the weapon
    public void Fire()
    {
        WeaponBehavior.Fire(this);  // Delegate firing to the WeaponBehavior

    }

    // Reload the weapon
    public void Reload()
    {
        WeaponBehavior.Reload(this);  // Delegate reloading to the WeaponBehavior
    }


}
