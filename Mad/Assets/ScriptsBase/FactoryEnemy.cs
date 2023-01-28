public class FactoryEnemy : FactoryAbstractBase<Enemy>
{
    public override Enemy Create(Enemy prefab)
    {
        return Instantiate(prefab);
    }
}
