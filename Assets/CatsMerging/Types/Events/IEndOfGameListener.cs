namespace CatMerge
{
    internal interface IEndOfGameListener
    {
        void OnEndOfGame(object e, GameResult result);
    }
}
