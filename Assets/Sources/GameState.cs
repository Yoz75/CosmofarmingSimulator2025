using System;
using UnityEngine;

namespace CS25
{
    public enum State
    {
        None = 0,
        Playing,
        Death,
        Pause
    }

    //Probably it would be better to do there a state machine?
    //I mean current system is pretty simple, but Pause, Unpause and Lose methods are ugly
    public class GameState : MonoBehaviour
    {
        public static GameState Instance
        {
            get; private set;
        }

        public event Action<State> StateChanged;

        private State State_ = State.Playing;
        private State State
        {
            get
            {
                return State_;
            }
            set
            {
                if(State_ == value) return;
                State_ = value;
                StateChanged?.Invoke(State);
            }
        }

        private void Awake()
        {
            Instance = this;
        }
        public void Pause()
        {
            if(State == State.Playing) State = State.Pause;
        }
        public void Unpause()
        {
            if(State == State.Pause) State = State.Playing;
        }
        public void Die()
        {
            if(State == State.Playing) State = State.Death;
        }
    }
}
