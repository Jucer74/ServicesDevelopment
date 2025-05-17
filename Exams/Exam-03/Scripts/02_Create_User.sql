DROP USER 'moneybankuser'@'%';
CREATE USER 'moneybankuser'@'%' IDENTIFIED BY 'M0n3yB4nkUs3r*01';
GRANT ALL PRIVILEGES ON *.* TO 'moneybankuser'@'%' WITH GRANT OPTION;
FLUSH PRIVILEGES;