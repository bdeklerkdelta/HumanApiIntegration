using System.ComponentModel.DataAnnotations;

namespace HealthApp.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}