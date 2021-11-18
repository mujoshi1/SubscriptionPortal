
CREATE TABLE Employee (
  EmpId int NOT NULL AUTO_INCREMENT,
  Name varchar(250) NOT NULL,
  EmailId varchar(50) DEFAULT NULL,
  Department varchar(20) DEFAULT NULL,
  Location varchar(200) DEFAULT NULL,
  PRIMARY KEY (EmpId)
) 