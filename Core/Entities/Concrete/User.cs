namespace Core.Entities.Concrete
{
    /// <summary>
    /// Uygulama rol bazlı kullanım olacağı/her yarde kullanılacağı için Entity katmanında değil Core da tanımlandı. Ayrıca
    /// Core dan Entity ye referans zaten mevcuttu çift yönlü referans verilemediği için buraya taşındı. 
    /// </summary>
    public class User : IEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }
        public bool Status { get; set; }
    }
}
