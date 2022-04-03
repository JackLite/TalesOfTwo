using UnityEngine;

namespace FlyAdventure.Core.Settings
{
    [CreateAssetMenu(menuName = "FlyAdventure/Settings/CoreSettings", fileName = "CoreSettings")]
    public class CoreSettingsContainer : ScriptableObject
    {
        [field:SerializeField]
        public CoreSettings CoreSettings { get; private set; }

        [field:SerializeField]
        public MoveSettings MoveSettings { get; private set; }
    }
}