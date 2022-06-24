namespace UsedCar.Models.CertificationModel
{
    public class CertificationModel
    {
        public string? Component { get; set; }
        public string? Condition { get; set; }
        public int? Rating { get; set; }
        public Dictionary<string, List<string>>? PropertyCondition { get; set; }
    }



}