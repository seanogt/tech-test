

====== Sql Container ======
docker run -e ACCEPT_EULA=Y -e SA_PASSWORD=zaq1ZAQ! -p 1434:1433 -d --name sqlserver --network testnet --net-alias ss mcr.microsoft.com/mssql/server:2017-CU8-ubuntu

Connect to sql db and Run CreateSeededDb.Sql
server name: 127.0.0.1,1434
login: sa
password: zaq1ZAQ!

The above steps need to be scripted
Docker compose can start up db, migrations, SUT
So that everything is in correct state for integration tests