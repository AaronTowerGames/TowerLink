public class FactorySkull : FactoryAbstractBase<EnemyStatusBar>
{
    public override EnemyStatusBar Create(EnemyStatusBar prefab)
    {
        return Instantiate(prefab);
    }
}

