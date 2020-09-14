#!/bin/bash
sleep 5s

echo 'Criando Database'
/opt/mssql-tools/bin/sqlcmd -S $SERVER_HOST -U $USER -P $SA_PASSWORD -Q 'CREATE DATABASE ['$DATABASE']'

echo 'Criando base de dados'
/opt/mssql-tools/bin/sqlcmd -S $SERVER_HOST -U $USER -P $SA_PASSWORD -d $DATABASE -i create-database.sql