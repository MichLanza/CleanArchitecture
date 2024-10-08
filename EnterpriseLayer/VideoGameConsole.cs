namespace EnterpriseLayer
{
    public class VideoGameConsole
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public DateTime LaunchDate { get; set; }

        public bool IsRetro() => LaunchDate.Year <= 2005;
    }
}
