using Microsoft.Xna.Framework;


namespace TeamJRPG
{
    public class LiveEntity : Entity
    {

        public Weapon weapon1;
        public Weapon weapon2;
        public Armor[] armor;
        public Equipment[] equipment;
        public static readonly int CHESTPLATE = 1, HELMET = 0, BOOTS = 2, GLOVES = 3, CAPE = 4, NECKLACE = 6, BELT = 5, RING1 = 7, RING2 = 8;


        public LiveEntity(Vector2 position) : base(position)
        {

        }
    }
}
