// File: IceWeapon.cs
public class IceWeapon : Weapon
{
    public IceWeapon(WeaponData data) : base(data, new IceWeaponBehavior()) { }

    // IceWeapon-specific logic can go here
}
