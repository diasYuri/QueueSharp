CREATE TABLE dbo.TQueueSharp(
   Id INT IDENTITY (1, 1),
   Topic varchar(255) not null,
   Status int not null,
   Payload varchar(max) not null,
   Created_At Datetime not null,
   Updated_At Datetime not null
   CONSTRAINT [PK_queue] PRIMARY KEY CLUSTERED
    ([id] ASC)
    WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
)
