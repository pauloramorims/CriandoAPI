using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);   /*resposonsavel por criar minha aplicacao web   */
var app = builder.Build();                          /*nosso hosting, aquele que vai escutar oq o usuario quer acessar*/

//endpoint abaixo!
app.MapGet("/", () => "Hello World! 5");  //toda vez que digitar o endereco da minha aplicacao e barra

app.MapPost ("/", () => new {Name = "Paulo", Age= 25 }); //Criando Objeto Anônimo, o proprio C# converte para o tipo JSON

//Nesta expressão lambda tudo após o "=>" é o corpo do meu método
app.MapGet ("/AddHeader", (HttpResponse response) => //Estou adicionando um cabeçalho e retornando um objeto.
{
    response.Headers.Add("Teste","Paulo Ricardo");
    return new {Name = "Paulo", Age= 25 };
});

app.MapPost("/sabeproduct", (Product product) => {  //Enviar uma informação atraves do Body//
    return product.Code + " - " + product.Name;
});

//↓↓ Extrair relatório do meu endpoint: Numero de usuários que acessou meu sistema
//↓↓ Através de parâmetros via URL: 2 tipos

//api.app.com/users?datestart={date}&dateend={date}  → Via carry string
app.MapGet("/getproduct", ([FromQuery]string dateStart, [FromQuery] string dateEnd) => {
    return dateStart + " - " + dateEnd;
});

//api.app.com/users/{code}  → Via rota
app.MapGet("/getproduct/{code}", ([FromRoute]string code) => {
    return code;
});

app.Run();

public class Product {
    public string Code { get; set; }
    public string Name { get; set; }
}