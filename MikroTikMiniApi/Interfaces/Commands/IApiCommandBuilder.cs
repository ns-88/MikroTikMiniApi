namespace MikroTikMiniApi.Interfaces.Commands
{
    public interface IApiCommandBuilder
    {
        IApiCommandBuilder AddParameter(string name, string value);

        IApiCommandBuilder AddParameter(string text);

        IApiCommand Build();
    }
}