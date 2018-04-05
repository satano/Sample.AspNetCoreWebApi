CREATE TABLE [dbo].[Person](
	[Id] [int] NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
	[Email] [nvarchar](255) NULL,
	[Address] [nvarchar](255) NULL,
	[OwnerId] [int] NOT NULL,
 CONSTRAINT [PK_People] PRIMARY KEY CLUSTERED
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[IdStore](
	[TableName] [nvarchar](100) NOT NULL,
	[LastId] [int] NOT NULL,
 CONSTRAINT [PK_IdStore] PRIMARY KEY CLUSTERED
(
	[TableName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


CREATE PROCEDURE [dbo].[spGetNewId]
(
    @TableName nvarchar(100) = '',
    @NumberOfItems int = 1
)
AS
BEGIN
    SET NOCOUNT ON

       begin transaction
       save transaction transSavePoint

       begin try

             declare @lastId int

             SELECT @lastId = LastId FROM IdStore WITH (XLOCK) WHERE (TableName = @TableName)

             if (@lastId is null)
             begin
                    INSERT INTO IdStore (TableName, LastId) VALUES (@TableName, @NumberOfItems)
                    set @lastId = 1
             end
             else
             begin
                    UPDATE IdStore SET LastId = @lastId + @NumberOfItems WHERE (TableName = @TableName)
                    set @lastId = @lastId + 1
             end

             select @lastId

       end try
    begin catch
        if @@TRANCOUNT > 0
        begin
            rollback transaction transSavePoint
        end

        declare @errorMessage nvarchar(4000)
        declare @errorSeverity int
        declare @errorState int

        select @errorMessage = ERROR_MESSAGE()
        select @errorSeverity = ERROR_SEVERITY()
        select @errorState = ERROR_STATE()

        raiserror (@errorMessage, @errorSeverity, @errorState)

    end catch
       commit transaction
END

CREATE TABLE [dbo].[IdStore](
	[TableName] [nvarchar](100) NOT NULL,
	[LastId] [int] NOT NULL,
 CONSTRAINT [PK_IdStore] PRIMARY KEY CLUSTERED 
(
	[TableName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


