namespace Prueba_Tecnica.Model
{
    public class ResponseData
    {
        public String Status { get; set; }
        public Object Data { get; set; }
    }
    public class QuestionsReturn
    {
        public double NumberQuestion { get; set; }
        public String? Description { get; set; }
        public double? NumberNextQuestion { get; set; }
        public List<Answers> Answers { get; set; }
        public String Type { get; set; }
    }
    public class AnswersUser
    {
        public String? R1 { get; set; }
        public String? R2 { get; set; }
        public String? R3 { get; set; }
        public String? R4 { get; set; }
        public String? R5 { get; set; }
        public String?[] R5_1 { get; set; }
        public String?[] R5_2 { get; set; }
        public double? R6 { get; set; }
        public String?[] R7 { get; set; }
        public String? R8 { get; set; }
    }
    public class AnswersUsers
    {
        public Guid Id { get; set; }
        public String? R1 { get; set; }
        public String? R2 { get; set; }
        public String? R3 { get; set; }
        public String? R4 { get; set; }
        public String? R5 { get; set; }
        public String? R5_1 { get; set; }
        public String? R5_2 { get; set; }
        public double? R6 { get; set; }
        public String? R7 { get; set; }
        public String? R8 { get; set; }
    }
}