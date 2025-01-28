using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum classType 
{
    Warrior,
    Ranger,
    Mage,
    Rouoge
}

[CreateAssetMenu(fileName = "BaseWeapon", menuName = "Weapon Data")]
public class BaseWeapon : ScriptableObject
{
    [Header("Info")]
    public new string name;
    public classType classType;

    [Header("Stats")]
    public float damage;
}
