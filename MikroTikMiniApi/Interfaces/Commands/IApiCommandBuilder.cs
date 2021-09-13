namespace MikroTikMiniApi.Interfaces.Commands
{
    public interface IApiCommandBuilder
    {
        IApiCommandBuilder AddParameter(string name, string value);
        IApiCommand Build();
    }
}