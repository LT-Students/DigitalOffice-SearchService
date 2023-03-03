using LT.DigitalOffice.Kernel.BrokerSupport.Attributes;
using LT.DigitalOffice.Kernel.BrokerSupport.Configurations;
using LT.DigitalOffice.Models.Broker.Requests.Department;
using LT.DigitalOffice.Models.Broker.Requests.News;
using LT.DigitalOffice.Models.Broker.Requests.Project;
using LT.DigitalOffice.Models.Broker.Requests.User;

namespace LT.DigitalOffice.SearchService.Models.Dto.Configurations
{
  public class RabbitMqConfig : BaseRabbitMqConfig
  {
    #region request endpoints

    [AutoInjectRequest(typeof(ISearchProjectsRequest))]
    public string SearchProjectsEndpoint { get; init; }

    [AutoInjectRequest(typeof(ISearchUsersRequest))]
    public string SearchUsersEndpoint { get; init; }

    [AutoInjectRequest(typeof(ISearchDepartmentsRequest))]
    public string SearchDepartmentsEndpoint { get; init; }

    [AutoInjectRequest(typeof(ISearchNewsRequest))]
    public string SearchNewsEndpoint { get; init; }

    #endregion
  }
}
