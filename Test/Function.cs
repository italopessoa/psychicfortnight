using System;
using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Test
{
    public static class StringExtensions
    {
        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

    }
    public class Function
    {

        public APIGatewayProxyResponse FunctionHandler(APIGatewayProxyRequest request, ILambdaContext context)
        {
            string body = request.IsBase64Encoded ? request.Body.Base64Decode() : request.Body;
            context.Logger.LogLine($"REQUEST BODY= {body}");

            context.Logger.LogLine("DESERIALIZAR");
            Question question = JsonConvert.DeserializeObject<Question>(body);
            context.Logger.LogLine("DESERIALIZAR OK");

            var response = new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConvert.SerializeObject(question),
                Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
            };

            return response;
        }
    }

    public class Question
    {
        public string Text { get; set; }
        public int Idade { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Question question = obj as Question;

            if (!Text.Equals(question.Text))
            {
                return false;
            }

            if (Idade != question.Idade)
            {
                return false;
            }

            return true;
        }
    }
}
