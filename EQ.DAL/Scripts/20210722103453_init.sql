IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF SCHEMA_ID(N'service') IS NULL EXEC(N'CREATE SCHEMA [service];');
GO

IF SCHEMA_ID(N'identity') IS NULL EXEC(N'CREATE SCHEMA [identity];');
GO

CREATE TABLE [identity].[Role] (
    [Id] uniqueidentifier NOT NULL,
    [RoleName] nvarchar(max) NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [service].[Service] (
    [Id] uniqueidentifier NOT NULL,
    [ServiceName] nvarchar(max) NULL,
    [Priority] int NOT NULL,
    CONSTRAINT [PK_Service] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [identity].[User] (
    [Id] uniqueidentifier NOT NULL,
    [RoleId] uniqueidentifier NOT NULL,
    [UserName] nvarchar(max) NULL,
    [NormalizedUserName] nvarchar(max) NULL,
    [Email] nvarchar(max) NULL,
    [NormalizedEmail] nvarchar(max) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_User_Role_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [identity].[Role] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [service].[Window] (
    [Id] uniqueidentifier NOT NULL,
    [WindowName] nvarchar(max) NULL,
    [IsOpen] bit NOT NULL,
    [UserId] uniqueidentifier NOT NULL,
    [ServiceId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Window] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Window_Service_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [service].[Service] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Window_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [identity].[User] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [service].[Request] (
    [Id] uniqueidentifier NOT NULL,
    [CreatedTime] datetime2 NOT NULL,
    [FinishedTime] datetime2 NULL,
    [ServiceStatus] int NOT NULL,
    [WindowId] uniqueidentifier NULL,
    CONSTRAINT [PK_Request] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Request_Window_WindowId] FOREIGN KEY ([WindowId]) REFERENCES [service].[Window] ([Id]) ON DELETE NO ACTION
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'RoleName') AND [object_id] = OBJECT_ID(N'[identity].[Role]'))
    SET IDENTITY_INSERT [identity].[Role] ON;
INSERT INTO [identity].[Role] ([Id], [RoleName])
VALUES ('f5dd7e21-9686-49db-8a9c-dd91a675b440', N'admin');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'RoleName') AND [object_id] = OBJECT_ID(N'[identity].[Role]'))
    SET IDENTITY_INSERT [identity].[Role] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'RoleId', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[identity].[User]'))
    SET IDENTITY_INSERT [identity].[User] ON;
INSERT INTO [identity].[User] ([Id], [AccessFailedCount], [ConcurrencyStamp], [Email], [EmailConfirmed], [LockoutEnabled], [LockoutEnd], [NormalizedEmail], [NormalizedUserName], [PasswordHash], [PhoneNumber], [PhoneNumberConfirmed], [RoleId], [SecurityStamp], [TwoFactorEnabled], [UserName])
VALUES ('75227a8b-b5c9-4686-b03d-0191e405fa4a', 0, N'e0fe494b-0bfa-41b9-82d6-d8ef4f556b0d', N'admin@eq.com', CAST(0 AS bit), CAST(0 AS bit), NULL, NULL, NULL, NULL, NULL, CAST(0 AS bit), 'f5dd7e21-9686-49db-8a9c-dd91a675b440', NULL, CAST(0 AS bit), NULL);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AccessFailedCount', N'ConcurrencyStamp', N'Email', N'EmailConfirmed', N'LockoutEnabled', N'LockoutEnd', N'NormalizedEmail', N'NormalizedUserName', N'PasswordHash', N'PhoneNumber', N'PhoneNumberConfirmed', N'RoleId', N'SecurityStamp', N'TwoFactorEnabled', N'UserName') AND [object_id] = OBJECT_ID(N'[identity].[User]'))
    SET IDENTITY_INSERT [identity].[User] OFF;
GO

CREATE INDEX [IX_Request_WindowId] ON [service].[Request] ([WindowId]);
GO

CREATE INDEX [IX_User_RoleId] ON [identity].[User] ([RoleId]);
GO

CREATE INDEX [IX_Window_ServiceId] ON [service].[Window] ([ServiceId]);
GO

CREATE INDEX [IX_Window_UserId] ON [service].[Window] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20210722103453_init', N'5.0.8');
GO

COMMIT;
GO

