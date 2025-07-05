using System;
using System.Collections.Generic;
using UnityEngine;

namespace CS25
{
    [Serializable]
    public struct GrowStage
    {
        public Sprite Sprite;
        public float StageTime;
    }

    [CreateAssetMenu(menuName = "CS25/Plant")]
    public class Plant : ScriptableObject
    {
        public long Cost;
        public float RotChance;
        public Sprite RottenSprite;
        public List<GrowStage> GrowStages;
    }
}