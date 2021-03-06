DROP DATABASE IF EXISTS todoapp;

CREATE DATABASE todoapp;

USE todoapp;

CREATE TABLE todos (
  id VARCHAR(36) NOT NULL,
  description VARCHAR(144) NOT NULL,
  is_completed BOOL NOT NULL DEFAULT false,
  timestamp DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (id)
);
