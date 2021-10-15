CREATE TABLE dbo.Movie
(
  MovieId     INT           NOT NULL IDENTITY(1,1),
  Title       NVARCHAR(200) NOT NULL,
  ReleaseDate DATE          NOT NULL,
  CONSTRAINT pkcMovie PRIMARY KEY CLUSTERED (MovieId)
)