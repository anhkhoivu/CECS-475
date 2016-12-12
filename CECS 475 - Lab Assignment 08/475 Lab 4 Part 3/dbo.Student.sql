CREATE TABLE [dbo].[Student] (
    [StudentID]   INT          IDENTITY (1, 1) NOT NULL,
    [StudentName] VARCHAR (50) NULL,
    [StandardId]  INT          NULL,
    [RowVersion]  ROWVERSION   NOT NULL,
    CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED ([StudentID] ASC),
    CONSTRAINT [FK_Student_Standard] FOREIGN KEY ([StandardId]) REFERENCES [dbo].[Standard] ([StandardId]) ON DELETE CASCADE

);
GO
UPDATE [dbo].[Student]
SET StudentID = Student.StudentID
WHERE StudentID = StudentID;
GO

ALTER TABLE [dbo].[Student] NOCHECK CONSTRAINT [FK_Student_Standard]
GO