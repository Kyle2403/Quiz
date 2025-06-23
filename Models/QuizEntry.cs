namespace Quiz.Models
{
    public class QuizEntry
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }


        public string AnswerText { get; set; }

        public bool IsCorrect { get; set; }
    }
    public record QuestionGroupKey(int QuestionId, string QuestionText);

}
