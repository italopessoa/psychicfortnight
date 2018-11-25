using Xunit;
using Amazon.Lambda.TestUtilities;

using Amazon.Lambda.APIGatewayEvents;
using Newtonsoft.Json;

namespace Test.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void FunctionHandlerApiGatewayRequestTest()
        {
            APIGatewayProxyRequest request;
            APIGatewayProxyResponse response;
            request = new APIGatewayProxyRequest();

            // Invoke the lambda function and confirm the string was upper cased.
            Function function = new Function();
            var context = new TestLambdaContext();
            Question expected = new Question
            {
                Idade = 26,
                Text = "Italo Ney Silva Pessoa"
            };

            request.Body = JsonConvert.SerializeObject(expected);
            response = function.FunctionHandler(request,context);

            Assert.Equal(200, response.StatusCode);
            Assert.NotEmpty(response.Body);

            Question actual = JsonConvert.DeserializeObject<Question>(response.Body);

            Assert.Equal(expected, actual);
        }
    }
}
