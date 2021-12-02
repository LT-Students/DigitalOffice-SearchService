using LT.DigitalOffice.Kernel.BrokerSupport.Attributes;
using LT.DigitalOffice.Kernel.BrokerSupport.Configurations;
using LT.DigitalOffice.Models.Broker.Requests.Department;
using LT.DigitalOffice.Models.Broker.Requests.Project;
using LT.DigitalOffice.Models.Broker.Requests.User;

namespace LT.DigitalOffice.SearchService.Models.Dto.Configurations
{
  public class RabbitMqConfig : BaseRabbitMqConfig
    {
        [AutoInjectRequest(typeof(ISearchProjectsRequest))]
        public string SearchProjectsEndpoint { get; set; }

        [AutoInjectRequest(typeof(ISearchUsersRequest))]
        public string SearchUsersEndpoint { get; set; }

        [AutoInjectRequest(typeof(ISearchDepartmentsRequest))]
        public string SearchDepartmentsEndpoint { get; set; }
    }
}
