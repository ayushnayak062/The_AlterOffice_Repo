public class FireWeapon : Weapon
{
    public FireWeapon(WeaponData data) : base(data, new FireWeaponBehavior()) { }
}
