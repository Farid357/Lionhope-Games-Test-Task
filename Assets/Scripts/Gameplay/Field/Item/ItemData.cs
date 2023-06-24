using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    [CreateAssetMenu(menuName = "Create/Item", fileName = "Item", order = 0)]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public int Level { get; private set; }
    }
}