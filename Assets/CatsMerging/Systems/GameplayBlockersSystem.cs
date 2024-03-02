using System.Collections.Generic;

namespace CatMerge
{
    internal class GameplayBlockersSystem : ISystem, IGameplayBlockerAddedListener, IGameplayBlockerRemovedListener
    {
        List<IGameplayBlocker> _gameplayBlockers = new List<IGameplayBlocker>();
        AnyGameplayBlockersEvent _anyGameplayBlockersEvent;

        public GameplayBlockersSystem(IConfigCatalogue configs)
        {
            _anyGameplayBlockersEvent = new AnyGameplayBlockersEvent();

            GameplayBlockerAddedEvent.AddListener(this);
            GameplayBlockerRemovedEvent.AddListener(this);
        }

        public void OnGameplayBlockerAdded(object e, IGameplayBlocker input)
        {
            _gameplayBlockers.Add(input);
            _anyGameplayBlockersEvent.OnAnyGameplayBlockers(_gameplayBlockers);
        }

        public void OnGameplayBlockerRemoved(object e, IGameplayBlocker input)
        {
            _gameplayBlockers.Remove(input);
            _anyGameplayBlockersEvent.OnAnyGameplayBlockers(_gameplayBlockers);
        }
    }
}
