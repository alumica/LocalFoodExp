using System.ComponentModel;

namespace WebApi.Models.Review
{
    public class ReviewFilterModel : PagingModel
    {
        [DefaultValue(false)]
        public bool? IsPaged { get; set; }
    }
}
