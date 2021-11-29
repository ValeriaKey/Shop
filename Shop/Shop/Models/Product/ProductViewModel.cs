﻿using Microsoft.AspNetCore.Http;
using Shop.Core.Dtos;
using Shop.Models.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models.Product
{
    public class ProductViewModel
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }

        public List<IFormFile> Files { get; set; }
        public List<ExistingFilePathDto> ExistingFilePaths { get; set; } = new List<ExistingFilePathDto>();
    }
}
