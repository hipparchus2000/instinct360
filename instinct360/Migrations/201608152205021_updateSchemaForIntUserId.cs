namespace instinct360.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateSchemaForIntUserId : DbMigration
    {
        public override void Up()
        {
            Sql(@"
IF (SELECT OBJECT_ID('tempdb..#tmpErrors')) IS NOT NULL DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
BEGIN TRANSACTION
GO
PRINT N'Dropping [dbo].[FK_dbo.ReviewSections_dbo.Reviews_Review_Id]...';


GO
ALTER TABLE [dbo].[ReviewSections] DROP CONSTRAINT [FK_dbo.ReviewSections_dbo.Reviews_Review_Id];


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
/*
The type for column UserId in table [dbo].[Reviews] is currently  UNIQUEIDENTIFIER NOT NULL but is being changed to  INT NOT NULL. There is no implicit or explicit conversion.
*/
GO
PRINT N'Starting rebuilding table [dbo].[Reviews]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Reviews] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [ReviewTemplateName] NVARCHAR (MAX) NULL,
    [UserId]             INT            NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.Reviews1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Reviews])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Reviews] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Reviews] ([Id], [ReviewTemplateName], [UserId])
        SELECT   [Id],
                 [ReviewTemplateName],
                 [UserId]
        FROM     [dbo].[Reviews]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Reviews] OFF;
    END

DROP TABLE [dbo].[Reviews];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Reviews]', N'Reviews';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.Reviews1]', N'PK_dbo.Reviews', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[FK_dbo.ReviewSections_dbo.Reviews_Review_Id]...';


GO
ALTER TABLE [dbo].[ReviewSections] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.ReviewSections_dbo.Reviews_Review_Id] FOREIGN KEY ([Review_Id]) REFERENCES [dbo].[Reviews] ([Id]);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO

IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT N'The transacted portion of the database update succeeded.'
COMMIT TRANSACTION
END
ELSE PRINT N'The transacted portion of the database update failed.'
GO
DROP TABLE #tmpErrors
GO
PRINT N'Checking existing data against newly created constraints';




GO
ALTER TABLE [dbo].[ReviewSections] WITH CHECK CHECK CONSTRAINT [FK_dbo.ReviewSections_dbo.Reviews_Review_Id];


GO
PRINT N'Update complete.';


GO



--------------------------------
IF (SELECT OBJECT_ID('tempdb..#tmpErrors')) IS NOT NULL DROP TABLE #tmpErrors
GO
CREATE TABLE #tmpErrors (Error int)
GO
SET XACT_ABORT ON
GO
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
GO
BEGIN TRANSACTION
GO
PRINT N'Dropping [dbo].[FK_dbo.ReviewTemplateSections_dbo.ReviewTemplates_ReviewTemplate_Id]...';


GO
ALTER TABLE [dbo].[ReviewTemplateSections] DROP CONSTRAINT [FK_dbo.ReviewTemplateSections_dbo.ReviewTemplates_ReviewTemplate_Id];


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
/*
The type for column UserId in table [dbo].[ReviewTemplates] is currently  UNIQUEIDENTIFIER NOT NULL but is being changed to  INT NOT NULL. There is no implicit or explicit conversion.
*/
GO
PRINT N'Starting rebuilding table [dbo].[ReviewTemplates]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_ReviewTemplates] (
    [Id]                 INT            IDENTITY (1, 1) NOT NULL,
    [ReviewTemplateName] NVARCHAR (MAX) NULL,
    [UserId]             INT            NOT NULL,
    CONSTRAINT [tmp_ms_xx_constraint_PK_dbo.ReviewTemplates1] PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[ReviewTemplates])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ReviewTemplates] ON;
        INSERT INTO [dbo].[tmp_ms_xx_ReviewTemplates] ([Id], [ReviewTemplateName], [UserId])
        SELECT   [Id],
                 [ReviewTemplateName],
                 [UserId]
        FROM     [dbo].[ReviewTemplates]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_ReviewTemplates] OFF;
    END

DROP TABLE [dbo].[ReviewTemplates];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_ReviewTemplates]', N'ReviewTemplates';

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_constraint_PK_dbo.ReviewTemplates1]', N'PK_dbo.ReviewTemplates', N'OBJECT';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO
PRINT N'Creating [dbo].[FK_dbo.ReviewTemplateSections_dbo.ReviewTemplates_ReviewTemplate_Id]...';


GO
ALTER TABLE [dbo].[ReviewTemplateSections] WITH NOCHECK
    ADD CONSTRAINT [FK_dbo.ReviewTemplateSections_dbo.ReviewTemplates_ReviewTemplate_Id] FOREIGN KEY ([ReviewTemplate_Id]) REFERENCES [dbo].[ReviewTemplates] ([Id]);


GO
IF @@ERROR <> 0
   AND @@TRANCOUNT > 0
    BEGIN
        ROLLBACK;
    END

IF @@TRANCOUNT = 0
    BEGIN
        INSERT  INTO #tmpErrors (Error)
        VALUES                 (1);
        BEGIN TRANSACTION;
    END


GO

IF EXISTS (SELECT * FROM #tmpErrors) ROLLBACK TRANSACTION
GO
IF @@TRANCOUNT>0 BEGIN
PRINT N'The transacted portion of the database update succeeded.'
COMMIT TRANSACTION
END
ELSE PRINT N'The transacted portion of the database update failed.'
GO
DROP TABLE #tmpErrors
GO
PRINT N'Checking existing data against newly created constraints';




GO
ALTER TABLE [dbo].[ReviewTemplateSections] WITH CHECK CHECK CONSTRAINT [FK_dbo.ReviewTemplateSections_dbo.ReviewTemplates_ReviewTemplate_Id];


GO
PRINT N'Update complete.';


GO


");
        }
        
        public override void Down()
        {
        }
    }
}
