BEGIN TRANSACTION;
GO

ALTER TABLE [service].[Window] DROP CONSTRAINT [FK_Window_Service_ServiceId];
GO

ALTER TABLE [service].[Window] DROP CONSTRAINT [FK_Window_User_UserId];
GO

DROP TABLE [service].[Request];
GO

DELETE FROM [identity].[User]
WHERE [Id] = 'b8a495d4-05d9-4f4f-8653-bf6f1e7f4723';
SELECT @@ROWCOUNT;

GO

DELETE FROM [identity].[Role]
WHERE [Id] = 'c49c0336-0283-45f9-9f70-a2015b49315e';
SELECT @@ROWCOUNT;

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[service].[Window]') AND [c].[name] = N'UserId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [service].[Window] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [service].[Window] ALTER COLUMN [UserId] uniqueidentifier NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[service].[Window]') AND [c].[name] = N'ServiceId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [service].[Window] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [service].[Window] ALTER COLUMN [ServiceId] uniqueidentifier NULL;
GO

CREATE TABLE [service].[Ticket] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedTime] datetime2 NOT NULL,
    [FinishedTime] datetime2 NULL,
    [ServiceStatus] int NOT NULL,
    [WindowId] uniqueidentifier NULL,
    [ServiceId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Ticket] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Ticket_Service_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [service].[Service] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Ticket_Window_WindowId] FOREIGN KEY ([WindowId]) REFERENCES [service].[Window] ([Id]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'RoleName') AND [object_id] = OBJECT_ID(N'[identity].[Role]'))
    SET IDENTITY_INSERT [identity].[Role] ON;
INSERT INTO [identity].[Role] ([Id], [RoleName])
VALUES ('e0048231-9ffe-481e-8478-21df1cdd4bf3', N'admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'RoleName') AND [object_id] = OBJECT_ID(N'[identity].[Role]'))
    SET IDENTITY_INSERT [identity].[Role] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'RoleId', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[identity].[User]'))
    SET IDENTITY_INSERT [identity].[User] ON;
INSERT INTO [identity].[User] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [RoleId], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES ('917f24c0-7d83-4976-8858-8641bcf0c0d7', 0, N'b57c5563-7c29-4b13-8f52-49c1e2511af7', N'admin@eq.com', CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, NULL, N'D404559F602EAB6FD602AC7680DACBFAADD13630335E951F097AF3900E9DE176B6DB28512F2E000B9D04FBA5133E8B1C6E8DF59DB3A8AB9D60BE4B97CC9E81DB', NULL, CAST(0 AS bit), 'e0048231-9ffe-481e-8478-21df1cdd4bf3', NULL, CAST(0 AS bit), NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'RoleId', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[identity].[User]'))
    SET IDENTITY_INSERT [identity].[User] OFF;
GO

CREATE INDEX [IX_Ticket_ServiceId] ON [service].[Ticket] ([ServiceId]);
GO

CREATE INDEX [IX_Ticket_WindowId] ON [service].[Ticket] ([WindowId]);
GO

ALTER TABLE [service].[Window] ADD CONSTRAINT [FK_Window_Service_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [service].[Service] ([Id]) ON DELETE NO ACTION;
GO

ALTER TABLE [service].[Window] ADD CONSTRAINT [FK_Window_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [identity].[User] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210722143207_ChangeRelations', N'5.0.8');
GO

COMMIT;
GO

