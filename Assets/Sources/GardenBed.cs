
using System;
using System.Collections;
using UnityEngine;

namespace CS25
{
    [RequireComponent(typeof(SpriteRenderer), typeof(AudioSource))]
    public class GardenBed : CSBehaviour
    {
        #region states
        private abstract class GrowTimeStage
        {
            protected GardenBed Bed;
            public void SetBed(GardenBed bed) => Bed = bed;
            public abstract GrowTimeStage NextStage();
            public abstract void OnClick();
        }

        private class UnplantedStage : GrowTimeStage
        {
            public override GrowTimeStage NextStage()
            {
                if(PlantedBedsCount >= MaxPlantedBeds)
                {
                    PlantedLimitReached?.Invoke();
                    return this; 
                }

                PlantedBedsCount++;
                var bed = new PlantedStage();
                bed.SetBed(Bed);
                return bed;
            }

            public override void OnClick()
            {
                Bed.PlantPlant();
            }
        }


        private class PlantedStage : GrowTimeStage
        {
            public override GrowTimeStage NextStage()
            {
                if(Bed.Plant.RotChance >= UnityEngine.Random.value)
                {
                    Bed.Renderer.sprite = Bed.Plant.RottenSprite;
                    Bed.StopCoroutine(Bed.GrowCoroutine());
                    var bed = new RottenStage();
                    bed.SetBed(Bed);

                    return bed;
                }
                else
                {
                    Bed.SoundPlayer.clip = Bed.GrewSound;
                    Bed.SoundPlayer.Play();

                    var bed = new GrewStage();
                    bed.SetBed(Bed);

                    return bed;
                }    
            }

            public override void OnClick()
            {
                return;
            }
        }

        private class GrewStage : GrowTimeStage
        {
            public override GrowTimeStage NextStage()
            {
                PlantedBedsCount--;

                var bed = new UnplantedStage();
                bed.SetBed(Bed);

                return bed;
            }

            public override void OnClick()
            {
                Bed.Collect();
            }
        }

        private class RottenStage : GrowTimeStage
        {
            public override GrowTimeStage NextStage()
            {
                PlantedBedsCount--;
                var bed = new UnplantedStage();
                bed.SetBed(Bed);

                return bed;
            }

            public override void OnClick()
            {
                Bed.CutPlant();
            }
        }
        #endregion

        public static event Action PlantedLimitReached;

        private const int MaxPlantedBeds = 7;

        [SerializeField] private Plant Plant;
        [SerializeField] private AudioClip GrewSound, CutSound;

        private SpriteRenderer Renderer;
        private AudioSource SoundPlayer;

        private GrowTimeStage Stage = new UnplantedStage();

        private bool CanBeClicked = true;

        private static int PlantedBedsCount_;
        public static int PlantedBedsCount
        {
            get
            { 
                return PlantedBedsCount_;
            }

            private set
            {
                if(value <= MaxPlantedBeds)
                    PlantedBedsCount_ = value;
                else
                {
                     PlantedLimitReached?.Invoke();
                }
            }
        }

        protected override void OnStart()
        {
            Renderer = GetComponent<SpriteRenderer>();
            SoundPlayer = GetComponent<AudioSource>();

            GameState.Instance.StateChanged += (state) => 
            {
                if(state == State.Death)
                {
                    CanBeClicked = false;
                    PlantedBedsCount_ = 0;
                }
            };
        }

        protected override void OnClickEnter()
        {
            if(!CanBeClicked) return;

            if(Stage is GrewStage or RottenStage)
            {
                Stage.SetBed(this);
                Stage.OnClick();
                return;
            }

            if(Stage is UnplantedStage && PlantedBedsCount >= MaxPlantedBeds)
            {
                PlantedLimitReached?.Invoke();
                return;
            }

            Stage.SetBed(this);
            Stage.OnClick();
        }

        /// <summary>
        /// Plant the plant
        /// </summary>
        private void PlantPlant() => StartCoroutine(GrowCoroutine());

        private void CutPlant()
        {
            SoundPlayer.clip = CutSound;
            SoundPlayer.Play();

            Stage = Stage.NextStage();
            Renderer.sprite = Plant.GrowStages[0].Sprite;
        }

        private IEnumerator GrowCoroutine()
        {
            Stage = Stage.NextStage();

            foreach(GrowStage stage in Plant.GrowStages)
            {
                Renderer.sprite = stage.Sprite;
                yield return new WaitForSeconds(stage.StageTime);
            }

            Stage = Stage.NextStage();
        }

        private void Collect()
        {
            Debug.LogError("TODO: ADD SOUND");
            PlayerCharacteristics.Instance.AddMoney(Plant.Cost);
            Stage = Stage.NextStage();
            Renderer.sprite = Plant.GrowStages[0].Sprite;
        }
    }
}