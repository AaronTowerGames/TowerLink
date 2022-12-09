using UnityEngine;

[CreateAssetMenu(menuName = "TowerData", fileName = "Tower")]
public class TowerData : ScriptableObject
{
    [SerializeField]
    private float _damage;
    public float Damage { get => _damage; }

    [SerializeField]
    private string _name;
    public string NameTower { get => _name; }

    [SerializeField]
    private TowerModel _towerModel;
    public TowerModel ModelTower { get => _towerModel; }
}
