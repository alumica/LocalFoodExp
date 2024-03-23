using System.ComponentModel;

namespace WebApi.Models.Host
{
    public class HostFilterModel : PagingModel
    {
        [DefaultValue(false)]
        public bool? IsPaged { get; set; }
    }
}
