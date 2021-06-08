using LT.DigitalOffice.Kernel.Attributes;
using LT.DigitalOffice.Kernel.Configurations;
using LT.DigitalOffice.Models.Broker.Requests.Company;
using LT.DigitalOffice.Models.Broker.Requests.Project;
using LT.DigitalOffice.Models.Broker.Requests.User;

namespace LT.DigitalOffice.SearchService.Models.Dto.Configurations
{
    public class RabbitMqConfig : BaseRabbitMqConfig
    {
        [AutoInjectRequest(typeof(ISearchProjectsRequest))]
        public string SearchProjectsEndpoint { get; set; }

        [AutoInjectRequest(typeof(ISearchUsersRequests))]
        public string SearchUsersEndpoint { get; set; }

        [AutoInjectRequest(typeof(ISearchDepartmentsRequests))]
        public string SearchDepartmentsEndpoint { get; set; }
    }
}
