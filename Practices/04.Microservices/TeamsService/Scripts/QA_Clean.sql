/* Delete Previous Data and Reset the Auto_Incremental Keys */
TRUNCATE TABLE `teamsservicedb`.`teams` ;

/*********/
/* TEAMS */
/*********/
INSERT INTO `teamsservicedb`.`teams` (Name, Coach, Conference) VALUES 
  ('Atlanta Hawks', 'Nate McMillan', 'East'),
  ('Boston Celtics', 'Ime Udoka', 'East'),
  ('Brooklyn Nets', 'Steve Nash', 'East'),
  ('Charlotte Hornets', 'James Borrego', 'East'),
  ('Chicago Bulls', 'Billy Donovan', 'East'),
  ('Cleveland Cavaliers', 'J. Bickerstaff', 'East'),
  ('Detroit Pistons', 'Dwane Casey', 'East'),
  ('Indiana Pacers', 'Rick Carlisle', 'East'),
  ('Miami Heat', 'Erik Spoelstra', 'East'),
  ('Milwaukee Bucks', 'Mike Budenholzer', 'East'),
  ('New York Knicks', 'Tom Thibodeau', 'East'),
  ('Orlando Magic', 'Jamahl Mosley', 'East'),
  ('Philadelphia 76ers', 'Doc Rivers', 'East'),
  ('Toronto Raptors', 'Nick Nurse', 'East'),
  ('Washington Wizards', 'Wes Unseld Jr.', 'East'),
  ('Dallas Mavericks', 'Jason Kidd', 'West'),
  ('Denver Nuggets', 'Michael Malone', 'West'),
  ('Golden State Warriors', 'Steve Kerr', 'West'),
  ('Houston Rockets', 'Stephen Silas', 'West'),
  ('Los Angeles Clippers', 'Tyronn Lue', 'West'),
  ('Los Angeles Lakers', 'Darvin Ham', 'West'),
  ('Memphis Grizzlies', 'Taylor Jenkins', 'West'),
  ('Minnesota Timberwolves', 'Chris Finch', 'West'),
  ('Oklahoma City Thunder', 'Mark Daigneault', 'West'),
  ('Phoenix Suns', 'Monty Williams', 'West'),
  ('Sacramento Kings', 'Mike Brown', 'West'),
  ('San Antonio Spurs', 'Gregg Popovich', 'West'),
  ('Utah Jazz', 'Quin Snyder', 'West');
  