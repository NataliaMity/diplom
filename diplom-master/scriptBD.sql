USE [master]
GO
/****** Object:  Database [MityaginaNP]    Script Date: 07.02.2023 15:54:13 ******/
CREATE DATABASE [MityaginaNP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MityaginaNP', FILENAME = N'D:\DATA\MSSQL14.SQLEXPRESS\MSSQL\DATA\MityaginaNP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'MityaginaNP_log', FILENAME = N'D:\DATA\MSSQL14.SQLEXPRESS\MSSQL\DATA\MityaginaNP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [MityaginaNP] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MityaginaNP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MityaginaNP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MityaginaNP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MityaginaNP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MityaginaNP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MityaginaNP] SET ARITHABORT OFF 
GO
ALTER DATABASE [MityaginaNP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MityaginaNP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MityaginaNP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MityaginaNP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MityaginaNP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MityaginaNP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MityaginaNP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MityaginaNP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MityaginaNP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MityaginaNP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MityaginaNP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MityaginaNP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MityaginaNP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MityaginaNP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MityaginaNP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MityaginaNP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MityaginaNP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MityaginaNP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MityaginaNP] SET  MULTI_USER 
GO
ALTER DATABASE [MityaginaNP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MityaginaNP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MityaginaNP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MityaginaNP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [MityaginaNP] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [MityaginaNP] SET QUERY_STORE = OFF
GO
USE [MityaginaNP]
GO
/****** Object:  User [LAB105]    Script Date: 07.02.2023 15:54:13 ******/
CREATE USER [LAB105] FOR LOGIN [LAB105] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  Table [dbo].[Client]    Script Date: 07.02.2023 15:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Client](
	[ClientID] [int] IDENTITY(1,1) NOT NULL,
	[ClientName] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED 
(
	[ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 07.02.2023 15:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentID] [int] IDENTITY(1,1) NOT NULL,
	[DepartmentName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Document]    Script Date: 07.02.2023 15:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Document](
	[DocumentID] [int] IDENTITY(1,1) NOT NULL,
	[DocumentSource] [nvarchar](max) NOT NULL,
	[ProjectID] [int] NOT NULL,
 CONSTRAINT [PK_Document] PRIMARY KEY CLUSTERED 
(
	[DocumentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Project]    Script Date: 07.02.2023 15:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Project](
	[ProjectID] [int] IDENTITY(2,1) NOT NULL,
	[ProjectName] [varchar](200) NOT NULL,
	[ProjectDescription] [nvarchar](max) NOT NULL,
	[ClientID] [int] NOT NULL,
	[ProjectStartDate] [date] NOT NULL,
	[ProjectEndDate] [date] NULL,
	[UserLogin] [nchar](6) NOT NULL,
 CONSTRAINT [PK_Project] PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 07.02.2023 15:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](40) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Task]    Script Date: 07.02.2023 15:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Task](
	[TaskID] [int] IDENTITY(1,1) NOT NULL,
	[TaskText] [nvarchar](max) NOT NULL,
	[DepartmentID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[UserLogin] [nchar](6) NOT NULL,
	[TaskDeadLine] [date] NOT NULL,
	[TaskStart] [date] NOT NULL,
 CONSTRAINT [PK_Task] PRIMARY KEY CLUSTERED 
(
	[TaskID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 07.02.2023 15:54:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Login] [nchar](6) NOT NULL,
	[Password] [nchar](8) NOT NULL,
	[RoleID] [int] NOT NULL,
	[DepartmentID] [int] NULL,
	[LastName] [nvarchar](30) NOT NULL,
	[Patronymic] [nvarchar](30) NOT NULL,
	[FirstName] [nvarchar](30) NOT NULL,
	[WorkExp] [int] NOT NULL,
	[BirthDate] [date] NOT NULL,
	[Passport] [nchar](10) NOT NULL,
	[Education] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Client] ON 

INSERT [dbo].[Client] ([ClientID], [ClientName]) VALUES (1, N'Звягинцев О.Ф.')
INSERT [dbo].[Client] ([ClientID], [ClientName]) VALUES (2, N'ООО "Застройщик"')
INSERT [dbo].[Client] ([ClientID], [ClientName]) VALUES (3, N'АО "Строитель"')
SET IDENTITY_INSERT [dbo].[Client] OFF
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([DepartmentID], [DepartmentName]) VALUES (1, N'Архитектурное проектированиe')
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName]) VALUES (2, N'Строительное 
проектирование')
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName]) VALUES (3, N'Сметно-экономические расчеты')
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName]) VALUES (4, N'Сантехнические работы')
INSERT [dbo].[Department] ([DepartmentID], [DepartmentName]) VALUES (5, N'Электротехнические работы')
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Document] ON 

