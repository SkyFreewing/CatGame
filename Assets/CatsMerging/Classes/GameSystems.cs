using CatMerge;

namespace Assets.CatsMerging.Classes
{
    internal class GameSystems : Systems 
    {
        public GameSystems(IConfigCatalogue configCatalogue)
        {
            Add(new BoardSpawn(configCatalogue));
            Add(new InputRead(configCatalogue));
        }
    }
}
