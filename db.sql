-- Create a new database
CREATE DATABASE IF NOT EXISTS student_registration;

-- Use the created database
USE student_registration;

-- Create a table to store student information
CREATE TABLE students (
 
student_registration.studentsstudent_registration.studentscreated_at    first_name VARCHAR(50) primary key,
    last_name VARCHAR(50) NOT NULL,
    email VARCHAR(100) NOT NULL UNIQUE,
    phone_number VARCHAR(15) NOT NULL,
    date_of_birth DATE NOT NULL,
    gender ENUM('Male', 'Female', 'Transgender') NOT NULL,
    address TEXT NOT NULL,
    course VARCHAR(100) NOstudent_registration.studentsstudent_registration.studentsT NULL,
   -- photo BLOB,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP 
);
edit students;
ALTER TABLE students
ADD COLUMN document_path LONGBLOB;
