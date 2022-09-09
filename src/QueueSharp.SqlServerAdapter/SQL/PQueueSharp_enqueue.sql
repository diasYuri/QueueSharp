CREATE OR ALTER PROCEDURE dbo.PQueueSharp_enqueue
    @Topic varchar(255),
    @Status int,
    @Payload varchar(max)
AS
BEGIN
    INSERT INTO dbo.TQueueSharp
    (Topic, Status, Payload, Created_At, Updated_At)
    VALUES
        (@Topic, @Status, @Payload, GETDATE(), GETDATE())
END;
GO