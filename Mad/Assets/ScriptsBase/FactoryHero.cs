public class FactoryHero : FactoryAbstractBase<Hero>
{
    public override Hero Create(Hero prefab)
    {
        return Instantiate(prefab);
    }
}
