/*DROP USER 'moneybankuser'@'localhost' ;*/
CREATE USER IF NOT EXISTS 'moneybankuser'@'%' IDENTIFIED BY 'M0n3yB4nkUs3r*01';
GRANT ALL PRIVILEGES ON moneybankdb.* TO 'moneybankuser'@'%';
FLUSH PRIVILEGES;