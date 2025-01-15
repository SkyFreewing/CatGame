using UnityEngine;

namespace CatMerge
{
    internal abstract class GamePlayBlockerPopup : MonoBehaviour, IPopup, IGameplayBlocker
    {
        public GameObject GameObject { get; set; }
        public string PrefabAssetPath { get; set; }

        GameplayBlockerAddedEvent _gameplayBlockerAddedEvent;
        GameplayBlockerRemovedEvent _gameplayBlockerRemovedEvent;

        public GamePlayBlockerPopup()
        {
            _gameplayBlockerAddedEvent = new GameplayBlockerAddedEvent();
            _gameplayBlockerRemovedEvent = new GameplayBlockerRemovedEvent();
        }

        public virtual void Show() 
        {
            _gameplayBlockerAddedEvent.OnGameplayBlockerAdded(this);
        }

        public virtual void Hide() 
        {
            _gameplayBlockerRemovedEvent.OnGameplayBlockerRemoved(this);
        }
    }
}
