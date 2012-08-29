stackato-mssql-example
======================

An example of an ASP.NET web application which accesses an MSSQL database provisioned on stackato.

Had to deploy from pretty deep in the application tree:

    $ cd <somedir>\HelloStackato\HelloStackato\HelloStackato
    $ stackato push

Target stackato via VMC to make sure we can find the MSSQL service.

    $ vmc target <somestackatoinstance>
    $ vmc login <someemail>
    $ vmc services

You'll probably see that some services are available, but you may need to provision .

		
		============== System Services ==============
		
		+------------+------------+------------------------------------------+
		| Service    | Version    | Description                              |
		+------------+------------+------------------------------------------+
		| filesystem | 1.0        | Persistent filesystem service            |
		| memcached  | 1.4        | Memcached in-memory object cache service |
		| mongodb    | 1.8        | MongoDB NoSQL store                      |
		| mssql      | 10.50.2500 | MS SQL database service                  |
		| mysql      | 5.5        | MySQL database service                   |
		| postgresql | 9.1        | PostgreSQL database service              |
		| rabbitmq   | 2.4        | RabbitMQ message queue                   |
		| redis      | 2.4        | Redis key-value store service            |
		+------------+------------+------------------------------------------+
		
		=========== Provisioned Services ============

Provision an MSSQL instance:

    $ vmc create-service mssql
    Creating Service [mssql-a450e]: OK

Update the application to bind to that service (choose YES when it asks you to bind to a service, and select what we just provisioned).

    $ stackato update HelloStackato

		Updating application 'HelloStackato'...
		Stopping Application [HelloStackato]: OK
		Application Url: http://SOMEURL
		Would you like to bind any services to 'HelloStackato' ?  [yN]: y
		Would you like to use an existing provisioned service ?  [yN]: y
		The following provisioned services are available
		1. mssql-a450e
		2. <None of the above>
		Please select one you wish to use: 1
		Binding Service: OK
		Would you like to bind another service ?  [yN]: n
		Uploading Application [HelloStackato]:
		  Checking for bad links:  OK
		  Copying to temp space:  OK
		  Checking for available resources:  OK
		  Processing resources: OK
		  Packing application: OK
		  Uploading (2K):  OK
		Push Status: OK
		Staging Application [HelloStackato]: OK
		Starting Application [HelloStackato]: .OK

Check that the Web.config for our deployed app has a connection string for our MSSQL instance.

    $ stackato files HelloStackato app/Web.config

    ...snip...
      <connectionStrings>
        <add name="Default" connectionString="CONNSTRING" />
      </connectionStrings>
    ...snip...

You should now be able to do 'stackato update HelloStackato' (no need to re-bind the MSSQL service), hit the URL of the app, and see that the connection is in an 'open' state.

Now let's tunnel into the MSSQL instance and make a table that we can read and write to. Note that the name of your MSSQL instance will differ from what's written below.

    $ vmc tunnel mssql-a450e sqlcmd

	Deploying tunnel application 'caldecott'.
	Uploading Application:
	  Checking for available resources: OK
	  Packing application: OK
	  Uploading (1K): OK
	Push Status: OK
	Binding Service [mssql-a450e]: OK
	Staging Application 'caldecott': OK
	Starting Application 'caldecott': OK
	Getting tunnel connection info: OK
	
	Service connection info:
	  username : <username>
	  password : <pass>
	  name     : d42132a56a2994213ba479990595fbac7
	
	Starting tunnel to mssql-a450e on port 10000.
	Launching 'sqlcmd -S localhost,10000 -U <username> -P <pass> -d d42132a56a2994213ba479990595fbac7'
	
	1> select @@IDENTITY;
	2> GO
	
	----------------------------------------
	                                    NULL
	
	(1 rows affected)
	1> select 1 AS TMP
	2> GO
	TMP
	-----------
	          1
	
	(1 rows affected)
	1>


You can use the displayed connection information to connect via SQL Management Studio (use SQL Server Auth). Just use localhost,10000 as the �Server name�. Be sure to start the tunnel first and keep it open during the time you�re using Management Studio. Quitting sqlcmd will close the tunnel.

At this point, you should then be able to run table.sql either through SQLCMD, or via SSMS.  This will create the table necessary for our example to work.  Note that you need to update the 'Use d42132a56a2994213ba479990595fbac7' in table.sql to reflect the MSSQL instance name given when you provisioned your own instance.

Refs:

* http://www.activestate.com/blog/2012/07/how-deploy-net-apps-stackato-20  -- How to deploy .NET apps to stackato
* http://blog.cloudfoundry.com/2011/11/17/now-you-can-tunnel-into-any-cloud-foundry-data-service/ -- Tunnel to MSSQL in stackato
* http://blog.ironfoundry.org/2012/04/caldecott-with-ms-sql-server/ -- More on getting MSSQL up and running / tunnelling to it in stackato
    