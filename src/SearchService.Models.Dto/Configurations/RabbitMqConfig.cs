using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Configurations;
using LT.DigitalOffice.Models.Broker.Requests.Project;

namespace LT.DigitalOffice.SearchService.Models.Dto.Configurations
{
    public class RabbitMqConfig : BaseRabbitMqConfig
    {
        [AutoInjectRequest(typeof(ISearchProjectsRequest))]
        public string SearchProjectsEndpoint { get; set; }
    }
}
