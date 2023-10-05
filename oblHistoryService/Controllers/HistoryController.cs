using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySqlConnector;
using System.Data;

namespace oblHistoryService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HistoryController : ControllerBase
    {
        private IDbConnection addDatabaseConnection = new MySqlConnection("Server=additionDatabase;Database=add-database;Uid=add-bruger;Pwd=C@ch3d1v2;");



    private readonly ILogger<HistoryController> _logger;

        public HistoryController(ILogger<HistoryController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        public IEnumerable<string> Post(int leftNumber, int rightNumber, bool isAddition, int result)
        {
            addDatabaseConnection.Open();
            var tables = addDatabaseConnection.Query<string>("SHOW TABLES LIKE 'AllNumbers'");
            if (!tables.Any())
            {
                addDatabaseConnection.Execute("CREATE TABLE AllNumbers (leftNumber int, rightNumber int,isAddition boolean, result int)");
            }
            addDatabaseConnection.Execute("INSERT INTO AllNumbers (leftNumber, rightNumber,isAddition, result) VALUES (@leftNum, @rightNum, @isAdd, @res)", new { leftNum = leftNumber, rightNum = rightNumber, isAdd = isAddition, res = result });

            var NumbersLeftinserted = addDatabaseConnection.Query<string>("Select leftNumber from AllNumbers");
            var NumbersRightinserted = addDatabaseConnection.Query<string>("Select rightNumber from AllNumbers");
            var isAdditioninserted = addDatabaseConnection.Query<bool>("Select isAddition from AllNumbers");
            var resultsinserted = addDatabaseConnection.Query<string>("Select result from AllNumbers");
            var leftList = NumbersLeftinserted.ToList();
            var rightList = NumbersRightinserted.ToList();
            var addList = isAdditioninserted.ToList();
            var resultList = resultsinserted.ToList();

            for (int i = 0; i < leftList.Count(); i++)
            {
                if (addList[i])
                {
                    leftList[i] += " + " + rightList[i] + " = " + resultList[i];
                }
                else
                {
                    leftList[i] += " - " + rightList[i] + " = " + resultList[i];
                }
            }
            return leftList;
        }
    }
}
