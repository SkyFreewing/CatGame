using CatMerge;

namespace Assets.CatsMerging.Classes
{
    internal class GameSystems : Systems 
    {
        public GameSystems(IConfigCatalogue configCatalogue)
        {  
            //Core Mechanics
            Add(new InputRead(configCatalogue));
            Add(new MoveElements(configCatalogue));

            //Gameplay 
            Add(new BoardSpawn(configCatalogue));
        }
    }
}
