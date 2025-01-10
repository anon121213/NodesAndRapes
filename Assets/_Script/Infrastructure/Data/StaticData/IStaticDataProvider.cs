namespace _Script.Infrastructure.Data.StaticData
{
    public interface IStaticDataProvider
    {
        AssetsReferences AssetsReferences { get; }
        NodesGeneratorConfig NodesGeneratorConfig { get; }
        RopesConfig RopesConfig { get; }
    }
}