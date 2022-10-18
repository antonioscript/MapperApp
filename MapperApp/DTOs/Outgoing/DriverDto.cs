namespace MapperApp.DTOs.Outgoing
{
    public class DriverDto
    {
        //Retornando o Nome Completo
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public int DriverNumber { get; set; }
        public int WorldChampionships { get; set; }
    }
}
