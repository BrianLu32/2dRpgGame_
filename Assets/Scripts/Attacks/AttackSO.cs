using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "Player Normal Attack", menuName = "Player Attack")]
public class AttackSO : ScriptableObject
{
    public AnimatorOverrideController animatorOV;
    // public float attackDelay;
}
