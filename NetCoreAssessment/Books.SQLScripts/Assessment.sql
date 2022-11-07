GO
CREATE DATABASE Assessment;

GO
USE Assessment;

GO
CREATE TABLE Book(
	BookId INT NOT NULL IDENTITY(1,1),
	Publisher NVARCHAR(100) NOT NULL,
	Title NVARCHAR(100) NOT NULL,
	AuthorLastName NVARCHAR(100) NOT NULL,
	AuthorFirstName NVARCHAR(100) NOT NULL,
	Price DECIMAL(10,2) NOT NULL,
	Year INT NOT NULL,
	Edition NVARCHAR(100) NOT NULL,
	Place NVARCHAR(100) NOT NULL,
	PageNumbers NVARCHAR(100) NOT NULL,
	CONSTRAINT PK_Book_BookId PRIMARY KEY (BookId)
)

GO
CREATE TABLE [dbo].[ErrorLog] (
      ErrorLogId INT NOT NULL IDENTITY(1,1),
      Application NVARCHAR(100) NOT NULL,
      Logged DATETIME NOT NULL,
      Level NVARCHAR(100) NOT NULL,
      Message NVARCHAR(MAX) NOT NULL,
      Logger NVARCHAR(MAX) NULL,
      Callsite NVARCHAR(MAX) NULL,
      Exception NVARCHAR(MAX) NULL,
	  CONSTRAINT PK_ErrorLog_ErrorLogId PRIMARY KEY (ErrorLogId)
)

GO
CREATE PROCEDURE [dbo].[usp_BookList]
(
	@IsSortByPublisher BIT
)
AS BEGIN                            
SET NOCOUNT ON;
DROP TABLE IF EXISTS #TempBookList;
;WITH BookListCTE AS (
	SELECT *, CONCAT(RTRIM(LTRIM(AuthorLastName)), ' ', RTRIM(LTRIM(AuthorFirstName))) AS Author FROM Book
) SELECT * INTO #TempBookList FROM BookListCTE;
	IF @IsSortByPublisher = 1
		BEGIN
			SELECT BookId, Publisher, Title, AuthorLastName, AuthorFirstName, Price, Year, Edition, Place, PageNumbers FROM #TempBookList ORDER BY Publisher, Author, Title;
		END
	ELSE
		BEGIN
			SELECT BookId, Publisher, Title, AuthorLastName, AuthorFirstName, Price, Year, Edition, Place, PageNumbers FROM #TempBookList ORDER BY Author, Title;
		END
END;