namespace JobsOffer.Api.Business.Helpers.LambdaManagement.Models
{
    public class ConditionGroupModel
    {
        public string LogicalOperator { get; set; } = "AND";
        public List<ConditionModel> Conditions { get; set; } = [];
        public List<ConditionGroupModel> Groups { get; set; } = [];
    }
}
