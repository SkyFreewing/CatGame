namespace CatMerge
{
    internal class EndOfGameUISystem : ISystem, IEndOfGameListener
    {
        public EndOfGameUISystem(IConfigCatalogue configs)
        {
            EndOfGameEvent.AddListener(this);
        }

        public void OnEndOfGame(object e, GameResult result)
        {
            var endPopup = new EndOfGamePopup();
            endPopup.Score = result.Score;

            GameUISystem.CreateGamePopup(endPopup);

            var sound = SoundSystem.Instance;
            sound.PlayUISound(false);
            sound.DampenBGMVolume(true);
        }
    }
}
