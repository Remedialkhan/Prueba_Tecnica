using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Model;

namespace Prueba_Tecnica.Data
{
    public class QuizManager
    {
        PruebaContext db = new();

        public QuestionsReturn getQuizz(double numberQuestion)
        {
            QuestionsReturn? data = new();
            try
            {
                data = db.questions.Where(x => x.numberquestion == numberQuestion).Select(x => new QuestionsReturn
                {
                    Description = x.description,
                    NumberQuestion = x.numberquestion,
                    Type = x.type,
                }).FirstOrDefault();
                if (numberQuestion != 6)
                {
                    List<Answers> answers = db.answers.Where(x => x.numberquestion == numberQuestion).ToList();
                    data.Answers = answers;
                    data.NumberNextQuestion = answers.FirstOrDefault().nextquestion;

                }
                else
                {
                    data.NumberNextQuestion = 7;
                }

            }
            catch (Exception e)
            {

                throw e;
            }
            return data;
        }
        public bool postQuizz(AnswersUser answers)
        {
            bool data = false;
            try
            {
                Guid id = Guid.NewGuid();

                Quizzez query1 = new Quizzez();
                query1.id = id;
                db.quizzez.Add(query1);


                Quiz_Questions query2 = new Quiz_Questions();
                query2.idquiz = id;
                query2.numberquestion = 1;
                query2.letteranswer = answers.R1;
                db.quiz_questions.Add(query2);

                Quiz_Questions query3 = new Quiz_Questions();
                query3.idquiz = id;
                query3.numberquestion = 2;
                query3.letteranswer = answers.R2;
                db.quiz_questions.Add(query3);

                Quiz_Questions query4 = new Quiz_Questions();
                query4.idquiz = id;
                query4.numberquestion = 3;
                query4.letteranswer = answers.R3;
                db.quiz_questions.Add(query4);

                Quiz_Questions query5 = new Quiz_Questions();
                query5.idquiz = id;
                query5.numberquestion = 4;
                query5.letteranswer = answers.R4;
                db.quiz_questions.Add(query5);

                Quiz_Questions query6 = new Quiz_Questions();
                query6.idquiz = id;
                query6.numberquestion = 5;
                query6.letteranswer = answers.R5;
                db.quiz_questions.Add(query6);

                Quiz_Questions query9 = new Quiz_Questions();
                query9.idquiz = id;
                query9.numberquestion = 6;
                query9.letteranswer = Convert.ToString(answers.R6);
                db.quiz_questions.Add(query9);

                Quiz_Questions query11 = new Quiz_Questions();
                query11.idquiz = id;
                query11.numberquestion = 8;
                query11.letteranswer = answers.R8;
                db.quiz_questions.Add(query11);

                db.SaveChanges();
                SaveAnswersUser(id, 5.1, answers.R5_1);
                SaveAnswersUser(id, 5.2, answers.R5_2);
                SaveAnswersUser(id, 7, answers.R7);
                data = true;


            }
            catch (Exception e)
            {

                throw e;
            }
            return data;
        }
        public List<AnswersUsers> getDataTable()
        {
            try
            {
                List<AnswersUsers> data = new();
                List<Answers> answers = db.answers.ToList();
                List<Questions> questions = db.questions.ToList();
                var data1 = db.quizzez.Join(db.quiz_questions, qz => qz.id, qu => qu.idquiz, (qz, qu) => new { qz, qu })
                    .Select(x => new
                    {
                        x.qz.id,
                        x.qu.numberquestion,
                        nameQuestion = x.qu.numberquestion,
                        x.qu.letteranswer
                    }).ToList();
                var data2 = data1.Select(x => new
                {
                    x.id,
                    x.numberquestion,
                    name = getNameQuestion(questions, x.nameQuestion),
                    answer = getNameAnswer(answers, x.letteranswer, x.numberquestion)
                }).ToList();
                List<Guid> ids = data2.Select(x => x.id).Distinct().ToList();
                foreach (Guid id in ids)
                {
                    int i = 1;
                    AnswersUsers answer = new();
                    foreach (var datas in data2)
                    {
                        if (id == datas.id)
                        {
                            switch (datas.numberquestion)
                            {
                                case 1:
                                    answer.R1 = datas.answer;
                                    break;
                                case 2:
                                    answer.R2 = datas.answer;
                                    break;
                                case 3:
                                    answer.R3 = datas.answer;
                                    break;
                                case 4:
                                    answer.R4 = datas.answer;
                                    break;
                                case 5:
                                    answer.R5 = datas.answer;
                                    break;
                                case 5.1:
                                    answer.R5_1 += datas.answer + " ,";
                                    break;
                                case 5.2:
                                    answer.R5_2 += datas.answer + " ,";
                                    break;
                                case 6:
                                    answer.R6 = Convert.ToDouble(datas.answer);
                                    break;
                                case 7:
                                    answer.R7 += datas.answer + " ,";
                                    break;
                                case 8:
                                    answer.R8 = datas.answer;
                                    break;

                            }
                        }
                    }
                    answer.Id = id;
                    data.Add(answer);
                }
                return data;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        private void SaveAnswersUser(Guid id, double number, String?[] answers)
        {
            try
            {
                if (answers.Length != 0 || answers != null)
                {
                    foreach (String? item in answers)
                    {
                        Quiz_Questions query = new Quiz_Questions();
                        query.idquiz = id;
                        query.numberquestion = number;
                        query.letteranswer = item;
                        db.quiz_questions.Add(query);
                        db.SaveChanges();
                    }
                }
                else
                {
                    Quiz_Questions query = new Quiz_Questions();
                    query.idquiz = id;
                    query.numberquestion = number;
                    query.letteranswer = null;
                    db.quiz_questions.Add(query);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        private String? getNameQuestion(List<Questions> questions, double? number)
        {
            try
            {
                String? name = questions.Where(x => x.numberquestion == number).Select(x => x.description).FirstOrDefault();
                return name;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        private String? getNameAnswer(List<Answers> answers, String? letter, double? numberQuestion)
        {
            try
            {
                String? name = "";
                if (numberQuestion != 6)
                {

                    name = answers.Where(x => x.letter == Convert.ToChar(letter) && x.numberquestion == numberQuestion).Select(x => x.description).FirstOrDefault();
                }
                else
                {
                    name = letter;
                }
                return name;
            }
            catch (Exception e)
            {

                throw e;
            }
        }
    }
}