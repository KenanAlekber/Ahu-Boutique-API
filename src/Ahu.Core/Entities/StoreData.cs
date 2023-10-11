using Ahu.Core.Entities.Common;

namespace Ahu.Core.Entities;

public class StoreData : BaseSectionEntity
{
    public string Phone { get; set; }
    public string Address { get; set; }
    public string LogoText { get; set; }
    public string CompanyName { get; set; }
    public string AboutCompany { get; set; }
    public string WhatsappLink { get; set; }
    public string InstagramLink { get; set; }
    public string FacebookLink { get; set; }
    public string LinkedinLink { get; set; }
    public string LogoImageName { get; set; }
    public string LogoImageLink { get; set; }
    public string EmptyBasketImageName { get; set; }
    public string EmptyBasketImageLink { get; set; }
}