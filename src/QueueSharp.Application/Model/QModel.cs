namespace QueueSharp.Application.Model;

public class QModel
{
    public long Id { get; set; }
    public QStatus Status { get; set; }
    public string Payload { get; set; }

    public QModel() { }
    public QModel(string payload)
    {
        Payload = payload;
        Status = QStatus.NoRead;
    }
}

public enum QStatus
{
    NoRead = 0,
    WasRead = 1
}