namespace Formula1.Contracts.ExternalServices;

public interface ISlackClient
{
    void SendMessage(string message);
}
