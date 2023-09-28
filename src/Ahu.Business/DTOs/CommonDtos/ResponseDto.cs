using System.Net;

namespace Ahu.Business.DTOs.CommonDtos;

public record ResponseDto(HttpStatusCode StatusCode, string Message);