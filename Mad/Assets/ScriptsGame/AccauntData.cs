using System;

[Serializable]
public class AccauntData
{
    public int id; //Позже для сети может пригодиться/таблицы рекордов
    public int idWeaponLeftArm;
    public int idWeaponRightArm;
    public int exp;
    public int idHero;
    public int reputation; //МЕТА параметр: Позже мб пригодится
    public bool isLevelToLoad;
    public int lastlevel;
}