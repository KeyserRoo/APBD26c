using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Zajecia6;

[ApiController]
[Route("/api/animals-c")]
public class AnimalControllers : ControllerBase
{
	private IConfiguration _config;
	private readonly IValidator<CreateAnimalRequest> _createAnimalValidator;
	private readonly IValidator<EditAnimalRequest> _editAnimalValidator;

	public AnimalControllers(IConfiguration config, IValidator<CreateAnimalRequest> createAnimalValidator, IValidator<EditAnimalRequest> editAnimalValidator)
	{
		_config = config;
		_createAnimalValidator = createAnimalValidator;
		_editAnimalValidator = editAnimalValidator;
	}
	
	[HttpGet]
	public IActionResult GetAllAnimals(string orderBy = null)
	{
		var animals = new List<GetAllAnimalsResponse>();
		using (var sqlConnection = new SqlConnection(_config.GetConnectionString("Default")))
		{
			string query = "SELECT * FROM Animal";
			if (!string.IsNullOrEmpty(orderBy))
			{
				var getColumnNames = new SqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME='Animal'", sqlConnection);
				getColumnNames.Connection.Open();
				var readerC = getColumnNames.ExecuteReader();

				while (readerC.Read())
				{
					if (orderBy.ToLower() == readerC.GetString(0).ToLower()) query += $" ORDER BY {orderBy} ASC";
				}
				readerC.Close();
				getColumnNames.Connection.Close();
			}
			else
			{
				query += $" ORDER BY name ASC";
			}

			var selectAllQuery = new SqlCommand(query, sqlConnection);
			selectAllQuery.Connection.Open();
			var readerS = selectAllQuery.ExecuteReader();
			while (readerS.Read())
			{
				animals.Add(new GetAllAnimalsResponse(
					readerS.GetInt32(0),
					readerS.GetString(1),
					readerS.IsDBNull(2) ? null : readerS.GetString(2),
					readerS.GetString(3),
					readerS.GetString(4))
				);
			}
			readerS.Close();
			selectAllQuery.Connection.Close();
		}
		return Ok(animals);
	}
	
