public class ElectricWeapon : Weapon
{
    public ElectricWeapon(WeaponData data) : base(data, new ElectricWeaponBehavior()) { }
}
