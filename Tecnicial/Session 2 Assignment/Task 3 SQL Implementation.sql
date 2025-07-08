--------------------------------------Task 3: SQL Implementation----------------------------------------------------
--CREATE DATABASE
CREATE DATABASE COMPANY;
GO 
USE company;
--1)Create the EMPLOYEE table with all constraints
CREATE TABLE EMPLOYEE(
SSN INT PRIMARY KEY,
Fname varchar(50) NOT NULL,
lname varchar(50) NOT NULL,
BIRTH_DATE DATE,
gender char check (gender='M' or gender='m' or gender='F' or gender ='f'),
SUPERSSN INT REFERENCES EMPLOYEE( SSN),
DNUM INT  )
--2)Create the DEPARTMENT table with proper relationships
--A
CREATE TABLE DEPARTMENT (
DNUM INT PRIMARY KEY,
DEPARTMENT_NAME VARCHAR(50) NOT NULL,
MGR_SSN INT REFERENCES EMPLOYEE(SSN ),
HIRING_DATE DATE)
GO
--B create DEPARTMENT_LOCTION
CREATE TABLE DEPARTMENT_LOCTION
(
DNUM INT REFERENCES DEPARTMENT(DNUM) ,
_LOCATION VARCHAR(255 ) NOT NULL
PRIMARY KEY(DNUM,_LOCATION))
--3)Create the PROJECT table
CREATE TABLE PROJECT(
PNumber int primary key,
Pname varchar(50) not null,
city varchar(50),
DNUM INT)
--4)Add foreign key constraints between tables 
ALTER TABLE EMPLOYEE
ADD CONSTRAINT FK_DNUM FOREIGN KEY (DNUM) REFERENCES DEPARTMENT(DNUM);
ALTER TABLE PROJECT
ADD CONSTRAINT FK_DNUM_PROJECT FOREIGN KEY (DNUM) REFERENCES DEPARTMENT(DNUM);
--5)Insert sample data into EMPLOYEE table (at least 5 employees)
INSERT INTO EMPLOYEE
VALUES(1, 'YAHYA', 'RAMDAN','2001-12-04' , 'M', NULL, NULL);
INSERT INTO EMPLOYEE
VALUES(5, 'EHAP', 'RAMDAN','2001-12-04',  'M', 1, NULL),(2, 'YOUSSEF', 'RAMDAN','2001-12-04',  'M', 1, NULL),(3, 'MOHAMED', 'RAMDAN','2001-12-04',  'M', 1, NULL),(4, 'SAPER', 'RAMDAN','2001-12-04' , 'M', 1, NULL)
SELECT *
FROM EMPLOYEE
--6)Insert sample data into DEPARTMENT table (at least 3 departments)
INSERT INTO DEPARTMENT 
VALUES 
(1, 'IT', 1, '2022-01-15'),
(2, 'HR', 2, '2021-06-01'),
(3, 'Finance', 3, '2020-03-20');
--7)Update an employee's department
UPDATE DEPARTMENT
SET DEPARTMENT_NAME='HR' 
WHERE DNUM=1
--8)Delete a dependent record
--8)a create table dependent
CREATE TABLE DEPENDENT 
(SSN INT,
DEPENDENT_NAME VARCHAR(50),
BIRTH_DATE DATE,
gender char check (gender='M' or gender='m' or gender='F' or gender ='f'),
PRIMARY KEY(SSN,DEPENDENT_NAME)
)
--8)B add vlaues in dependent
INSERT INTO DEPENDENT (SSN, DEPENDENT_NAME, BIRTH_DATE, Gender)
VALUES (1, 'ALI', '2010-01-01', 'M');
SELECT *FROM DEPENDENT
--8)C Delete a dependent record 
DELETE FROM DEPENDENT
WHERE SSN = 1 
SELECT *FROM DEPENDENT
--9)Retrieve all employees working in a specific department
---NOTE BECAUSE I DIDN'T INSERT ANY DNUM VALUES IN EMPLOYEE I INSERT IT NOW BY UPDATE
UPDATE EMPLOYEE 
SET DNUM =1
WHERE  BIRTH_DATE='2001-12-04';
GO
--Retrieve all employees working in a specific department
SELECT *
FROM EMPLOYEE
WHERE DNUM=1;
--10)Find all employees and their project assignments with working hours
--10)A CREATE TABLE EMPLOYEE_PROJECT
CREATE TABLE EMPLOYEE_PROJECT
(EMP_SSN INT REFERENCES EMPLOYEE(SSN),
EMP_PNUM INT REFERENCES PROJECT(PNUMBER),
HOURS FLOAT 
PRIMARY KEY(EMP_SSN,EMP_PNUM))
--10)B INSERT DATA IN PROJECT FIRST 
INSERT INTO PROJECT 
VALUES (1, 'ERD1', 'CAIRO', 1),(2, 'MAPPING', 'MENOFIA', 2);
GO
--10)C INSERT DATA IN PROJECT SECOND
INSERT INTO EMPLOYEE_PROJECT (EMP_SSN, EMP_PNUM, HOURS)
VALUES 
(1, 1, 8.5),
(1, 2, 4.0),
(2, 2, 5.0);
--10)D Find all employees and their project assignments with working hours
--NOTE WE DID'T TAKE LECTURE ABOUT JOIN YET SO I SELECT ONLY EMPLOYEE SSN AND HIS PROJECT NUMPER AND HOURS IN THIS PROJECT
SELECT *FROM PROJECT



