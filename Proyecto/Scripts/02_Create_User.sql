/*DROP USER 'arepasuser'@'localhost' ;*/
CREATE USER 'arepasuser'@'localhost' IDENTIFIED BY 'Ar3p4sUs3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'arepasuser'@'localhost' WITH GRANT OPTION;
FLUSH PRIVILEGES;