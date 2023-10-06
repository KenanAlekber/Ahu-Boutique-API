﻿using Ahu.Core.Entities;

namespace Ahu.Business.DTOs.BrandDtos;

public record BrandPostDto(Guid Id, string Name, List<Product> Products);