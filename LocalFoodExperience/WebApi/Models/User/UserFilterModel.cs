using System.ComponentModel;

namespace WebApi.Models.User
{
    public class UserFilterModel : PagingModel
    {
        [DefaultValue(false)]
        public bool? IsPaged { get; set; }
    }
}
