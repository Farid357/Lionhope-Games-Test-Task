using UnityEngine;

namespace LionhopeGamesTest.Gameplay
{
    public class Item : MonoBehaviour, IItem
    {
        [field: SerializeField] public ItemData Data { get; private set; }
     
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}