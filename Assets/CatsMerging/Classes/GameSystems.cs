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

            //Gameplay 
            Add(new BoardSpawnSystem(configCatalogue));
        }
    }
}
