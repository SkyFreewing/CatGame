namespace CatMerge
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

            //UI
            Add(new AppUISystem(configCatalogue));
            Add(new GameUISystem(configCatalogue));
            Add(new EndOfGameUISystem(configCatalogue));

            //Spawn
            Add(new BoardSpawnSystem(configCatalogue));
           
            //Scoring
            Add(new ScoreCountingSystem(configCatalogue));         
        }
    }
}
