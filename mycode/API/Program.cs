using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);   /*resposonsavel por criar minha aplicacao web   */
var app = builder.Build();                          /*nosso hosting, aquele que vai escutar oq o usuario quer acessar*/

//endpoint abaixo!
app.MapGet("/", () => "Hello World! 5");                 //toda vez que digitar o endereco da minha aplicacao e barra

app.MapPost ("/", () => new {Name = "Paulo", Age= 25 }); //Criando Objeto Anônimo, o proprio C# converte para o tipo JSON

//Nesta expressão lambda tudo após o "=>" é o corpo do meu método
app.MapGet ("/AddHeader", (HttpResponse response) => //Estou adicionando um cabeçalho e retornando um objeto.
{
    response.Headers.Add("Teste","Paulo Ricardo");
    return new {Name = "Paulo", Age= 25 };
});

app.MapPost("/products", (Product product) => {  //Enviar uma informação atraves do Body//Adicionando um produto a minha lista
    ProductRepository.Add(product);
});

//↓↓ Extrair relatório do meu endpoint: Numero de usuários que acessou meu sistema
//↓↓ Através de parâmetros via URL: 2 tipos

/*/api.app.com/users?datestart={date}&dateend={date}  → Via carry string
app.MapGet("/getproduct", ([FromQuery]string dateStart, [FromQuery] string dateEnd) => {
    return dateStart + " - " + dateEnd;
});*/

//api.app.com/users/{code}  → Via rota
app.MapGet("/products/{code}", ([FromRoute]string code) => {
    var product = ProductRepository.GetBy(code);
    return product;
});

//→ Via Header
app.MapGet("/getproductbyheader", (HttpRequest request) => 
{
    request.Headers["product-code"].ToString();
}); //HttpRequest é responsável por receber a solicitação do usuário no endpoint

app.MapPut("/products", (Product product) => {    //endpoint para editar meus produtos
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
});

app.MapDelete("/products/{code}", ([FromRoute]string code) => {
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);
    
});

app.Run();

//Toda vez que rodo meu servidor a memoria será perdida para um método do tipo List, se estivesse em um banco de dados isso nãoa conteceria pois estaria num arquivo e não na memória...
public static class ProductRepository{ //A cada requisição que eu realizar, a classe é construida do zero. Para q isso n aconteça utilizamos 'static'
    public static List<Product> Products { get; set; } //Método para adicionar um poduto
    public static void Add(Product product) //Método para adicionar um poduto
    {
        if (Products == null)   //Conferindo se alista está vazia
            Products = new List<Product>();

        Products.Add(product);
    }

    public static Product GetBy(string code){ //Método para procurar um poduto
        return Products.FirstOrDefault(p => p.Code == code); //Se não achar na lista, retorna um valor "null"
    }

    public static void Remove(Product product){ //Método para remover um poduto
        Products.Remove(product);
    }
}
public class Product { //Minha tabela
    public string Code { get; set; }
    public string Name { get; set; }
};