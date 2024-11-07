using System.ComponentModel.DataAnnotations;

namespace ClearBank.DeveloperTest.Options
{
    public class DataStoreOptions
    {
        [Required]
        public required string Type { get; init; } = "Default";
    }
}
