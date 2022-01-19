//Representação da minha entidade, pois sempre é bom "protegê-la"
//Pois seus payLoads tendem a ser diferentes
public record ProductRequest(string Code, string Name, string Description, int CategoryId, List<string>tags); //Request, command...Objeto de transferência de dados
