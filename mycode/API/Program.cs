using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);   /*resposonsavel por criar minha aplicacao web   */
builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration["Database:SqlServer"]); //Servico para realizar comunicação com meu Banco de dados

var app = builder.Build();                          /*nosso hosting, aquele que vai escutar oq o usuario quer acessar*/
var configuration = app.Configuration;
ProductRepository.Init(configuration);

//endpoint abaixo!
app.MapGet("/", () => "Hello World! 5");                 //toda vez que digitar o endereco da minha aplicacao e barra

app.MapPost ("/", () => new {Name = "Paulo", Age= 25 }); //Criando Objeto Anônimo, o proprio C# converte para o tipo JSON

//Nesta expressão lambda tudo após o "=>" é o corpo do meu método
app.MapGet ("/AddHeader", (HttpResponse response) => //Estou adicionando um cabeçalho e retornando um objeto.
{
    response.Headers.Add("Teste","Paulo Ricardo");
    return new {Name = "Paulo", Age= 25 };
});

//↓↓ Extrair relatório do meu endpoint: Numero de usuários que acessou meu sistema
//↓↓ Através de parâmetros via URL: 2 tipos

/*/api.app.com/users?datestart={date}&dateend={date}  → Via carry string
app.MapGet("/getproduct", ([FromQuery]string dateStart, [FromQuery] string dateEnd) => {
    return dateStart + " - " + dateEnd;
});*/

//→ Via Header
app.MapGet("/getproductbyheader", (HttpRequest request) => 
{
    request.Headers["product-code"].ToString();
}); //HttpRequest é responsável por receber a solicitação do usuário no endpoint

//api.app.com/users/{code}  → Via rota
app.MapGet("/products/{code}", ([FromRoute]string code) => {
    var product = ProductRepository.GetBy(code);
    
    if(product != null)
        return Results.Ok(product);
    return Results.NotFound();
        
});
                        //↓Vem do body do meu endpoint↓   ↓Servico do AspNet configurado nas primeitas linhas
app.MapPost("/products", (ProductRequest productRequest, ApplicationDbContext context) => {  //Enviar uma informação atraves do Body//Adicionando um produto a minha lista
    var category = context.Category.Where( c => c.Id == productRequest.CategoryId).First(); //.ToList() ↓ Daria acesso a todas as categorias, mas quero consultar no banco de dados pelo ID da categoria
    
    var product = new Product {
        Code = productRequest.Code,
        Name = productRequest.Name,
        Description = productRequest.Description,
        Category = category
    };

    
    context.Products.Add(product);
    context.SaveChanges(); //Instrução para commitar
    
    return Results.Created($"/products/{product.Id}", product.Id);
});

app.MapPut("/products", (Product product) => {    //endpoint para editar meus produtos
    var productSaved = ProductRepository.GetBy(product.Code);
    productSaved.Name = product.Name;
    return Results.Ok();
});

app.MapDelete("/products/{code}", ([FromRoute]string code) => { //endpoint para deletar algum produto
    var productSaved = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSaved);

    if(productSaved != null)
        return Results.Ok(productSaved);
    return Results.NotFound();
});

app.MapGet("/configuration/database", (IConfiguration configuration) =>{
    return Results.Ok($"{configuration["database:connection"]}/{configuration["database:Port"]}");
});

app.Run();
