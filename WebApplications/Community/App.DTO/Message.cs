namespace App.DTO;

public class Message
{
    public Message()
    {
    }

    public Message(params string[] messages)
    {
        Messages = messages;
    }

    public ICollection<string> Messages { get; set; } = new List<string>();
}