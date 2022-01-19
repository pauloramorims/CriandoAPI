
//Toda vez que rodo meu servidor a memoria será perdida para um método do tipo List, se estivesse em um banco de dados isso nãoa conteceria pois estaria num arquivo e não na memória...
public static class ProductRepository{ //A cada requisição que eu realizar, a classe é construida do zero. Para q isso n aconteça utilizamos 'static'
    
    public static List<Product> Products { get; set; } = Products = new List<Product>();//Inicializando minha lista
    
    public static void Init(IConfiguration configuration)
    {
        var products = configuration.GetSection("Products").Get<List<Product>>();
        Products = products; //A lista que iniciei é igual a lista que estou injentando através do IConfiguration
    }

    public static void Add(Product product) //Método para adicionar um poduto
    {      
        Products.Add(product);
    }

    public static Product GetBy(string code){ //Método para procurar um poduto
        return Products.FirstOrDefault(p => p.Code == code); //Se não achar na lista, retorna um valor "null"
    }

    public static void Remove(Product product){ //Método para remover um poduto
        Products.Remove(product);
    }
}
