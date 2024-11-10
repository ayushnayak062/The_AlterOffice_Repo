public static class WeaponFactory
{
    public static Weapon CreateWeapon(WeaponData data)
    {
        // Decide the firing behavior and create the appropriate weapon class
        switch (data.weaponType)
        {
            case WeaponType.Fire:

                return new FireWeapon(data); // Instantiate FireWeapon
            case WeaponType.Ice:
                return new IceWeapon(data); // Instantiate IceWeapon (you need to implement this class similarly)
            case WeaponType.Electric:
                return new ElectricWeapon(data); // Instantiate ElectricWeapon (implement this class similarly)
            default:
                return null;
        }
    }
}
