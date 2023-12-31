namespace Barracks5e
{

    public class Equipment
    {
        public static int ConvertWeaponToStats(Weapon weapon)
        {
            //TODO IMPLEMENT
            return 0;
        }

        public static int ConvertArmorToAC(Armor armor)
        {
            //TODO IMPLEMENT
            return 0;
        }
    }

    public enum Weapon
    {
        Unarmed,
        HandAxe,
        Javelin,
        GreatAxe
    }

    public enum Armor
    {
        Unarmored
    }

    public enum TravelEquipment
    {
        Backpack,
        Bedroll,
        MessKit,
        Tinderbox,
        Torch,
        Ration,
        Waterskin,
        HempenRope
    }

    public enum EquipmentPackType
    {
        ExplorersPack
    }
}