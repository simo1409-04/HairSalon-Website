BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124114225_AddedEmailColumnToHairdressers'
)
BEGIN
    ALTER TABLE [HairDressers] ADD [Email] nvarchar(max) NOT NULL DEFAULT N'';
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124114225_AddedEmailColumnToHairdressers'
)
BEGIN
    EXEC(N'UPDATE [HairDressers] SET [Email] = N''simeon.ivanov@gmail.com''
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124114225_AddedEmailColumnToHairdressers'
)
BEGIN
    EXEC(N'UPDATE [HairDressers] SET [Email] = N''viktoria.petrova@gmail.com''
    WHERE [Id] = 2;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124114225_AddedEmailColumnToHairdressers'
)
BEGIN
    EXEC(N'UPDATE [HairDressers] SET [Email] = N''martin.ivanov@gmail.com''
    WHERE [Id] = 3;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124114225_AddedEmailColumnToHairdressers'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261124114225_AddedEmailColumnToHairdressers', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124120652_RemovedDefaultConstraint'
)
BEGIN

                    DECLARE @ConstraintName nvarchar(200);
                    SELECT @ConstraintName = dc.name
                    FROM sys.default_constraints dc
                    JOIN sys.columns c
                        ON dc.parent_object_id = c.object_id
                       AND dc.parent_column_id = c.column_id
                    WHERE dc.parent_object_id = OBJECT_ID(N'[dbo].[HairDressers]')
                      AND c.name = N'Email';

                    IF @ConstraintName IS NOT NULL
                        EXEC(N'ALTER TABLE [dbo].[HairDressers] DROP CONSTRAINT [' + @ConstraintName + N']');
                
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124120652_RemovedDefaultConstraint'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261124120652_RemovedDefaultConstraint', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124121411_fixedEmailMaxLength'
)
BEGIN
    DECLARE @var nvarchar(max);
    SELECT @var = QUOTENAME([d].[name])
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[HairDressers]') AND [c].[name] = N'Email');
    IF @var IS NOT NULL EXEC(N'ALTER TABLE [HairDressers] DROP CONSTRAINT ' + @var + ';');
    ALTER TABLE [HairDressers] ALTER COLUMN [Email] nvarchar(254) NOT NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124121411_fixedEmailMaxLength'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261124121411_fixedEmailMaxLength', N'10.0.14');
END;

COMMIT;
GO

BEGIN TRANSACTION;
IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    ALTER TABLE [Reservations] ADD [ClientEmail] nvarchar(254) NULL;
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 2;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 3;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 4;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 5;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 6;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 7;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 8;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 9;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 10;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 11;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 12;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 13;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 14;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 15;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 16;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 17;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 18;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 19;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 20;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 21;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 22;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 23;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 24;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 25;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 26;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 27;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 28;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 29;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 30;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 31;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 32;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 33;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 34;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 35;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 36;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 37;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    EXEC(N'UPDATE [Reservations] SET [ClientEmail] = NULL
    WHERE [Id] = 38;
    SELECT @@ROWCOUNT');
END;

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20261124180853_AddedClientEmailForReservations'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261124180853_AddedClientEmailForReservations', N'10.0.14');
END;

COMMIT;
GO

