using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CatMerge
{
    public class AppController : MonoBehaviour, IAnyGameplayBlockersListener
    {
        public ConfigCatalogue ConfigCatalogue;

        Systems GameSystems;
        bool _pauseGame;

        void Awake() 
        {
            AnyGameplayBlockersEvent.AddListener(this);
        }

        void Start()
        {
            if (ConfigCatalogue == null)
                ConfigCatalogue = new ConfigCatalogue();

            //DOTween
            DOTween.Init();

            //Adding all features
            GameSystems = new GameSystems(ConfigCatalogue);

            //Feature starts here
            GameSystems.Startup();
        }

        void Update()
        {
            //Feature execute loops here
            if(!_pauseGame)
                GameSystems.Execute();
        }

        //Pause all game features, while any UI/Blockers are showing
        public void OnAnyGameplayBlockers(object e, List<IGameplayBlocker> blockers) => _pauseGame = blockers.Any();
    }
}