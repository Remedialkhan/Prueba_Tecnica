using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica.Data;
using Prueba_Tecnica.Model;
namespace Prueba_Tecnica.Controllers.API
{
    [Route("api/Quizz")]
    [ApiController]
    public class QuizzApiController : ControllerBase
    {

        private QuizManager quiz = new QuizManager();

        [HttpGet("getQuizz")]
        public ResponseData getQuizz(double id)
        {
            try
            {
                QuestionsReturn dataResponse = quiz.getQuizz(id);
                return new ResponseData
                {
                    Status = "Ok",
                    Data = dataResponse
                };
            }
            catch (Exception e)
            {

                return new ResponseData
                {
                    Status = "Error",
                    Data = e.Message
                };
            }
        }
        [HttpPost("PostQuizz")]
        public ResponseData postQuizz(AnswersUser answers)
        {
            try
            {
                bool dataResponse = quiz.postQuizz(answers);
                return new ResponseData
                {
                    Status = "Ok",
                    Data = dataResponse
                };
            }
            catch (Exception e)
            {

                throw e;
            }
        }
        [HttpGet("getDataTable")]
        public ResponseData getDataTable()
        {
            try
            {
                List<AnswersUsers> response = quiz.getDataTable();
                return new ResponseData
                {
                    Status = "Ok",
                    Data = response
                };
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
