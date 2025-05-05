using System.ComponentModel.DataAnnotations;

namespace InsigniApi.Models.Attributes
{
    public class ImageUrlAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var url = value.ToString();
            var validExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp" };

            if (url == null || !Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                return new ValidationResult("Invalid URL format.");
            }

            var extension = Path.GetExtension(uri.AbsolutePath).ToLowerInvariant();
            if (!validExtensions.Contains(extension))
            {
                return new ValidationResult($"Invalid image format. Supported formats are: {string.Join(", ", validExtensions)}");
            }

            return ValidationResult.Success;
        }
    }
}
