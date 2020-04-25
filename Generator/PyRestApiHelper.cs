using System;
using System.Collections.Generic;
using System.Text;
using Generator;
using GeneratorService.Models;
using Newtonsoft.Json;
using RestSharp;

namespace GeneratorService
{
    class PyRestApiHelper
    {
        public PyExpressionJson ScrambleExpression(string expression, int maxVar)
        {
            var client = new RestClient("http://127.0.0.1:5000/");
            var expr = new PyExpressionJson();
            expr.Expression = expression;
            expr.MaxVar = maxVar;
            var jsonExpr = JsonConvert.SerializeObject(expr);
            var request = new RestRequest("scramble", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(jsonExpr);

            var response = client.Execute(request);

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                return JsonConvert.DeserializeObject<PyExpressionJson>(response.Content);
            }

            return null;
        }

        public PyEquationJson SolveEquation(string left, string right)
        {
            var client = new RestClient("http://127.0.0.1:5000/");
            var equat = new PyEquationJson();
            equat.LeftSide = left;
            equat.RightSide = right;

            var jsonEquat = JsonConvert.SerializeObject(equat);
            var request = new RestRequest("solve", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(jsonEquat);

            var response = client.Execute(request);

            if (response.ResponseStatus == ResponseStatus.Completed)
            {
                return JsonConvert.DeserializeObject<PyEquationJson>(response.Content);
            }

            return null;
        }
    }
}
