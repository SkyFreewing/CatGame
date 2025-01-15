namespace CatMerge
{
    public interface IConfigCatalogue
    {
        IGameConfig GameConfig { get; }
        IInputConfig InputConfig { get; }
        IAnimConfig AnimConfig { get; }
        IGameUIConfig GameUIConfig { get; }
        IAppUIConfig AppUIConfig { get; }
    }
}
