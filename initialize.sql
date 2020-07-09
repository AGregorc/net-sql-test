drop database if exists ec38;
create database ec38;
use ec38;


CREATE TABLE kosi (
   id              INT            NOT   NULL   AUTO_INCREMENT,
   guid            INT,
   delovni_nalog   VARCHAR(30),
   cas_vnosa       TIMESTAMP      NOT   NULL DEFAULT CURRENT_TIMESTAMP,
   PRIMARY   KEY   (id)
);
