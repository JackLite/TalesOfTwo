namespace TheTalesOfTwo.Core.Hp
{
    public struct HpComponent
    {
        public int currentHp;
        public readonly int maxHp;

        public HpComponent(int maxHp)
        {
            this.maxHp = maxHp;
            currentHp = maxHp;
        }
    }
}