using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EnumGenie.Sample.Enums
{
    public enum Described
    {
        [Display(Description = "Number 1")]
        First,
        [Display(Description = "Number 2")]
        Second,
        [Display(Description = "Number 3")]
        Third
    }
}