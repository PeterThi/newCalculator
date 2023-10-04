using Microsoft.AspNetCore.Mvc;
//using MySql.Data.MySqlClient;
using System.Data;
using MySqlConnector;
using Dapper;

namespace oblAdditionService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdditionController : ControllerBase
    {
        private IDbConnection addDatabaseConnection = new MySqlConnection("Server=additionDatabase;Database=add-database;Uid=add-bruger;Pwd=C@ch3d1v2;");
        public AdditionController()
        {

        }

        [HttpPost]
        public int Add(int leftNumber, int rightNumber)
        {
            Console.WriteLine("Got a request for :" + leftNumber);

            addDatabaseConnection.Open();
            var tables = addDatabaseConnection.Query<string>("SHOW TABLES LIKE 'counters'");
            /*String createTableQuery = "CREATE TABLE IF NOT EXISTS AddTable(" +
                "leftNumber int not null," +
                "rightNumber int not null)";*/

            Console.WriteLine(tables);
            
           // using MySqlCommand cmd = new MySqlCommand(createTableQuery, addDatabaseConnection);
            //cmd.ExecuteNonQuery();

            /*String insertQuery = "INSERT INTO AddTable(leftNumber, rightNumber) values (@leftNumber, @rightNumber)";
            using MySqlCommand insertcmd = new MySqlCommand(insertQuery, connection);

            Console.WriteLine(insertcmd.ExecuteNonQuery());*/
            return leftNumber + rightNumber;
        }
    }
}