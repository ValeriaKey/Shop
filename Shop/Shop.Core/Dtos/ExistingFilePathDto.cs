using System;
namespace Shop.Core.Dtos
{
    public class ExistingFilePathDto
    {
        public Guid PhotoId { get; set; }
        public string FilePath { get; set; }
        public Guid ProductId { get; set; }
    }
}
