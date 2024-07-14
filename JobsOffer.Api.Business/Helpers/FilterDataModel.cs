﻿using JobsOffer.Api.Business.Helpers.LambdaManagement.Models;

namespace JobsOffer.Api.Business.Helpers
{
    public class FilterDataModel
    {
        public LambdaExpressionModel LambdaExpressionModel { get; set; } = new LambdaExpressionModel();
        public string Includes { get; set; } = string.Empty;
        public string SplitChar { get; set; } = ",";
        public bool DisableTracking { get; set; } = true;
        public int Take { get; set; } = 0;
        public int Offset { get; set; } = 0;
    }
}
