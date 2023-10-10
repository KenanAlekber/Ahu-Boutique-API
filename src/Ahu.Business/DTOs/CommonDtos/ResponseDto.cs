using System.Net;

namespace Ahu.Business.DTOs.CommonDtos;

public record ResponseDto(Guid Id, HttpStatusCode StatusCode, string Message);