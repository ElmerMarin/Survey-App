USE [master]
GO
/****** Object:  Database [SistemaEncuestas]    Script Date: 27/11/2020 09:56:23 ******/
CREATE DATABASE [SistemaEncuestas]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SistemaEncuestas', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SistemaEncuestas.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SistemaEncuestas_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\SistemaEncuestas_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [SistemaEncuestas] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SistemaEncuestas].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SistemaEncuestas] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET ARITHABORT OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SistemaEncuestas] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SistemaEncuestas] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SistemaEncuestas] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SistemaEncuestas] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SistemaEncuestas] SET  MULTI_USER 
GO
ALTER DATABASE [SistemaEncuestas] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SistemaEncuestas] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SistemaEncuestas] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SistemaEncuestas] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SistemaEncuestas] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SistemaEncuestas] SET QUERY_STORE = OFF
GO
USE [SistemaEncuestas]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [SistemaEncuestas]
GO
/****** Object:  Table [dbo].[Areas]    Script Date: 27/11/2020 09:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Areas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Estado] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Areas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 27/11/2020 09:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[IdArea] [int] NOT NULL,
 CONSTRAINT [PK_Categorias] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coordinadores]    Script Date: 27/11/2020 09:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coordinadores](
	[IdCoordinador] [int] IDENTITY(1,1) NOT NULL,
	[Dni] [char](8) NOT NULL,
	[Nombres] [varchar](50) NOT NULL,
	[ApellidoPaterno] [varchar](50) NOT NULL,
	[ApellidoMaterno] [varchar](50) NOT NULL,
	[Direccion] [varchar](100) NOT NULL,
	[Edad] [int] NOT NULL,
	[Telefono] [char](9) NOT NULL,
	[Sexo] [varchar](20) NOT NULL,
	[IdUsuario] [int] NOT NULL,
 CONSTRAINT [PK_Coordinadores] PRIMARY KEY CLUSTERED 
(
	[Dni] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleEncuesta]    Script Date: 27/11/2020 09:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleEncuesta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FechaInicio] [datetime] NOT NULL,
	[Fechafinal] [datetime] NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[IdEncuesta] [int] NOT NULL,
	[IdArea] [int] NOT NULL,
	[IdCategoria] [int] NOT NULL,
 CONSTRAINT [PK_DetalleEncuesta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DetalleResultado]    Script Date: 27/11/2020 09:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleResultado](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdEncuesta] [int] NOT NULL,
	[IdResultado] [int] NOT NULL,
	[Valor] [varchar](200) NOT NULL,
 CONSTRAINT [PK_Resultados] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Encuestados]    Script Date: 27/11/2020 09:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Encuestados](
	[IdEncuestado] [int] IDENTITY(1,1) NOT NULL,
	[Dni] [char](8) NOT NULL,
	[Nombres] [varchar](50) NOT NULL,
	[ApellidoPaterno] [varchar](50) NOT NULL,
	[ApellidoMaterno] [varchar](50) NOT NULL,
	[Direccion] [varchar](100) NOT NULL,
	[Edad] [int] NOT NULL,
	[Sexo] [varchar](20) NOT NULL,
	[Telefono] [char](9) NOT NULL,
	[IdUsuario] [int] NOT NULL,
 CONSTRAINT [PK_Encuestados] PRIMARY KEY CLUSTERED 
(
	[Dni] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Encuestas]    Script Date: 27/11/2020 09:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Encuestas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](100) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[Estado] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Encuestas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Preguntas]    Script Date: 27/11/2020 09:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Preguntas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[IdEncuesta] [int] NOT NULL,
 CONSTRAINT [PK_Preguntas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Respuestas]    Script Date: 27/11/2020 09:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Respuestas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[Estado] [varchar](20) NOT NULL,
	[IdPregunta] [int] NOT NULL,
 CONSTRAINT [PK_Respuestas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Resultados]    Script Date: 27/11/2020 09:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Resultados](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdUsuario] [int] NOT NULL,
	[Hora_Inicio] [varchar](50) NOT NULL,
	[Hora_Final] [varchar](50) NOT NULL,
	[Fecha] [date] NOT NULL,
 CONSTRAINT [PK_DetalleResultado] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 27/11/2020 09:56:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TipoUsuario] [varchar](50) NOT NULL,
	[Correo] [varchar](50) NOT NULL,
	[NombreUsuario] [varchar](50) NOT NULL,
	[Contraseña] [varchar](500) NOT NULL,
	[Token] [varchar](500) NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Areas] ON 

INSERT [dbo].[Areas] ([Id], [Nombre], [Fecha], [Estado]) VALUES (7, N'Electromesticos', CAST(N'2020-12-07T00:00:00.000' AS DateTime), N'Inactivo')
INSERT [dbo].[Areas] ([Id], [Nombre], [Fecha], [Estado]) VALUES (1002, N'Deportes', CAST(N'2020-11-26T00:00:00.000' AS DateTime), N'Activo')
SET IDENTITY_INSERT [dbo].[Areas] OFF
GO
SET IDENTITY_INSERT [dbo].[Categorias] ON 

INSERT [dbo].[Categorias] ([Id], [Nombre], [Fecha], [Estado], [IdArea]) VALUES (3, N'Computadoras', CAST(N'2020-11-18T12:00:00.000' AS DateTime), N'Activo', 7)
INSERT [dbo].[Categorias] ([Id], [Nombre], [Fecha], [Estado], [IdArea]) VALUES (4, N'Futbol', CAST(N'2020-11-26T00:00:00.000' AS DateTime), N'Activo', 1002)
SET IDENTITY_INSERT [dbo].[Categorias] OFF
GO
SET IDENTITY_INSERT [dbo].[Coordinadores] ON 

INSERT [dbo].[Coordinadores] ([IdCoordinador], [Dni], [Nombres], [ApellidoPaterno], [ApellidoMaterno], [Direccion], [Edad], [Telefono], [Sexo], [IdUsuario]) VALUES (3, N'80673456', N'Raul', N'Fernandez', N'Lopez', N'Los cedros 678', 30, N'983412678', N'Masculino', 4)
SET IDENTITY_INSERT [dbo].[Coordinadores] OFF
GO
SET IDENTITY_INSERT [dbo].[DetalleEncuesta] ON 

INSERT [dbo].[DetalleEncuesta] ([Id], [FechaInicio], [Fechafinal], [Estado], [IdEncuesta], [IdArea], [IdCategoria]) VALUES (2, CAST(N'2020-11-26T12:00:00.000' AS DateTime), CAST(N'2020-11-27T12:00:00.000' AS DateTime), N'Inactivo', 11, 1002, 4)
INSERT [dbo].[DetalleEncuesta] ([Id], [FechaInicio], [Fechafinal], [Estado], [IdEncuesta], [IdArea], [IdCategoria]) VALUES (3, CAST(N'2020-11-08T12:00:00.000' AS DateTime), CAST(N'2020-11-27T12:00:00.000' AS DateTime), N'Inactivo', 12, 7, 3)
SET IDENTITY_INSERT [dbo].[DetalleEncuesta] OFF
GO
SET IDENTITY_INSERT [dbo].[DetalleResultado] ON 

INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2034, 11, 2012, N'Kateringur')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2035, 11, 2012, N'Julio Cesar')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2036, 11, 2012, N'Edison Cavani')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2037, 11, 2012, N'Klyan Mappe')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2038, 11, 2013, N'sarah')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2039, 11, 2013, N'Pedro Gallese')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2040, 11, 2013, N'Edison Cavani')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2041, 11, 2013, N'Klyan Mappe')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2042, 1014, 2014, N'Pop')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2043, 1014, 2014, N'Alejandro Sanz')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2044, 1014, 2015, N'Pop')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2045, 1014, 2015, N'Laura Pausini')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2051, 11, 2018, N'sarah')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2052, 11, 2018, N'Pedro Gallese')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2053, 11, 2018, N'Edison Cavani')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2054, 11, 2018, N'Klyan Mappe')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2055, 1014, 2019, N'Pop')
INSERT [dbo].[DetalleResultado] ([Id], [IdEncuesta], [IdResultado], [Valor]) VALUES (2056, 1014, 2019, N'Alejandro Sanz')
SET IDENTITY_INSERT [dbo].[DetalleResultado] OFF
GO
SET IDENTITY_INSERT [dbo].[Encuestados] ON 

INSERT [dbo].[Encuestados] ([IdEncuestado], [Dni], [Nombres], [ApellidoPaterno], [ApellidoMaterno], [Direccion], [Edad], [Sexo], [Telefono], [IdUsuario]) VALUES (4, N'23456733', N'Meliza', N'Paredes', N'Gonzales', N'los cedros 567', 23, N'Femenino', N'341267   ', 1005)
INSERT [dbo].[Encuestados] ([IdEncuestado], [Dni], [Nombres], [ApellidoPaterno], [ApellidoMaterno], [Direccion], [Edad], [Sexo], [Telefono], [IdUsuario]) VALUES (6, N'23456789', N'Julian', N'Rodriguez', N'Barco', N'los cedros 456', 34, N'Masculino', N'451234   ', 1007)
INSERT [dbo].[Encuestados] ([IdEncuestado], [Dni], [Nombres], [ApellidoPaterno], [ApellidoMaterno], [Direccion], [Edad], [Sexo], [Telefono], [IdUsuario]) VALUES (7, N'34126789', N'Angi', N'Jimenez', N'Rodriguez', N'los cedros 567', 34, N'Femenino', N'903412367', 1008)
INSERT [dbo].[Encuestados] ([IdEncuestado], [Dni], [Nombres], [ApellidoPaterno], [ApellidoMaterno], [Direccion], [Edad], [Sexo], [Telefono], [IdUsuario]) VALUES (5, N'45123490', N'Pedro', N'Diaz', N'Polar', N'los cedros', 23, N'Masculino', N'451234   ', 1006)
INSERT [dbo].[Encuestados] ([IdEncuestado], [Dni], [Nombres], [ApellidoPaterno], [ApellidoMaterno], [Direccion], [Edad], [Sexo], [Telefono], [IdUsuario]) VALUES (8, N'45127890', N'Felipe', N'Jimenez', N'Rodriguez', N'los cedros 567', 34, N'Masculino', N'891234567', 1009)
SET IDENTITY_INSERT [dbo].[Encuestados] OFF
GO
SET IDENTITY_INSERT [dbo].[Encuestas] ON 

INSERT [dbo].[Encuestas] ([Id], [Titulo], [Descripcion], [Estado]) VALUES (11, N'Copa Mundial Rusia 2018', N'Participaron las 32 mejores selecciones del mundo ', N'Abierta')
INSERT [dbo].[Encuestas] ([Id], [Titulo], [Descripcion], [Estado]) VALUES (12, N'Computadoras HP', N'son las mejores laptops', N'Abierta')
INSERT [dbo].[Encuestas] ([Id], [Titulo], [Descripcion], [Estado]) VALUES (1014, N'Musica  en el siglo 21', N'la mejor música y los mejores cantantes ', N'Abierta')
SET IDENTITY_INSERT [dbo].[Encuestas] OFF
GO
SET IDENTITY_INSERT [dbo].[Preguntas] ON 

INSERT [dbo].[Preguntas] ([Id], [Descripcion], [Estado], [IdEncuesta]) VALUES (15, N'Los Mejores estadios', N'Activo', 11)
INSERT [dbo].[Preguntas] ([Id], [Descripcion], [Estado], [IdEncuesta]) VALUES (16, N'Que tipo de pantalla usas mas', N'Activo', 12)
INSERT [dbo].[Preguntas] ([Id], [Descripcion], [Estado], [IdEncuesta]) VALUES (17, N'Las mejores marcas', N'Activo', 12)
INSERT [dbo].[Preguntas] ([Id], [Descripcion], [Estado], [IdEncuesta]) VALUES (19, N'La mejor Arquero del mundo', N'Activo', 11)
INSERT [dbo].[Preguntas] ([Id], [Descripcion], [Estado], [IdEncuesta]) VALUES (20, N'El mejor Delantero', N'Activo', 11)
INSERT [dbo].[Preguntas] ([Id], [Descripcion], [Estado], [IdEncuesta]) VALUES (1018, N'El jugador mas veloz', N'Activo', 11)
INSERT [dbo].[Preguntas] ([Id], [Descripcion], [Estado], [IdEncuesta]) VALUES (1019, N'Tipo de Procesador', N'Activo', 12)
INSERT [dbo].[Preguntas] ([Id], [Descripcion], [Estado], [IdEncuesta]) VALUES (1020, N'El mejor genero ', N'Activo', 1014)
INSERT [dbo].[Preguntas] ([Id], [Descripcion], [Estado], [IdEncuesta]) VALUES (1021, N'El mejor Cantante', N'Activo', 1014)
SET IDENTITY_INSERT [dbo].[Preguntas] OFF
GO
SET IDENTITY_INSERT [dbo].[Respuestas] ON 

INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (12, N'sarah', N'Activo', 15)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (13, N'HP', N'activo', 17)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (14, N'Lenovo', N'activo', 17)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (15, N'Led', N'activo', 16)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (16, N'Plasma', N'activo', 16)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (1020, N'Kateringur', N'Activo', 15)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2017, N'Pedro Gallese', N'Activo', 19)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2018, N'Julio Cesar', N'Activo', 19)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2019, N'Fernando Muslera', N'Activo', 19)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2020, N'Manuel Mhoyer', N'Inactivo', 19)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2021, N'Paolo Guerrero', N'Activo', 20)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2022, N'Edison Cavani', N'Activo', 20)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2023, N'Arturo Vidal', N'Activo', 20)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2024, N'Leonel Messi', N'Activo', 20)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2025, N'Luis Advincula', N'Activo', 1018)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2026, N'Klyan Mappe', N'Inactivo', 1018)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2027, N'AMD', N'Activo', 1019)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2028, N'Intel', N'Activo', 1019)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2029, N'Salsa', N'Activo', 1020)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2030, N'Rock', N'Activo', 1020)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2031, N'Pop', N'Activo', 1020)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2032, N'Laura Pausini', N'Activo', 1021)
INSERT [dbo].[Respuestas] ([Id], [Descripcion], [Estado], [IdPregunta]) VALUES (2033, N'Alejandro Sanz', N'Activo', 1021)
SET IDENTITY_INSERT [dbo].[Respuestas] OFF
GO
SET IDENTITY_INSERT [dbo].[Resultados] ON 

INSERT [dbo].[Resultados] ([Id], [IdUsuario], [Hora_Inicio], [Hora_Final], [Fecha]) VALUES (2012, 4, N'26/11/2020', N'11:50:09 PM', CAST(N'2020-11-26' AS Date))
INSERT [dbo].[Resultados] ([Id], [IdUsuario], [Hora_Inicio], [Hora_Final], [Fecha]) VALUES (2013, 4, N'27/11/2020', N'11:55:45 AM', CAST(N'2020-11-27' AS Date))
INSERT [dbo].[Resultados] ([Id], [IdUsuario], [Hora_Inicio], [Hora_Final], [Fecha]) VALUES (2014, 4, N'27/11/2020', N'8:53:21 PM', CAST(N'2020-11-27' AS Date))
INSERT [dbo].[Resultados] ([Id], [IdUsuario], [Hora_Inicio], [Hora_Final], [Fecha]) VALUES (2015, 4, N'27/11/2020', N'8:53:42 PM', CAST(N'2020-11-27' AS Date))
INSERT [dbo].[Resultados] ([Id], [IdUsuario], [Hora_Inicio], [Hora_Final], [Fecha]) VALUES (2018, 1006, N'27/11/2020', N'9:02:03 PM', CAST(N'2020-11-27' AS Date))
INSERT [dbo].[Resultados] ([Id], [IdUsuario], [Hora_Inicio], [Hora_Final], [Fecha]) VALUES (2019, 1006, N'27/11/2020', N'9:02:12 PM', CAST(N'2020-11-27' AS Date))
SET IDENTITY_INSERT [dbo].[Resultados] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Id], [TipoUsuario], [Correo], [NombreUsuario], [Contraseña], [Token]) VALUES (4, N'Coordinador', N'admin@gmail.com', N'admin', N'123456', N'fggg')
INSERT [dbo].[Usuarios] ([Id], [TipoUsuario], [Correo], [NombreUsuario], [Contraseña], [Token]) VALUES (1005, N'Encuestado', N'meli@gmail.com', N'meli', N'6DzdkV3WotOY9IWRuxRNq2q3WyqWDN9p/OqGDGV3aIxORIi6S50yKhZLTYT/iLkndV8rbxxCbDeYAVI8fPXGTDbylQI=', N'')
INSERT [dbo].[Usuarios] ([Id], [TipoUsuario], [Correo], [NombreUsuario], [Contraseña], [Token]) VALUES (1006, N'Encuestado', N'pepe@gmail.com', N'pepe', N'lo3gMq851iCEKLcRJoTOCGwURrm1YzC21cQTSEYQa6bYE2Djbb4WaCpubRRrVEqSlZVcLONMIz21rd/quX+UAHbhBpgf0dvxMux3FnTz', N'')
INSERT [dbo].[Usuarios] ([Id], [TipoUsuario], [Correo], [NombreUsuario], [Contraseña], [Token]) VALUES (1007, N'Encuestado', N'julian@gmail.com', N'julian', N'uuMScmA8s1UqTTOOkaSxcbDLjzkWkubHBXNUD0D46Niw+ZKWTXz8zD0AxohRUrVo+nwg0kDzutD/sVOOG9CUuYDmfm57zOc8eg==', N'')
INSERT [dbo].[Usuarios] ([Id], [TipoUsuario], [Correo], [NombreUsuario], [Contraseña], [Token]) VALUES (1008, N'Encuestado', N'angi@gmail.com', N'angi', N'dHpDbFcEHpi92Rlsy8E7mj86zhQvTG6ZM0MVAB5clTHWK8hqs9ResqoOLfRQDw0MMWcweat2sb6vP71As+sFNlV7t3YkFzIjYq01XXHvCw==', N'')
INSERT [dbo].[Usuarios] ([Id], [TipoUsuario], [Correo], [NombreUsuario], [Contraseña], [Token]) VALUES (1009, N'Encuestado', N'mardc0602@gmail.com', N'felipe', N'A3Vk9tY/V4e8f9BKeuxSsXoEGjbuLxtuuo+i0+D4tUv6Jy2AEeVr5c7B12KLRzG6z9c/qv0/dLWsABGJnXS29rKAsx+7cvRKwii0lbk=', N'Kvhg37JIQgPMGyWMbApKJsIXaPjWAZsAvsiGF11TvqVpZety8iuh86ULwMlDpnbUz2Vv48zPsZqWL0FthfE38pYxBos%3d')
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
ALTER TABLE [dbo].[Categorias]  WITH CHECK ADD  CONSTRAINT [FK_Categorias_Areas] FOREIGN KEY([IdArea])
REFERENCES [dbo].[Areas] ([Id])
GO
ALTER TABLE [dbo].[Categorias] CHECK CONSTRAINT [FK_Categorias_Areas]
GO
ALTER TABLE [dbo].[Coordinadores]  WITH CHECK ADD  CONSTRAINT [FK_Coordinadores_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Coordinadores] CHECK CONSTRAINT [FK_Coordinadores_Usuarios]
GO
ALTER TABLE [dbo].[DetalleEncuesta]  WITH CHECK ADD  CONSTRAINT [FK_DetalleEncuesta_Areas] FOREIGN KEY([IdArea])
REFERENCES [dbo].[Areas] ([Id])
GO
ALTER TABLE [dbo].[DetalleEncuesta] CHECK CONSTRAINT [FK_DetalleEncuesta_Areas]
GO
ALTER TABLE [dbo].[DetalleEncuesta]  WITH CHECK ADD  CONSTRAINT [FK_DetalleEncuesta_Categorias] FOREIGN KEY([IdCategoria])
REFERENCES [dbo].[Categorias] ([Id])
GO
ALTER TABLE [dbo].[DetalleEncuesta] CHECK CONSTRAINT [FK_DetalleEncuesta_Categorias]
GO
ALTER TABLE [dbo].[DetalleEncuesta]  WITH CHECK ADD  CONSTRAINT [FK_DetalleEncuesta_Encuestas] FOREIGN KEY([IdEncuesta])
REFERENCES [dbo].[Encuestas] ([Id])
GO
ALTER TABLE [dbo].[DetalleEncuesta] CHECK CONSTRAINT [FK_DetalleEncuesta_Encuestas]
GO
ALTER TABLE [dbo].[DetalleResultado]  WITH CHECK ADD  CONSTRAINT [FK_DetalleResultado_Encuestas] FOREIGN KEY([IdEncuesta])
REFERENCES [dbo].[Encuestas] ([Id])
GO
ALTER TABLE [dbo].[DetalleResultado] CHECK CONSTRAINT [FK_DetalleResultado_Encuestas]
GO
ALTER TABLE [dbo].[DetalleResultado]  WITH CHECK ADD  CONSTRAINT [FK_Resultados_DetalleResultado] FOREIGN KEY([IdResultado])
REFERENCES [dbo].[Resultados] ([Id])
GO
ALTER TABLE [dbo].[DetalleResultado] CHECK CONSTRAINT [FK_Resultados_DetalleResultado]
GO
ALTER TABLE [dbo].[Encuestados]  WITH CHECK ADD  CONSTRAINT [FK_Encuestados_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Encuestados] CHECK CONSTRAINT [FK_Encuestados_Usuarios]
GO
ALTER TABLE [dbo].[Preguntas]  WITH CHECK ADD  CONSTRAINT [FK_Preguntas_Encuestas] FOREIGN KEY([IdEncuesta])
REFERENCES [dbo].[Encuestas] ([Id])
GO
ALTER TABLE [dbo].[Preguntas] CHECK CONSTRAINT [FK_Preguntas_Encuestas]
GO
ALTER TABLE [dbo].[Respuestas]  WITH CHECK ADD  CONSTRAINT [FK_Respuestas_Preguntas] FOREIGN KEY([IdPregunta])
REFERENCES [dbo].[Preguntas] ([Id])
GO
ALTER TABLE [dbo].[Respuestas] CHECK CONSTRAINT [FK_Respuestas_Preguntas]
GO
ALTER TABLE [dbo].[Resultados]  WITH CHECK ADD  CONSTRAINT [FK_Resultados_Usuarios] FOREIGN KEY([IdUsuario])
REFERENCES [dbo].[Usuarios] ([Id])
GO
ALTER TABLE [dbo].[Resultados] CHECK CONSTRAINT [FK_Resultados_Usuarios]
GO
USE [master]
GO
ALTER DATABASE [SistemaEncuestas] SET  READ_WRITE 
GO
