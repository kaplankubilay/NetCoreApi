using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Concrete
{
    public class ExceptionLog:IEntity
    {
        public ExceptionLog(string ErrorMessage, string Controller, string Action)
        {
            CreateDate = DateTime.Now;
            this.ErrorMessage = ErrorMessage;
            this.Controller = Controller;
            this.Action = Action;
        }

        public ExceptionLog()
        {

        }
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string ErrorMessage { get; set; }
    }
}
