SET IDENTITY_INSERT Movie ON
GO

MERGE Movie AS TARGET
USING (VALUES ( 1, 'Dr. No',                           '1962-10-10'),
              ( 2, 'From Russia with Love',            '1964-05-27'),
              ( 3, 'Goldfinger',                       '1965-01-09'),
              ( 4, 'Thunderball',                      '1965-12-22'),
              ( 5, 'You Only Live Twice',              '1967-06-13'),
              ( 6, 'On Her Majesty''s Secret Service', '1969-12-19'),
              ( 7, 'Diamonds Are Forever',             '1971-12-17'),
              ( 8, 'Live and Let Live',                '1973-06-27'),
              ( 9, 'The Man with the Golden Gun',      '1974-12-20'),
              (10, 'The Spy Who Loved Me',             '1997-08-03'),
              (11, 'Moonraker',                        '1979-06-29'),
              (12, 'For Your Eyes Only',               '1981-06-26'),
              (13, 'Octopussy',                        '1983-06-10'),
              (14, 'Never Say Never Again',            '1983-10-07'),
              (15, 'A View to a Kill',                 '1985-05-24'),
              (16, 'The Living Daylights',             '1987-07-31'),
              (17, 'License to Kill',                  '1989-07-14'),
              (18, 'GoldenEye',                        '1995-11-17'),
              (19, 'Tomorrow Never Dies',              '1997-12-19'),
              (20, 'The World Is Not Enough',          '1999-11-19'),
              (21, 'Die Another Day',                  '2002-11-22'),
              (22, 'Casino Royale',                    '2006-11-17'),
              (23, 'Quantum of Solace',                '2008-11-14'),
              (24, 'Skyfall',                          '2012-11-09'),
              (25, 'Spectre',                          '2015-11-06'),
              (26, 'No Time to Die',                   '2021-10-08'))
AS SOURCE (MovieId,
           Title,
           ReleaseDate)
ON TARGET.MovieId = SOURCE.MovieId
WHEN MATCHED THEN UPDATE SET TARGET.Title       = SOURCE.Title,
                             TARGET.ReleaseDate = SOURCE.ReleaseDate
WHEN NOT MATCHED THEN INSERT (MovieId,
                              Title,
                              ReleaseDate)
                      VALUES (SOURCE.MovieId,
                              SOURCE.Title,
                              SOURCE.ReleaseDate);

SET IDENTITY_INSERT Movie OFF
GO