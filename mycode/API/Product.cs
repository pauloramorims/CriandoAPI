public class Product { //Minha tabela
   
    public int Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }    //Relacionamento de 1:1;
    public Category Category { get; set; } //Relacionamento de 1:1;
    public List<Tag> Tags { get; set; }    //Relacionamento de 1:N;
};
