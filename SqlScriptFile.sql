USE [master]
GO
/****** Object:  Database [SnehaDB]    Script Date: 6/20/2021 8:38:41 PM ******/
CREATE DATABASE [SnehaDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SnehaDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SnehaDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SnehaDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\SnehaDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [SnehaDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SnehaDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SnehaDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SnehaDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SnehaDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SnehaDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SnehaDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [SnehaDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [SnehaDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SnehaDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SnehaDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SnehaDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SnehaDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SnehaDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SnehaDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SnehaDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SnehaDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [SnehaDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SnehaDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SnehaDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SnehaDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SnehaDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SnehaDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SnehaDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SnehaDB] SET RECOVERY FULL 
GO
ALTER DATABASE [SnehaDB] SET  MULTI_USER 
GO
ALTER DATABASE [SnehaDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SnehaDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SnehaDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SnehaDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SnehaDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SnehaDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'SnehaDB', N'ON'
GO
ALTER DATABASE [SnehaDB] SET QUERY_STORE = OFF
GO
USE [SnehaDB]
GO
/****** Object:  UserDefinedFunction [dbo].[GetTermName]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[GetTermName]
(
	@TestDate DateTime
)  
RETURNS VARCHAR(50)  
 AS  
BEGIN  
    Declare @DatePartString varchar(20)
	Set @DatePartString = DATENAME(month,  @TestDate)	
	Return(Select TermName from TermList where TermPeriod like @DatePartString + '%' OR TermPeriod like '%' + @DatePartString OR TermPeriod like '%' + @DatePartString + '%')
END
GO
/****** Object:  Table [dbo].[Grade]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Grade](
	[GradeID] [int] IDENTITY(1,1) NOT NULL,
	[GradeName] [nvarchar](50) NOT NULL,
	[GradeYear] [nvarchar](50) NULL,
	[SchoolID] [int] NOT NULL,
 CONSTRAINT [PK_Grade] PRIMARY KEY CLUSTERED 
(
	[GradeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[School]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[School](
	[SchoolID] [int] IDENTITY(1,1) NOT NULL,
	[SchoolName] [nvarchar](50) NOT NULL,
	[Duration] [nvarchar](50) NULL,
 CONSTRAINT [PK_School] PRIMARY KEY CLUSTERED 
(
	[SchoolID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Score]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Score](
	[ScoreID] [int] IDENTITY(1,1) NOT NULL,
	[SubjectID] [int] NOT NULL,
	[Score] [decimal](18, 0) NOT NULL,
	[TestDate] [datetime] NOT NULL,
	[GradeID] [int] NULL,
 CONSTRAINT [PK_Score] PRIMARY KEY CLUSTERED 
(
	[ScoreID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subject]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[SubjectID] [int] NOT NULL,
	[GradeID] [int] NOT NULL,
 CONSTRAINT [PK_Subject] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SubjectList]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SubjectList](
	[SubjectID] [int] NOT NULL,
	[SubjectName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_SubjectList] PRIMARY KEY CLUSTERED 
(
	[SubjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TermList]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TermList](
	[TermID] [int] IDENTITY(1,1) NOT NULL,
	[TermName] [nvarchar](50) NOT NULL,
	[TermPeriod] [nvarchar](50) NULL,
 CONSTRAINT [PK_Term] PRIMARY KEY CLUSTERED 
(
	[TermID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Grade]  WITH CHECK ADD  CONSTRAINT [FK_Grade_School] FOREIGN KEY([SchoolID])
REFERENCES [dbo].[School] ([SchoolID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Grade] CHECK CONSTRAINT [FK_Grade_School]
GO
ALTER TABLE [dbo].[Score]  WITH CHECK ADD  CONSTRAINT [FK_Score_Grade] FOREIGN KEY([GradeID])
REFERENCES [dbo].[Grade] ([GradeID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Score] CHECK CONSTRAINT [FK_Score_Grade]
GO
ALTER TABLE [dbo].[Score]  WITH CHECK ADD  CONSTRAINT [FK_Score_SubjectList] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[SubjectList] ([SubjectID])
GO
ALTER TABLE [dbo].[Score] CHECK CONSTRAINT [FK_Score_SubjectList]
GO
ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_Subject_Grade] FOREIGN KEY([GradeID])
REFERENCES [dbo].[Grade] ([GradeID])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_Subject_Grade]
GO
ALTER TABLE [dbo].[Subject]  WITH CHECK ADD  CONSTRAINT [FK_Subject_SubjectList] FOREIGN KEY([SubjectID])
REFERENCES [dbo].[SubjectList] ([SubjectID])
GO
ALTER TABLE [dbo].[Subject] CHECK CONSTRAINT [FK_Subject_SubjectList]
GO
/****** Object:  StoredProcedure [dbo].[Prc_AddGrade]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[Prc_AddGrade]
(
  @GradeName nvarchar(50),
  @GradeYear nvarchar(50),
  @SchoolID int
)
AS
Begin
Set NOCOUNT ON
Insert InTo Grade(GradeName,GradeYear,SchoolID) values (@GradeName,@GradeYear,@SchoolID)
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_AddSchool]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[Prc_AddSchool]
(
	@SchoolName nvarchar(50),
	@Duration nvarchar(50)
)
AS
Begin
   SET NOCOUNT ON
   Insert into School Values (@SchoolName,@Duration)	
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_AddScore]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE Procedure [dbo].[Prc_AddScore]
(
  @GradeID int,
  @SubjectID int,
  @Score decimal(18,0),
  @TestDate DateTime,
  @ScoreID int output,
  @TermName varchar(20) output
)
As
Begin
SET NOCOUNT ON
Insert into Score Values(@SubjectID,@Score,@TestDate,@GradeID) 
Select @ScoreID = SCOPE_IDENTITY(), @TermName = dbo.GetTermName(@TestDate)
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_AddSubject]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Prc_AddSubject]
(
	@SubjectIDList nvarchar(max),
	@GradeID int
)
As
Begin
	SET NOCOUNT ON
	Declare @Exist Bit
	DECLARE @MyCursor CURSOR;
	Declare @SubjectID int
	BEGIN
		SET @MyCursor = CURSOR FOR
		select TRY_CAST(value as int) from  string_split(@SubjectIDList,',')     

		OPEN @MyCursor 
		FETCH NEXT FROM @MyCursor 
		INTO @SubjectID

		WHILE @@FETCH_STATUS = 0
		BEGIN
		  Set @Exist = 0 
		  Select @Exist= 1 from Subject where SubjectID =  @SubjectID and GradeID = @GradeID
		  If(@Exist=0)
		  Begin
			Insert into Subject Values(@SubjectID,@GradeID)
		  End
		  FETCH NEXT FROM @MyCursor 
		  INTO @SubjectID 
		END

		CLOSE @MyCursor ;
		DEALLOCATE @MyCursor;
	END
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_Chart]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Prc_Chart]
(
	@GradeID int
)
As
Begin
	Set NoCount On
	Select SL.SubjectName,Concat(DateName(month,TestDate),'-',Year(TestDate))  as TermDate,Avg(Score) as Score from dbo.Score
	inner join SubjectList SL on Score.SubjectID = SL.SubjectID
	inner join Grade G on Score.GradeID = G.GradeID where G.GradeID = @GradeID Group by Concat(DateName(month,TestDate),'-',Year(TestDate)),SL.SubjectName
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_DeleteGrade]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[Prc_DeleteGrade]
(
   @GradeID int
)
As
Begin
Set NoCount On
Delete From Grade Where GradeID = @GradeID
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_DeleteSchool]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[Prc_DeleteSchool]
(
   @SchoolID int
)
As
Begin
Set NoCount ON
Delete From School Where SchoolID = @SchoolID
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_DeleteScore]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[Prc_DeleteScore]
(
   @ScoreID int
)
As
Begin
Set NoCount On
Delete From Score Where ScoreID = @ScoreID
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_DeleteSubjectByGrade]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Prc_DeleteSubjectByGrade]
(
	@SubjectIDList nvarchar(max),
	@GradeID int
)
As
Begin
   SET NOCOUNT ON
   Delete From Subject where SubjectID In ( select Try_Cast(value as int) from string_split(@SubjectIDList,',')) and GradeID=@GradeID
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_EditGrade]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Prc_EditGrade]
(
	@GradeID int,
	@GradeName nvarchar(50),
	@GradeYear nvarchar(50),
	@SchoolID int
)
as
Begin
   SET NOCOUNT ON
   Update Grade Set GradeName=@GradeName, GradeYear=@GradeYear,SchoolID=@SchoolID where GradeID = @GradeID
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_EditSchool]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[Prc_EditSchool]
(
	@SchoolID int,
	@SchoolName nvarchar(50),
	@Duration nvarchar(50)
)
As
Begin
  SET NOCOUNT ON
  Update School Set SchoolName=@SchoolName, Duration=@Duration where SchoolID=@SchoolID
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_EditScore]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create Procedure [dbo].[Prc_EditScore]
(
  @ScoreID int,
  @Score decimal(18,0),
  @TestDate DateTime
)
As
Begin
SET NOCOUNT ON
Update Score Set Score=@Score, TestDate=@TestDate where ScoreID = @ScoreID 
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_GetBarChart]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Prc_GetBarChart]
(
	@GradeID int
)
As
Begin
	Set NoCount On
	Select SL.SubjectName, CAST(AVG(Score.Score) AS DECIMAL(10,2)) as Score, dbo.GetTermName(TestDate) As TermName from Score
	inner join SubjectList SL on Score.SubjectID = SL.SubjectID
	inner join Grade G on Score.GradeID = G.GradeID
	where G.GradeID =@GradeID
	group by SL.SubjectName,dbo.GetTermName(TestDate)
End


GO
/****** Object:  StoredProcedure [dbo].[Prc_GetGrades]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[Prc_GetGrades]
As
Begin
  Set NOCOUNT ON
  Select Gradename,Gradeyear, GradeID 
  From Grade 
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_GetGradesBySchool]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create procedure [dbo].[Prc_GetGradesBySchool]
As
Begin
  Set NOCOUNT ON
  Select SchoolName,Gradename,Gradeyear,Grade.GradeID,School.SchoolID,School.Duration 
  From Grade Right Outer join School 
  On Grade.SchoolID = School.SchoolID
  order by Convert(int,SUBSTRING(Duration,6,4))
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_GetOverAllTermChart]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Prc_GetOverAllTermChart]
(
	@GradeID int
)
As
Begin
	Set NoCount On
	Select CAST(AVG(Score.Score) AS DECIMAL(10,2)) as TermScore,dbo.GetTermName(TestDate) As TermName from Score
	Inner join Grade on Score.GradeID = Grade.GradeID
	Where Grade.GradeID = @GradeID
	Group By dbo.GetTermName(TestDate)
End


GO
/****** Object:  StoredProcedure [dbo].[Prc_GetSchools]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Prc_GetSchools]
as
Begin
    Set NOCOUNT ON
	Select * from School
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_GetScores]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
 CREATE Procedure [dbo].[Prc_GetScores]
  As
  Begin
  set NoCount On
  Select SL.SubjectName, G.GradeName, Score.Score, Score.ScoreID,Score.TestDate, G.GradeID,SL.SubjectID, dbo.GetTermName(TestDate) As TermName from Score
  inner join SubjectList SL on Score.SubjectID = SL.SubjectID
  inner join Grade G on Score.GradeID = G.GradeID
  End
GO
/****** Object:  StoredProcedure [dbo].[Prc_GetSubjectList]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Procedure [dbo].[Prc_GetSubjectList]
As
Begin
    Set NOCOUNT ON
	Select * From SubjectList
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_GetSubjectsByGrade]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[Prc_GetSubjectsByGrade]
As
Begin
Set NoCount ON
Select SL.SubjectName, G.GradeName,S.SubjectID, G.GradeID 
from SubjectList SL
inner join Subject S on SL.SubjectID = S.SubjectID
right join Grade G on G.GradeID=S.GradeID
Order by G.GradeID
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_GetSubjectsBySelectedGrade]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


create Proc [dbo].[Prc_GetSubjectsBySelectedGrade]
@GradeID int
As
Begin
Set NoCount ON
Select SL.SubjectName, G.GradeName,S.SubjectID, S.GradeID 
from SubjectList SL
inner join Subject S on SL.SubjectID = S.SubjectID
inner join Grade G on G.GradeID=S.GradeID
where G.GradeID = @GradeID
Order by G.GradeID
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_GetTermList]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Proc [dbo].[Prc_GetTermList]
As
Begin
Set NoCount On
Select TermName From TermList
End
GO
/****** Object:  StoredProcedure [dbo].[Prc_GetTermScoreBySubject]    Script Date: 6/20/2021 8:38:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create Procedure [dbo].[Prc_GetTermScoreBySubject]
(
	@GradeID int
)
As
Begin
	Set NoCount On
	Select SL.SubjectName, Avg(Score), dbo.GetTermName(TestDate) As TermName from Score
	inner join SubjectList SL on Score.SubjectID = SL.SubjectID
	inner join Grade G on Score.GradeID = G.GradeID
	where G.GradeID =@GradeID
	group by SL.SubjectName,dbo.GetTermName(TestDate)
End
GO
USE [master]
GO
ALTER DATABASE [SnehaDB] SET  READ_WRITE 
GO
