DELETE FROM `schooldb`.`Students` WHERE Id >= 0;
ALTER TABLE `schooldb`.`Students` AUTO_INCREMENT = 1;

INSERT INTO `schooldb`.`students` ( `FirstName`, `LastName`, `DateOfBirth`, `Sex`)
VALUES ( 
<{LastName: }>,
<{DateOfBirth: }>,
<{Sex: }>);



