namespace CatMerge
{
    internal class EndOfGamePopup : GamePlayBlockerPopup
    {
        public EndOfGamePopup() : base()
        {
            PrefabAssetPath = "Prefabs/EndgamePopup";
        }

        public override void Show()
        {
            base.Show();


            ///TODO: Animations
        }

        public override void Hide()
        {
            base.Hide();

            ///TODO: Animations
        }
    }
}
