using Microsoft.AspNetCore.Http;

namespace Ahu.Business.DTOs.StoreDataDtos;

public class StoreDataPutDto
{
    public string? Phone { get; set; }
    public string? Address { get; set; }
    public string? LogoText { get; set; }
    public string? CompanyName { get; set; }
    public string? AboutCompany { get; set; }
    public string? WhatsappLink { get; set; }
    public string? InstagramLink { get; set; }
    public string? FacebookLink { get; set; }
    public string? LinkedinLink { get; set; }
    public IFormFile? LogoImageFile { get; set; }
}