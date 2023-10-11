using Microsoft.AspNetCore.Http;

namespace Ahu.Business.DTOs.StoreDataDtos;

public record StoreDataPostDto(string Phone, string Address, string LogoText, string CompanyName, string AboutCompany, string WhatsappLink,
    string InstagramLink, string FacebookLink, string LinkedinLink, IFormFile LogoImageFile, IFormFile EmptyBasketImageFile);