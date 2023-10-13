using Microsoft.AspNetCore.Http;

namespace Ahu.Business.DTOs.SliderDtos;

public record SliderGetDto(Guid Id, string Title, string Description, string ImageName, string ImageUrl);