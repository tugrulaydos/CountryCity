namespace CountryCity.Models
{
    using StackExchange.Redis;

    public class RedisService 
    {
        ConnectionMultiplexer connectionMultiplexer;
        public void Connect() => connectionMultiplexer = ConnectionMultiplexer.Connect("localhost:1453"); //Redis sunucusuna bağlantı gerçekleştiriyoruz.
        public IDatabase GetDb(int db)
        {
            return connectionMultiplexer.GetDatabase(db);
        }  //Redis VeriTabanları.


        //Burada Connect metodu içerisinde 'ConnectionMultiplexer' sınıfıyla Redis sunucusuna bağlantı gerçekleştirilmekte 
        //ve ardından 'GetDb' metodu ile de bağlantı gerçekleştirilmiş ilgili sınıf üzerinden Redis sunucusu üzerindeki
        //veritabanları çağırılarak elde edilmektedir.


    }


}
