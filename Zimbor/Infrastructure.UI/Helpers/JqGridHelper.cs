using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Web.Mvc.JQuery.JqGrid;

namespace Infrastructure.UI.Helpers
{
    public static class JqGridHelper
    {
        public static string GetFilterExpression(JqGridRequest request)
        {
            string result = string.Empty;

            if (request.SearchingFilter != null)
                return GetFilter(request.SearchingFilter.SearchingName, request.SearchingFilter.SearchingOperator, request.SearchingFilter.SearchingValue);
            else if (request.SearchingFilters != null)
            {
                StringBuilder filterExpressionBuilder = new StringBuilder();
                string groupingOperatorToString = request.SearchingFilters.GroupingOperator.ToString();

                foreach (JqGridRequestSearchingFilter element in request.SearchingFilters.Filters)
                {
                    filterExpressionBuilder.Append(GetFilter(element.SearchingName, element.SearchingOperator, element.SearchingValue));
                    filterExpressionBuilder.Append(String.Format(" {0} ", groupingOperatorToString));
                }

                if (filterExpressionBuilder.Length > 0)
                    filterExpressionBuilder.Remove(filterExpressionBuilder.Length - groupingOperatorToString.Length - 2, groupingOperatorToString.Length + 2);

                result = filterExpressionBuilder.ToString();
            }

            return result;
        }

        public static string GetFilterExpression(JqGridRequest request, string ignoreString)
        {
            string result = string.Empty;

            if (request.SearchingFilter != null)
                return GetFilter(request.SearchingFilter.SearchingName, request.SearchingFilter.SearchingOperator, request.SearchingFilter.SearchingValue.Equals(ignoreString) ? "" : request.SearchingFilter.SearchingValue);
            else if (request.SearchingFilters != null)
            {
                StringBuilder filterExpressionBuilder = new StringBuilder();
                string groupingOperatorToString = request.SearchingFilters.GroupingOperator.ToString();

                foreach (JqGridRequestSearchingFilter element in request.SearchingFilters.Filters)
                {
                    filterExpressionBuilder.Append(GetFilter(element.SearchingName, element.SearchingOperator, element.SearchingValue.Equals(ignoreString) ? "" : element.SearchingValue));
                    filterExpressionBuilder.Append(String.Format(" {0} ", groupingOperatorToString));
                }

                if (filterExpressionBuilder.Length > 0)
                    filterExpressionBuilder.Remove(filterExpressionBuilder.Length - groupingOperatorToString.Length - 2, groupingOperatorToString.Length + 2);

                result = filterExpressionBuilder.ToString();
            }

            return result;
        }

        public static string GetSortExpression(JqGridRequest request)
        {
            return request.SortingName + " " + request.SortingOrder;
        }

        public static string GetSortExpressionForStoredProcedure(JqGridRequest request)
        {
            string sortingName = request.SortingName.Substring(request.SortingName.IndexOf(".") + 1);
            return sortingName + " " + request.SortingOrder;
        }

        private static string GetFilter(string searchingName, JqGridSearchOperators searchingOperator, string searchingValue)
        {
            string searchingOperatorString = String.Empty;
            switch (searchingOperator)
            {
                case JqGridSearchOperators.Eq:
                    searchingOperatorString = "=";
                    break;
                case JqGridSearchOperators.Ne:
                    searchingOperatorString = "!=";
                    break;
                case JqGridSearchOperators.Lt:
                    searchingOperatorString = "<";
                    break;
                case JqGridSearchOperators.Le:
                    searchingOperatorString = "<=";
                    break;
                case JqGridSearchOperators.Gt:
                    searchingOperatorString = ">";
                    break;
                case JqGridSearchOperators.Ge:
                    searchingOperatorString = ">=";
                    break;
            }

            if (!string.IsNullOrEmpty(searchingOperatorString))
                return String.Format("{0} {1} {2}", searchingName, searchingOperatorString, searchingValue);

            switch (searchingOperator)
            {
                case JqGridSearchOperators.Bw:
                    return String.Format("{0}.StartsWith(\"{1}\")", searchingName, searchingValue);
                case JqGridSearchOperators.Bn:
                    return String.Format("!{0}.StartsWith(\"{1}\")", searchingName, searchingValue);
                case JqGridSearchOperators.Ew:
                    return String.Format("{0}.EndsWith(\"{1}\")", searchingName, searchingValue);
                case JqGridSearchOperators.En:
                    return String.Format("!{0}.EndsWith(\"{1}\")", searchingName, searchingValue);
                case JqGridSearchOperators.Cn:
                    return String.Format("{0}.Contains(\"{1}\")", searchingName, searchingValue);
                case JqGridSearchOperators.Nc:
                    return String.Format("!{0}.Contains(\"{1}\")", searchingName, searchingValue);
                default:
                    return String.Format("{0} {1} \"{2}\"", searchingName, searchingOperatorString, searchingValue);
            }
        }
    }
}
