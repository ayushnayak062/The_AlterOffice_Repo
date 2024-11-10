using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class WeaponManager : MonoBehaviour
{
    public List<WeaponData> weaponDatas;
    private Dictionary<WeaponType, Weapon> weapons = new Dictionary<WeaponType, Weapon>();
    private Dictionary<WeaponType, GameObject> weaponModels = new Dictionary<WeaponType, GameObject>();
    private static Dictionary<WeaponType, VisualEffect> weaponVFXs = new Dictionary<WeaponType, VisualEffect>();
    private Weapon currentWeapon;
    private GameObject currentWeaponModel;
    private GameObject currentVisualEffect;

    public UIManager uiManager;
    public Transform gunHolder;


    private bool isFiringContinuously = false; // To control continuous firing
    private float fireCooldown = 0f;

    private void Start()
    {
        InitializeWeapons();
        SwitchWeapon(WeaponType.Fire);

    }


    private void InitializeWeapons()
    {
        foreach (var data in weaponDatas)
        {
            Weapon weapon = WeaponFactory.CreateWeapon(data);
            weapons[data.weaponType] = weapon;

            GameObject weaponModelInstance = Instantiate(weapon.GunModel, gunHolder);
            weaponModelInstance.SetActive(false);
            weaponModels[data.weaponType] = weaponModelInstance;

            VisualEffect weaponVfX = Instantiate(weapon.FireVFXPrefab, weapon.FirePoint.transform.position, weapon.FirePoint.transform.rotation);
            weaponVfX.gameObject.SetActive(false);
            weaponVFXs[data.weaponType] = weaponVfX;
        }
    }

    public void SwitchWeapon(WeaponType weaponType)
    {
        if (!weapons.TryGetValue(weaponType, out var newWeapon))
        {
            Debug.LogWarning($"Weapon {weaponType} not found.");
            return;
        }

        if (currentWeaponModel != null) currentWeaponModel.SetActive(false);
        if (currentVisualEffect != null) currentVisualEffect.SetActive(false);

        if (weaponModels.TryGetValue(weaponType, out var newWeaponModel))
        {
            newWeaponModel.SetActive(true);
            newWeaponModel.transform.localPosition = Vector3.zero;
            newWeaponModel.transform.localRotation = Quaternion.identity;
            currentWeaponModel = newWeaponModel;
        }
        else
        {
            Debug.LogWarning($"Model for weapon {weaponType} not found.");
            return;
        }
        if (weaponVFXs.TryGetValue(weaponType, out var newVFX))
        {
            currentVisualEffect = newVFX.gameObject;
        }
        else
        {
            Debug.LogWarning($"Vfx for weapon {weaponType} not found");
            return;
        }
        currentWeapon = newWeapon;
        uiManager.UpdateWeaponUI(newWeapon.Data);
    }
    public static VisualEffect GetCurrentVFX(WeaponType weaponType)
    {
        return weaponVFXs[weaponType];

    }
    public void SwitchWeaponByIndex(int weaponTypeIndex)
    {
        // Cast index to WeaponType and switch
        if (System.Enum.IsDefined(typeof(WeaponType), weaponTypeIndex))
            SwitchWeapon((WeaponType)weaponTypeIndex);
        else
            Debug.LogWarning("Invalid weapon index.");
    }


    public void FireCurrentWeapon()
    {

        if (fireCooldown <= 0f && currentWeapon != null)
        {
            currentWeapon.Fire();
            fireCooldown = currentWeapon.FireRate;
        }
    }

    // Start continuous fire
    public void StartContinuousFire()
    {
        isFiringContinuously = true;
        StartCoroutine(ContinuousFireRoutine());
    }

    // Stop continuous fire
    public void StopContinuousFire()
    {
        isFiringContinuously = false;
    }

    // Coroutine for continuous firing
    private IEnumerator ContinuousFireRoutine()
    {
        while (isFiringContinuously)
        {
            FireCurrentWeapon();
            yield return new WaitForSeconds(currentWeapon?.FireRate ?? 0.1f);
        }
    }

    private void Update()
    {
        // Handle cooldown for single shots
        if (fireCooldown > 0f)
            fireCooldown -= Time.deltaTime;
    }
    public void ReloadCurrentWeapon()
    {
        if (currentWeapon == null)
        {
            Debug.LogError("Current weapon is null!");
            return;
        }

        currentWeapon.Reload();
    }
}


/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public List<WeaponData> weaponDatas;
    private Dictionary<WeaponType, Weapon> weapons = new Dictionary<WeaponType, Weapon>();
    private Dictionary<WeaponType, GameObject> weaponModels = new Dictionary<WeaponType, GameObject>();
    private Weapon currentWeapon;
    private GameObject currentWeaponModel;

    public UIManager uiManager;
    public Transform gunHolder;

    private bool isFiring = false; // Track firing state

    private void Start()
    {
        InitializeWeapons();
        SwitchWeapon(WeaponType.Fire); // Set default weapon
    }

    private void InitializeWeapons()
    {
        foreach (var data in weaponDatas)
        {
            Weapon weapon = WeaponFactory.CreateWeapon(data);
            weapons[data.weaponType] = weapon;

            GameObject weaponModelInstance = Instantiate(weapon.GunModel, gunHolder);
            weaponModelInstance.SetActive(false);
            weaponModels[data.weaponType] = weaponModelInstance;
        }
    }

    public void SwitchWeapon(WeaponType weaponType)
    {
        if (!weapons.TryGetValue(weaponType, out var newWeapon))
        {
            Debug.LogWarning($"Weapon {weaponType} not found.");
            return;
        }

        if (currentWeaponModel != null)
            currentWeaponModel.SetActive(false);

        if (weaponModels.TryGetValue(weaponType, out var newWeaponModel))
        {
            newWeaponModel.SetActive(true);
            newWeaponModel.transform.localPosition = Vector3.zero;
            newWeaponModel.transform.localRotation = Quaternion.identity;
            currentWeaponModel = newWeaponModel;
        }
        else
        {
            Debug.LogWarning($"Model for weapon {weaponType} not found.");
            return;
        }

        currentWeapon = newWeapon;
        uiManager.UpdateWeaponUI(newWeapon.Data);
        Debug.Log($"Switched to {weaponType} weapon.");
    }

    public void SwitchWeaponByIndex(int weaponTypeIndex)
    {
        if (System.Enum.IsDefined(typeof(WeaponType), weaponTypeIndex))
            SwitchWeapon((WeaponType)weaponTypeIndex);
        else
            Debug.LogWarning("Invalid weapon index.");
    }

    // Method to start firing continuously
    public void StartFiring()
    {
        if (!isFiring && currentWeapon != null)
        {
            isFiring = true;
            StartCoroutine(FireContinuously());
        }
    }

    // Method to stop firing
    public void StopFiring()
    {
        isFiring = false;
    }

    private IEnumerator FireContinuously()
    {
        while (isFiring)
        {
            currentWeapon?.Fire();
            yield return new WaitForSeconds(currentWeapon.FireRate);
        }
    }

    public void ReloadCurrentWeapon()
    {
        if (currentWeapon == null)
        {
            Debug.LogError("Current weapon is null!");
            return;
        }

        currentWeapon.Reload();
    }
}*/
