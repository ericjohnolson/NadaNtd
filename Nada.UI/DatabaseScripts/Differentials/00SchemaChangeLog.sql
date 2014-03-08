CREATE TABLE [SchemaChangeLog](
   [ID] AUTOINCREMENT,
   [MajorReleaseNumber] TEXT(2),
   [MinorReleaseNumber] TEXT(2),
   [PointReleaseNumber] TEXT(4),
   [ScriptName] TEXT(50),
   [DateApplied] DATETIME,
   CONSTRAINT [PrimaryKey] PRIMARY KEY ([ID])
);

INSERT INTO [SchemaChangeLog]
       ([MajorReleaseNumber]
       ,[MinorReleaseNumber]
       ,[PointReleaseNumber]
       ,[ScriptName]
       ,[DateApplied])
VALUES
       ('01'
       ,'00'
       ,'0000'
       ,'00SchemaChangeLog.sql'
       ,Now());