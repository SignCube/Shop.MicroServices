using Npgsql;

namespace Discount.API.Extensions
{
    public static class HostExtensions
    {
       
        public static IHost MigrateDatabase<TContext>(this IHost app, int? retry = 0)
        {
            int retryForAvaibality = retry.Value;

            WebApplicationBuilder b;
    

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var configuration = services.GetRequiredService<IConfiguration>(); 
                var logger = services.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Migrating PostgresSql Database");
                    using var connection = new NpgsqlConnection(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
                    connection.Open();

                    using var command = new NpgsqlCommand()
                    {
                        Connection = connection
                    };

                    command.CommandText = "DROP TABLE IF EXISTS Coupon";
                    command.ExecuteNonQuery();

                    command.CommandText = @"CREATE TABLE Coupon(Id serial primary key,
                                            ProductName varchar(24) not null,
                                            Description text,
                                            Amount int)";
                    command.ExecuteNonQuery();


                    command.CommandText = @"INSERT INTO Coupon(ProductName,Description,Amount) VALUES ('IPhone X','IPhone Discount',150)";
                    command.ExecuteNonQuery();

                    command.CommandText = @"INSERT INTO Coupon(ProductName,Description,Amount) VALUES ('Samsung 10','Samsung Discount',100)";
                    command.ExecuteNonQuery();

                    logger.LogInformation("Migrated PostgresSql Database");
                    
                }
                catch (NpgsqlException ex)
                {

                    logger.LogError(ex, "an error occured while migrating the postgresql database");
                    if(retryForAvaibality<1)
                    {
                        retryForAvaibality++;
                        System.Threading.Thread.Sleep(2000);
                        MigrateDatabase<TContext>(app, retryForAvaibality);

                        
                    }
                }
                
                return app;
            }
        }
    }
}
