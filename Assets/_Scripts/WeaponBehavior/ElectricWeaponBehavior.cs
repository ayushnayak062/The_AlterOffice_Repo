public class ElectricWeaponBehavior : IWeaponBehavior
{
    public void Fire(Weapon weapon)
    {
        WeaponHelper.HandleFire(weapon, this);
    }

    public void Reload(Weapon weapon)
    {
        WeaponHelper.StartReload(weapon, this);
    }
}
