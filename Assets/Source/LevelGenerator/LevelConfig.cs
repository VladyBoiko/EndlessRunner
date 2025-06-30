using UnityEngine;

namespace Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Data/Level/LevelConfig", order = 0)]
    public class LevelConfig : ScriptableObject
    {
        public float DistanceToSpawn;
        public float DistanceToDespawn;
    }
}