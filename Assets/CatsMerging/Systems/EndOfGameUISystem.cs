namespace CatMerge
{
    internal class EndOfGameUISystem : ISystem, IEndOfGameListener
    {
        const string prefabPath = "Prefabs/GameUI/EndOfGameMenu";

        public EndOfGameUISystem(IConfigCatalogue configs)
        {
            EndOfGameEvent.AddListener(this);
        }

        public void OnEndOfGame(object e, GameResult result)
        {
            var endPopup = new EndOfGamePopup();
            endPopup.PrefabAssetPath = prefabPath;

            GameUISystem.CreateGamePopup(endPopup);
        }
    }
}
