using System.ComponentModel.DataAnnotations;

namespace Zajecia6;

public record GetAllAnimalsResponse(int Id, string Name, string Description, string Category, string Area);

public record GetSingleAnimalResponse(int Id, string Name, string Description, string Category, string Area);

public record CreateAnimalRequest(
	[Required][MaxLength(200, ErrorMessage = "Has to be shorter than 200 letters")] string Name,
	[MaxLength(200, ErrorMessage = "Has to be shorter than 200 letters")] string Description,
	[Required][MaxLength(200, ErrorMessage = "Has to be shorter than 200 letters")] string Category,
	[Required][MaxLength(200, ErrorMessage = "Has to be shorter than 200 letters")] string Area
);

public record EditAnimalRequest(
	[Required][MaxLength(200, ErrorMessage = "Has to be shorter than 200 letters")] string Name,
	[MaxLength(200, ErrorMessage = "Has to be shorter than 200 letters")] string Description,
	[Required][MaxLength(200, ErrorMessage = "Has to be shorter than 200 letters")] string Category,
	[Required][MaxLength(200, ErrorMessage = "Has to be shorter than 200 letters")] string Area
	);