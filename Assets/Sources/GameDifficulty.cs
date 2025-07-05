using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CS25
{
    [Serializable]
    public struct DifficultyStage
    {
        public float StageTime;
        public float DifficultyMultiplier;
    }

    public class GameDifficulty : MonoBehaviour
    {
        [SerializeField] private List<DifficultyStage> Stages;

        private float CurrentMultiplier;

        public static GameDifficulty Instance { get; private set; }

        private void Awake()
        {
            Instance = this;

            StartCoroutine(ChangeCoroutine());
        }

        private IEnumerator ChangeCoroutine()
        {
            foreach(var stage in Stages)
            {
                CurrentMultiplier = stage.DifficultyMultiplier;

                yield return new WaitForSeconds(stage.StageTime);
            }
        }

        public float GetDifficulty()
        {
            return CurrentMultiplier;
        }
    }
}