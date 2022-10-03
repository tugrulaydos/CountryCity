namespace CountryCity.Models.ViewModel
{
    public class RoleAssignViewModel
    {
        //Bu sınıf o anki kullanıcının bilgilerini tutacak.
        public int RoleId { get; set; }
        public string RoleName { get; set; } 
        public bool HasAssign { get; set; }  //O anki rolun kullanıcıya atanıp atanmadığının bilgisi tutulur.

    }
}
