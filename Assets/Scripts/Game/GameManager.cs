using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pinball.Scripts.Game
{
    /// <summary>
    /// The game manager.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private Utils.References.IntegerReference score;
        [SerializeField]
        private Utils.References.IntegerReference lives;
        [SerializeField]
        private Utils.References.Vector2Reference spawnPosition;
        [SerializeField]
        private Utils.GameEvent restartEvent;
        [SerializeField]
        private GameObject ballPrefab;

        private void Start()
        {
            this.lives.Reset();
            this.score.Reset();
        }

        private void OnEnable()
        {
            this.lives.ChangedValueEvent += this.OnLivesChanged;
        }

        private void OnDisable()
        {
            this.lives.ChangedValueEvent -= this.OnLivesChanged;
        }

        /// <summary>
        /// Updates the game.
        /// </summary>
        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit(0);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                this.ResetGame();
            }
        }

        private void ResetGame()
        {
            this.lives.Value = 0;
            this.score.Value = 0;
            this.restartEvent?.Call();

            GameObject instantiated = Instantiate(this.ballPrefab);
            instantiated.transform.position = this.spawnPosition.Value;
        }

        /// <summary>
        /// Called when the lives have changed.
        /// </summary>
        /// <param name="newLives">The new lives.</param>
        private void OnLivesChanged(int newLives)
        {
            if(newLives <= 0)
            {
                // TODO: IMPLEMENTATION
                return;
            }
            GameObject instantiated = Instantiate(this.ballPrefab);
            instantiated.transform.position = this.spawnPosition.Value;
        }
    }
}
