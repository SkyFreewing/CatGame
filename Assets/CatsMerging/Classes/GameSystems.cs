using CatMerge;

namespace Assets.CatsMerging.Classes
{
    internal class GameSystems : Systems 
    {
        public GameSystems(IConfigCatalogue configCatalogue)
        {  
            //Core Mechanics
            Add(new InputReadSystem(configCatalogue));
            Add(new MoveElementsSystem(configCatalogue));
            Add(new EndOfGameSystem(configCatalogue));
            Add(new GameplayBlockersSystem(configCatalogue));

            //Gameplay 
            Add(new BoardSpawnSystem(configCatalogue));
        }
    }
}
