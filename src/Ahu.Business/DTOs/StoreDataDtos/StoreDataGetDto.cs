using Microsoft.AspNetCore.Http;

namespace Ahu.Business.DTOs.StoreDataDtos;

public record StoreDataGetDto(Guid Id, string Phone, string Address, string LogoText, string CompanyName, string AboutCompany, string WhatsappLink,
    string InstagramLink, string FacebookLink, string LinkedinLink, string LogoImageName, IFormFile LogoImageFile, string LogoImageLink, string EmptyBasketImageName,
    string EmptyBasketImageLink);