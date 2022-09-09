CREATE OR ALTER PROCEDURE dbo.PQueueSharp_dequeue
    @Topic varchar(255)
AS
BEGIN
    
    BEGIN TRANSACTION
    
            DECLARE @NextID INTEGER
            DECLARE @Status INTEGER
            DECLARE @Payload VARCHAR(max)
    
    SELECT TOP 1
        @NextID = Id,
        @Status = Status,
        @Payload = Payload
    FROM dbo.TQueueSharp WITH (ROWLOCK, READPAST)
    WHERE
        Status = 0
      AND
        Topic = @Topic
    ORDER BY Id ASC;
    
    IF @NextID is not null
    BEGIN
        UPDATE dbo.TQueueSharp SET Status = 1, Updated_At = GETDATE() WHERE Id = @NextId;
        SELECT @NextID as Id, @Payload as Payload, @Status as Status, @Topic as Topic;
    END
    
    COMMIT TRANSACTION

END;
GO