INSERT [dbo].[Document] ([DocumentID], [DocumentSource], [ProjectID]) VALUES (1, N'Doc/School', 1)
SET IDENTITY_INSERT [dbo].[Document] OFF
GO
SET IDENTITY_INSERT [dbo].[Project] ON 

INSERT [dbo].[Project] ([ProjectID], [ProjectName], [ProjectDescription], [ClientID], [ProjectStartDate], [ProjectEndDate], [UserLogin]) VALUES (1, N'Школа №1', N'Разработка документации для строительства школы в городе Н.', 1, CAST(N'2019-04-02' AS Date), CAST(N'2024-04-02' AS Date), N'GIPGIP')
INSERT [dbo].[Project] ([ProjectID], [ProjectName], [ProjectDescription], [ClientID], [ProjectStartDate], [ProjectEndDate], [UserLogin]) VALUES (2, N'ЖК "Олимп"', N'Строительство ЖК в городе Краснодаре.', 1, CAST(N'2020-02-08' AS Date), CAST(N'2025-01-09' AS Date), N'GIPGIP')
INSERT [dbo].[Project] ([ProjectID], [ProjectName], [ProjectDescription], [ClientID], [ProjectStartDate], [ProjectEndDate], [UserLogin]) VALUES (3, N'Детский сад №98', N'Проектирование детского сада для города Анапа.', 2, CAST(N'2021-09-08' AS Date), CAST(N'2026-09-09' AS Date), N'GIPGIP')
SET IDENTITY_INSERT [dbo].[Project] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (1, N'ГИП')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (2, N'Начальник')
INSERT [dbo].[Role] ([RoleID], [RoleName]) VALUES (3, N'Сотрудник')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[Task] ON 

INSERT [dbo].[Task] ([TaskID], [TaskText], [DepartmentID], [ProjectID], [UserLogin], [TaskDeadLine], [TaskStart]) VALUES (2, N'Разработать макет школы', 1, 1, N'User  ', CAST(N'2024-02-02' AS Date), CAST(N'2023-02-02' AS Date))
SET IDENTITY_INSERT [dbo].[Task] OFF
GO
INSERT [dbo].[User] ([Login], [Password], [RoleID], [DepartmentID], [LastName], [Patronymic], [FirstName], [WorkExp], [BirthDate], [Passport], [Education]) VALUES (N'Chef  ', N'Chef    ', 2, 1, N'Голубев', N'Ерофеевич', N'Олег', 20, CAST(N'1978-10-02' AS Date), N'0123294123', N'Нет')
INSERT [dbo].[User] ([Login], [Password], [RoleID], [DepartmentID], [LastName], [Patronymic], [FirstName], [WorkExp], [BirthDate], [Passport], [Education]) VALUES (N'GIPGIP', N'GIPGIP  ', 1, NULL, N'Ирсанов', N'Николаевич', N'Дмитрий', 10, CAST(N'1988-03-02' AS Date), N'0136700285', N'Нет')
INSERT [dbo].[User] ([Login], [Password], [RoleID], [DepartmentID], [LastName], [Patronymic], [FirstName], [WorkExp], [BirthDate], [Passport], [Education]) VALUES (N'User  ', N'User    ', 3, 1, N'Исаков', N'Петрович', N'Андрей', 1, CAST(N'1991-02-09' AS Date), N'1223839203', N'Нет')
GO
ALTER TABLE [dbo].[Document]  WITH CHECK ADD  CONSTRAINT [FK_Document_Project] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[Document] CHECK CONSTRAINT [FK_Document_Project]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_Client] FOREIGN KEY([ClientID])
REFERENCES [dbo].[Client] ([ClientID])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_Client]
GO
ALTER TABLE [dbo].[Project]  WITH CHECK ADD  CONSTRAINT [FK_Project_User] FOREIGN KEY([UserLogin])
REFERENCES [dbo].[User] ([Login])
GO
ALTER TABLE [dbo].[Project] CHECK CONSTRAINT [FK_Project_User]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([DepartmentID])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Department]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_Project] FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_Project]
GO
ALTER TABLE [dbo].[Task]  WITH CHECK ADD  CONSTRAINT [FK_Task_User] FOREIGN KEY([UserLogin])
REFERENCES [dbo].[User] ([Login])
GO
ALTER TABLE [dbo].[Task] CHECK CONSTRAINT [FK_Task_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Department] FOREIGN KEY([DepartmentID])
REFERENCES [dbo].[Department] ([DepartmentID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Department]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([RoleID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [MityaginaNP] SET  READ_WRITE 
GO
