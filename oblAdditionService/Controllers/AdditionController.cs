using Microsoft.AspNetCore.Mvc;
//using MySql.Data.MySqlClient;
using System.Data;
using MySqlConnector;
using Dapper;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public IEnumerable<string> Add(int leftNumber, int rightNumber)
        {
            Console.WriteLine("Got a request for :" + leftNumber);

            addDatabaseConnection.Open();
            var tables = addDatabaseConnection.Query<string>("SHOW TABLES LIKE 'Numbers'");
            if (!tables.Any())
            {
                addDatabaseConnection.Execute("CREATE TABLE Numbers (leftNumber int, rightNumber int)");
            }

            addDatabaseConnection.Execute("INSERT INTO Numbers (leftNumber, rightNumber) VALUES (@leftNum, @rightNum)", new { leftNum = leftNumber, rightNum = rightNumber });

            var Numbersinserted = addDatabaseConnection.Query<string>("Select leftNumber, rightNumber from Numbers");

            Console.WriteLine("values gotten from database: " + Numbersinserted);
            foreach(var items in Numbersinserted)
            {
                Console.WriteLine("Found number in database: " + items.ToString());
            }
            
            return Numbersinserted;
        }
    }
}