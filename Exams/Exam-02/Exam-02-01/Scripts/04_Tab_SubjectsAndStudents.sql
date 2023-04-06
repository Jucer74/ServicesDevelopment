CREATE TABLE `schooldb`.`SubjectsAndStudents` (
  `SubjectId` INT NOT NULL,
  `StudentId` INT NOT NULL,
  PRIMARY KEY (`SubjectId`, `StudentId`),
  INDEX `StudentId_idx` (`StudentId` ASC) VISIBLE,
  CONSTRAINT `Fk_Subject`
    FOREIGN KEY (`SubjectId`)
    REFERENCES `schooldb`.`subjects` (`Id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `Fk_Student`
    FOREIGN KEY (`StudentId`)
    REFERENCES `schooldb`.`students` (`Id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE);
