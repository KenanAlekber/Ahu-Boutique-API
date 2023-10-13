using Microsoft.AspNetCore.Http;

namespace Ahu.Business.DTOs.SliderDtos;

public record SliderPostDto(string Title, string Description, IFormFile? ImageFile);