using System.ComponentModel;

namespace WebApi.Models.Dish
{
    public class DishFilterModel : PagingModel
    {
        [DefaultValue(false)]
        public bool? IsPaged { get; set; }
    }
}
