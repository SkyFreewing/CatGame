namespace CatMerge
{
    public interface IConfigCatalogue
    {
        IGameConfig GameConfig { get; }
        IInputConfig InputConfig { get; }
        IAnimConfig AnimConfig { get; }
    }
}