	[HttpGet("{id}")]
	public IActionResult GetAnimal(int id)
	{
		GetSingleAnimalResponse animal = null;
		using (var sqlConnection = new SqlConnection(_config.GetConnectionString("Default")))
		{
			string query = $"SELECT * FROM Animal WHERE IdAnimal={id}";

			var selectAllQuery = new SqlCommand(query, sqlConnection);
			selectAllQuery.Connection.Open();
			var readerS = selectAllQuery.ExecuteReader();
			if (!readerS.Read()) return NotFound("Animal with this index does not exist!");
			animal = new GetSingleAnimalResponse(
			readerS.GetInt32(0), readerS.GetString(1), readerS.IsDBNull(2) ? null : readerS.GetString(2), readerS.GetString(3), readerS.GetString(4));

			readerS.Close();
			selectAllQuery.Connection.Close();
		}

		return Ok(animal);
	}
	[HttpPost]
	public IActionResult CreateAnimal(CreateAnimalRequest animal)
	{
		// if (json.IsNullOrEmpty()) return BadRequest("You must provide data");
		// if (!CheckIsJson(json)) return BadRequest("Data must be formatted as JSON");

		// CreateAnimalRequest animal;
		try
		{
			// animal = JsonConvert.DeserializeObject<CreateAnimalRequest>(json);

			if (animal == null) return BadRequest("You must provide data");
			var validation = _createAnimalValidator.Validate(animal);
			if (!validation.IsValid)
			{
				var problemDetails = new ValidationProblemDetails(new ModelStateDictionary());
				foreach (var error in validation.Errors)
				{
					problemDetails.Errors.Add(error.PropertyName, new[] { error.ErrorMessage });
				}
				return ValidationProblem(problemDetails);
			}
		}
		catch (Newtonsoft.Json.JsonException)
		{
			return BadRequest("Pass correct data!");
		}

		string connectionString = _config.GetConnectionString("Default");
		if (connectionString.IsNullOrEmpty())
		{
			return NotFound("Database connection string is not configured.");
		}

		using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
		{
			connection.Open();
			if (animal.Description != null)
				using (var command = new SqlCommand(
						"INSERT INTO Animal (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area);", connection))
				{
					command.Parameters.AddWithValue("@Name", animal.Name);
					command.Parameters.AddWithValue("@Description", animal.Description);
					command.Parameters.AddWithValue("@Category", animal.Category);
					command.Parameters.AddWithValue("@Area", animal.Area);

					command.ExecuteNonQuery();
				}
			else
				using (var command = new SqlCommand(
						"INSERT INTO Animal (Name, Category, Area) VALUES (@Name, @Category, @Area);", connection))
				{
					command.Parameters.AddWithValue("@Name", animal.Name);
					command.Parameters.AddWithValue("@Category", animal.Category);
					command.Parameters.AddWithValue("@Area", animal.Area);

					command.ExecuteNonQuery();
				}
		}
		return Ok();
	}
	[HttpPut("{id}")]
	public IActionResult UpdateAnimal(int id, string json)
	{
		if (json.IsNullOrEmpty()) return BadRequest("You must provide data");
		if (!CheckIsJson(json)) return BadRequest("Data must be formatted as JSON");

		EditAnimalRequest animal;
		try
		{
			animal = JsonConvert.DeserializeObject<EditAnimalRequest>(json);
			if (animal == null) return BadRequest("You must provide data");
			var validation = _editAnimalValidator.Validate(animal);
			if (!validation.IsValid)
			{
				var problemDetails = new ValidationProblemDetails(new ModelStateDictionary());
				foreach (var error in validation.Errors)
				{
					problemDetails.Errors.Add(error.PropertyName, new[] { error.ErrorMessage });
				}
				return ValidationProblem(problemDetails);
			}
		}
		catch (Newtonsoft.Json.JsonException)
		{
			return BadRequest("Pass correct data!");
		}

		using (var connection = new SqlConnection(_config.GetConnectionString("Default")))
		{
			connection.Open();
			int affected = -1;
			if (animal.Description != null)
			{
				using (var command = new SqlCommand(
						"UPDATE Animal SET Name = @Name, Description = @Description, Category = @Category, Area = @Area WHERE IdAnimal = @Id;", connection))
				{
					command.Parameters.AddWithValue("@Id", id);
					command.Parameters.AddWithValue("@Name", animal.Name);
					command.Parameters.AddWithValue("@Description", animal.Description);
					command.Parameters.AddWithValue("@Category", animal.Category);
					command.Parameters.AddWithValue("@Area", animal.Area);

					affected = command.ExecuteNonQuery();
				}
				if (affected == 0)
				{
					using (var command = new SqlCommand(
						"INSERT INTO Animal (Name, Description, Category, Area) VALUES (@Name, @Description, @Category, @Area);", connection))
					{
						command.Parameters.AddWithValue("@Name", animal.Name);
						command.Parameters.AddWithValue("@Description", animal.Description);
						command.Parameters.AddWithValue("@Category", animal.Category);
						command.Parameters.AddWithValue("@Area", animal.Area);

						command.ExecuteNonQuery();
					}
				}
			}
			else
			{
				using (var command = new SqlCommand(
					"UPDATE Animal SET Name = @Name, Category = @Category, Area = @Area WHERE IdAnimal = @Id;", connection))
				{
					command.Parameters.AddWithValue("@Id", id);
					command.Parameters.AddWithValue("@Name", animal.Name);
					command.Parameters.AddWithValue("@Category", animal.Category);
					command.Parameters.AddWithValue("@Area", animal.Area);

					affected = command.ExecuteNonQuery();
				}
				if (affected == 0)
				{
					using (var command = new SqlCommand(
						"INSERT INTO Animal (Name, Category, Area) VALUES (@Name, @Category, @Area);", connection))
					{
						command.Parameters.AddWithValue("@Name", animal.Name);
						command.Parameters.AddWithValue("@Category", animal.Category);
						command.Parameters.AddWithValue("@Area", animal.Area);

						command.ExecuteNonQuery();
					}
				}
			}
			return Ok();
		}
	}
	[HttpDelete("{id}")]
	public IActionResult RemoveAnimal(int id)
	{
		GetSingleAnimalResponse animal = null;
		using (var sqlConnection = new SqlConnection(_config.GetConnectionString("Default")))
		{
			sqlConnection.Open();
			string query = $"DELETE FROM Animal WHERE IdAnimal={id}";

			using (var deleteQuery = new SqlCommand(query, sqlConnection))
			{
				int rowsAffected = deleteQuery.ExecuteNonQuery();
				if (rowsAffected == 0) return NotFound("Animal with this index does not exist!");
			}
		}
		return Ok(animal);
	}
	private static bool CheckIsJson(string input)
	{
		try
		{
			var obj = System.Text.Json.JsonSerializer.Deserialize<object>(input);
			return true;
		}
		catch (System.Text.Json.JsonException)
		{
			return false;
		}
	}
}
