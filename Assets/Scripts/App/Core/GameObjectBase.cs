using UnityEngine;

namespace App.Gameplay.GameEntities
{
    public class GameObjectBase
    {
        public GameObject SelfObject { get; }

        protected GameObjectBase(GameObject selfObject)
        {
            SelfObject = selfObject;
        }
    }
}