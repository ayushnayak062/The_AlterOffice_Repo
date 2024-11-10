using System.Collections;
using UnityEngine;

public static class WeaponHelper
{
    static bool isReloading;
    // Handles firing logic for any weapon
    public static void HandleFire(Weapon weapon, IWeaponBehavior weaponBehavior)
    {
        if (isReloading)
        {
            Debug.Log($"[WeaponHelper] Cannot fire {weapon.Data.name}: Weapon is currently reloading.");
            AudioManager.Instance.PlaySound(weapon.EmptyMagazineSound);
            return;
        }

        if (weapon.CurrentAmmo > 0)
        {
            PlayFireVFX(weapon);

            weapon.CurrentAmmo--;
            Debug.Log($"[WeaponHelper] Firing {weapon.Data.name}. Remaining Ammo: {weapon.CurrentAmmo}/{weapon.Data.name}");

            if (weapon.WeaponAudioSource != null)
            {
                weapon.WeaponAudioSource.PlayOneShot(weapon.FireSound);
            }

            AudioManager.Instance.PlaySound(weapon.FireSound);
        }
        else
        {
            Debug.Log($"[WeaponHelper] {weapon.Data.name} is out of ammo. Starting reload.");
            AudioManager.Instance.PlaySound(weapon.EmptyMagazineSound);
            StartReload(weapon, weaponBehavior);
        }
    }

    // Plays fire VFX if available
    private static void PlayFireVFX(Weapon weapon)
    {
        if (weapon.FireVFXPrefab != null)
        {
            // Get the current VFX from WeaponManager
            var curVfx = WeaponManager.GetCurrentVFX(weapon.Data.weaponType);

            if (curVfx != null)
            {
                // Reset and play the VFX
                curVfx.gameObject.SetActive(true);
                curVfx.gameObject.transform.position = weapon.FirePoint.position;
                curVfx.gameObject.transform.rotation = weapon.FirePoint.rotation;
                curVfx.Play();  // Play the VFX again
                Debug.Log($"[WeaponHelper] Playing fire VFX for {weapon.Data.name}.");
            }
            else
            {
                Debug.Log($"[WeaponHelper] No fire VFX found for {weapon.Data.name}.");
            }
        }
        else
        {
            Debug.Log($"[WeaponHelper] No fire VFX prefab assigned for {weapon.Data.name}.");
        }
    }


    // Starts the reload process
    public static void StartReload(Weapon weapon, IWeaponBehavior weaponBehavior)
    {
        if (isReloading || weapon.CurrentAmmo == weapon.MaxAmmo)
        {
            Debug.Log($"[WeaponHelper] Reload not required for {weapon.Data.name} (Already reloading or ammo full).");
            return;
        }

        isReloading = true;
        Debug.Log($"[WeaponHelper] Initiating reload for {weapon.Data.name}. Current Ammo: {weapon.CurrentAmmo}/{weapon.MaxAmmo}");
        CoroutineRunner.Instance.StartCoroutineFromNonMono(ReloadCoroutine(weapon));
    }

    // Reload coroutine that refills ammo after a delay
    private static IEnumerator ReloadCoroutine(Weapon weapon)
    {
        Debug.Log($"[WeaponHelper] Reloading {weapon.Data.name}... Waiting {weapon.ReloadTime} seconds.");
        AudioManager.Instance.PlaySound(weapon.ReloadClip);
        yield return new WaitForSeconds(weapon.ReloadTime);

        weapon.CurrentAmmo = weapon.MaxAmmo;
        isReloading = false;
        Debug.Log($"[WeaponHelper] Reload complete for {weapon.Data.name}. Ammo refilled to {weapon.CurrentAmmo}/{weapon.MaxAmmo}.");
    }
}