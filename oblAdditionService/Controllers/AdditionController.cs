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


            var NumbersLeftinserted = addDatabaseConnection.Query<string>("Select leftNumber from Numbers");
            var NumbersRightinserted = addDatabaseConnection.Query<string>("Select rightNumber from Numbers");
            var leftList = NumbersLeftinserted.ToList();
            var rightList = NumbersRightinserted.ToList();
            for (int i = 0; i < leftList.Count(); i++)
            {
                leftList[i] += " + " + rightList[i] + " = " + (Convert.ToInt32(leftList[i]) + Convert.ToInt32(rightList[i]));
            }

            return leftList;
        }
    }
}