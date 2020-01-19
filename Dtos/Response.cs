using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Dtos
{
    public class Response
    {
        public string Text { get; set; }
        public ResponseType ResponseT { get; set; }

        public Response(string text, ResponseType responseType)
        {
            this.Text = text;
            this.ResponseT = responseType;
        }
    }

    public enum ResponseType
    {
        Question = 0,
        Answere = 1
    }
}
